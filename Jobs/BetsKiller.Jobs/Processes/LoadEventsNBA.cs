﻿using NCalc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using BetsKiller.Helper.Constants;
using BetsKiller.Helper.Types;
using BetsKiller.Model;

namespace BetsKiller.Jobs.Processes
{
    public class LoadEventsNBA : Load
    {
        #region Private

        private List<string> _dates;

        private List<BetStatus> _betStatuses;
        private List<Sport> _sports;
        private List<TeamsNBA> _teams;

        private List<ScheduleResultsNBA> _updatedScheduleResults;
        private List<ScheduleResultsNBA> _addedScheduleResults;
        private List<Analysis> _updatedAnalysis;

        #endregion

        #region Constructors

        public LoadEventsNBA(List<string> dates)
        {
            this._dates = dates;
        }

        #endregion

        #region Methods

        public void Start()
        {
            // Get bet statuses and sportss
            this._betStatuses = base.GetBetStatuses();
            this._sports = base.GetSports();
            this._teams = base.GetTeamsNBA();

            foreach (var date in this._dates)
            {
                // Get events NBA
                this.GetParseEventsNBA(date);

                // Get related analysis and resolve their status
                this.GetResolveRelatedAnalysis();

                // Save events
                this.SaveEventsNBA();

                Thread.Sleep(base.WAIT_TIME); //TODO: Erikberg limit request per 10 seconds
            }
        }

        #endregion

        #region Helper methods

        private void GetParseEventsNBA(string date)
        {
            this._updatedScheduleResults = new List<ScheduleResultsNBA>();
            this._addedScheduleResults = new List<ScheduleResultsNBA>();

            // Get todays games
            var methodEvents = new API.Erikberg.Methods.MethodEvents();
            var dataEvents = methodEvents.Get(date, SportConst.NBA);

            foreach (var dataEvent in dataEvents.Event)
            {
                var scheduleResult = base.AppDataRepository.GetAllScheduleResultsNBA().Where(x => x.EventId == dataEvent.EventId).FirstOrDefault();

                if (scheduleResult != null) // Update existing event in DB
                {
                    scheduleResult.TeamPointsScored = dataEvent.HomePointsScored;
                    scheduleResult.TeamPeriodScores = string.Join(",", dataEvent.HomePeriodScores);

                    scheduleResult.OpponentPointsScored = dataEvent.AwayPointsScored;
                    scheduleResult.OpponentPeriodScores = string.Join(",", dataEvent.AwayPeriodScores);

                    scheduleResult.EventStatus = dataEvent.EventStatus;

                    this._updatedScheduleResults.Add(scheduleResult);
                }
                else // Event is new so it has to be saved in DB
                {
                    scheduleResult = new ScheduleResultsNBA();
                    scheduleResult.EventId = dataEvent.EventId;
                    scheduleResult.EventStatus = dataEvent.EventStatus;
                    scheduleResult.EventStartDateTime = dataEvent.StartDateTime;
                    scheduleResult.EventSeasonType = dataEvent.SeasonType;
                    scheduleResult.TeamEventNumberInSeason = -1;
                    scheduleResult.TeamEventLocationType = null;
                    scheduleResult.TeamEventResult = null;
                    scheduleResult.TeamPointsScored = -1;
                    scheduleResult.TeamEventsWon = -1;
                    scheduleResult.TeamEventsLost = -1;
                    scheduleResult.OpponentPointsScored = -1;
                    scheduleResult.OpponentEventsWon = -1;
                    scheduleResult.OpponentEventsLost = -1;
                    scheduleResult.SiteCapacity = dataEvent.Site.Capacity;
                    scheduleResult.SiteSurface = dataEvent.Site.Surface;
                    scheduleResult.SiteName = dataEvent.Site.Name;
                    scheduleResult.SiteState = dataEvent.Site.State;
                    scheduleResult.SiteCity = dataEvent.Site.City;

                    var homeTeam = this._teams.Where(x => x.Name.NameErikberg == dataEvent.HomeTeam.TeamId).FirstOrDefault();
                    if (homeTeam != null)
                    {
                        scheduleResult.Team_Id = homeTeam.Id;
                    }

                    var awayTeam = this._teams.Where(x => x.Name.NameErikberg == dataEvent.AwayTeam.TeamId).FirstOrDefault();
                    if (awayTeam != null)
                    {
                        scheduleResult.Opponent_Id = awayTeam.Id;
                    }

                    this._addedScheduleResults.Add(scheduleResult);
                }
            }
        }

        private void GetResolveRelatedAnalysis()
        {
            this._updatedAnalysis = new List<Analysis>();

            // Get related anlysis foreach updated scheduleResult and resolve their status.
            foreach (var scheduleResult in this._updatedScheduleResults)
            {
                // See is game completed/postponed/suspended/cancelled
                if (scheduleResult.EventStatus != EventStatusConst.SCHEDULED)
                {
                    // Find if game is connected to any analysis
                    var analysis = base.AppDataRepository.GetAllAnalysis().Where(x => x.EventNBA != null && x.EventNBA.EventId == scheduleResult.EventId).ToList();

                    foreach (var analyse in analysis)
                    {
                        if (analyse.BetStatus.Name == BetStatusConst.SCHEDULED) // Don't update updated analysis
                        {
                            this.ResloveAnalyse(analyse, scheduleResult);

                            this.UpdateAnalysisProfit(analyse);

                            this._updatedAnalysis.Add(analyse);
                        }
                    }
                }
            }
        }

        private void ResloveAnalyse(Analysis analyse, ScheduleResultsNBA scheduleResult)
        {
            // Update data in analysis
            analyse.Changed = DateTime.Now;

            var homePoints = scheduleResult.TeamPeriodScores.Split(',').Select(x => Convert.ToInt32(x)).ToList();
            var awayPoints = scheduleResult.OpponentPeriodScores.Split(',').Select(x => Convert.ToInt32(x)).ToList();

            analyse.Result = scheduleResult.TeamPointsScored.ToString() + " - " + scheduleResult.OpponentPointsScored.ToString() + " (" + (homePoints[0] + homePoints[1]).ToString() + " - " + (awayPoints[0] + awayPoints[1]).ToString() + ")";

            // First see is game postponed, suspended or cancelled, then it is PUSH bet status
            if ((scheduleResult.EventStatus == EventStatusConst.POSTPONED) || (scheduleResult.EventStatus == EventStatusConst.SUSPENDED) || (scheduleResult.EventStatus == EventStatusConst.CANCELLED))
            {
                analyse.BetStatus_Id = this._betStatuses.Single(x => x.Name == BetStatusConst.PUSH).Id;
                analyse.Profit = 0;
            }
            else // It is completed so we can resolve result
            {
                // First resolve PUSH status if it is set
                var isPush = false;
                if (!string.IsNullOrEmpty(analyse.BetLogicPush))
                {
                    var pushExpression = analyse.BetLogicPush
                                            .Replace("X", scheduleResult.TeamPointsScored.ToString())
                                            .Replace("X1", homePoints[0].ToString())
                                            .Replace("X2", homePoints[1].ToString())
                                            .Replace("X3", homePoints[2].ToString())
                                            .Replace("X4", homePoints[3].ToString())
                                            .Replace("Y", scheduleResult.OpponentPointsScored.ToString())
                                            .Replace("Y1", awayPoints[0].ToString())
                                            .Replace("Y2", awayPoints[1].ToString())
                                            .Replace("Y3", awayPoints[2].ToString())
                                            .Replace("Y4", awayPoints[3].ToString());

                    var expression = new Expression(pushExpression);
                    isPush = (bool)expression.Evaluate();
                }

                if (isPush)
                {
                    analyse.BetStatus_Id = this._betStatuses.Single(x => x.Name == BetStatusConst.PUSH).Id;
                    analyse.Profit = 0;
                }
                else // If it is not PUSH status, resolve WIN/LOSS status.+
                {
                    var winLossExpression = analyse.BetLogicWinLoss
                                            .Replace("X", scheduleResult.TeamPointsScored.ToString())
                                            .Replace("X1", homePoints[0].ToString())
                                            .Replace("X2", homePoints[1].ToString())
                                            .Replace("X3", homePoints[2].ToString())
                                            .Replace("X4", homePoints[3].ToString())
                                            .Replace("Y", scheduleResult.OpponentPointsScored.ToString())
                                            .Replace("Y1", awayPoints[0].ToString())
                                            .Replace("Y2", awayPoints[1].ToString())
                                            .Replace("Y3", awayPoints[2].ToString())
                                            .Replace("Y4", awayPoints[3].ToString());

                    var expression = new Expression(winLossExpression);
                    var isWin = (bool)expression.Evaluate();

                    if (isWin)
                    {
                        analyse.BetStatus_Id = this._betStatuses.Single(x => x.Name == BetStatusConst.WIN).Id;
                        analyse.Profit = Math.Round(((analyse.Invested * analyse.Odd) - analyse.Invested), 2);
                    }
                    else
                    {
                        analyse.BetStatus_Id = this._betStatuses.Single(x => x.Name == BetStatusConst.LOSS).Id;
                        analyse.Profit = -analyse.Invested;
                    }
                }
            }
        }

        private void UpdateAnalysisProfit(Analysis analyse)
        {
            var year = analyse.Created.Year;
            var month = analyse.Created.Month;
            var week = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(analyse.Created, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

            var analysisProfit = base.AppDataRepository.GetAllAnalysisProfit().Where(x => x.Year == year && x.Week == week).FirstOrDefault();

            if (analysisProfit != null)
            {
                analysisProfit.TotalBets++;

                if (analyse.BetStatus_Id == this._betStatuses.Single(x => x.Name == BetStatusConst.WIN).Id)
                {
                    analysisProfit.Wins++;
                }
                else if (analyse.BetStatus_Id == this._betStatuses.Single(x => x.Name == BetStatusConst.LOSS).Id)
                {
                    analysisProfit.Losses++;
                }

                analysisProfit.Invested += analyse.Invested;
                analysisProfit.Profit += analyse.Profit.Value;
                analysisProfit.ROI = analysisProfit.Invested > 0 ? Math.Round(analysisProfit.Profit / analysisProfit.Invested, 4) : 0;
            }
            else
            {
                analysisProfit = new AnalysisProfit();
                analysisProfit.Sport_Id = this._sports.Single(x => x.Name == SportConst.NBA).Id;
                analysisProfit.TotalBets = 1;
                analysisProfit.Year = year;
                analysisProfit.Month = month;
                analysisProfit.Week = week;
                analysisProfit.FirstDayInWeek = TypeDateTime.GetFirstDayOfWeek(DateTime.Now);

                if (analyse.BetStatus_Id == this._betStatuses.Single(x => x.Name == BetStatusConst.WIN).Id)
                {
                    analysisProfit.Wins = 1;
                }
                else if (analyse.BetStatus_Id == this._betStatuses.Single(x => x.Name == BetStatusConst.LOSS).Id)
                {
                    analysisProfit.Losses = 1;
                }

                analysisProfit.Invested = analyse.Invested;
                analysisProfit.Profit = analyse.Profit.Value;
                analysisProfit.ROI = analysisProfit.Invested > 0 ? Math.Round(analysisProfit.Profit / analysisProfit.Invested, 4) : 0;
            }

            base.AppDataRepository.SaveAnalysisProfit(new List<AnalysisProfit>() { analysisProfit });
        }

        private void SaveEventsNBA()
        {
            if (this._updatedAnalysis.Count > 0)
            {
                base.AppDataRepository.SaveAnalysis(this._updatedAnalysis);
            }

            if (this._updatedScheduleResults.Count > 0)
            {
                base.AppDataRepository.SaveScheduleResultsNBA(this._updatedScheduleResults);
            }

            if (this._addedScheduleResults.Count > 0)
            {
                base.AppDataRepository.SaveScheduleResultsNBA(this._addedScheduleResults);
            }
        }

        #endregion
    }
}

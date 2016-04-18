using BetsKiller.DAL;
using BetsKiller.DAL.AppData;
using BetsKiller.Helper.Constants;
using BetsKiller.Helper.Types;
using BetsKiller.Model;
using NCalc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

            foreach (string date in this._dates)
            {
                // Get events NBA
                this.GetParseEventsNBA(date);

                // Get related analysis and resolve their status
                this.GetResolveRelatedAnalysis();

                // Save events
                this.SaveEventsNBA();

                // Update analysis profit
                this.UpdateAnalysisProfit();

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
            BetsKiller.API.Erikberg.Methods.MethodEvents methodEvents = new API.Erikberg.Methods.MethodEvents();
            BetsKiller.API.Erikberg.Entities.Events dataEvents = methodEvents.Get(date, SportConst.NBA);

            foreach (BetsKiller.API.Erikberg.Entities.Event dataEvent in dataEvents.Event)
            {
                ScheduleResultsNBA scheduleResult = base.AppDataRepository.GetAllScheduleResultsNBA().Where(x => x.EventId == dataEvent.EventId).FirstOrDefault();

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

                    TeamsNBA homeTeam = this._teams.Where(x => x.Name.NameErikberg == dataEvent.HomeTeam.TeamId).FirstOrDefault();
                    if (homeTeam != null)
                    {
                        scheduleResult.Team_Id = homeTeam.Id;
                    }

                    TeamsNBA awayTeam = this._teams.Where(x => x.Name.NameErikberg == dataEvent.AwayTeam.TeamId).FirstOrDefault();
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
            foreach (ScheduleResultsNBA scheduleResult in this._updatedScheduleResults)
            {
                // See is game completed/postponed/suspended/cancelled
                if (scheduleResult.EventStatus != EventStatusConst.SCHEDULED)
                {
                    // Find if game is connected to any analysis
                    List<Analysis> analysis = base.AppDataRepository.GetAllAnalysis().Where(x => x.EventNBA != null && x.EventNBA.EventId == scheduleResult.EventId).ToList();

                    foreach (Analysis analyse in analysis)
                    {
                        if (analyse.BetStatus.Name == BetStatusConst.SCHEDULED) // Don't update updated analysis
                        {
                            this.ResloveAnalyse(analyse, scheduleResult);

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

            List<int> homePoints = scheduleResult.TeamPeriodScores.Split(',').Select(x => Convert.ToInt32(x)).ToList();
            List<int> awayPoints = scheduleResult.OpponentPeriodScores.Split(',').Select(x => Convert.ToInt32(x)).ToList();

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
                bool isPush = false;
                if (!string.IsNullOrEmpty(analyse.BetLogicPush))
                {
                    string pushExpression = analyse.BetLogicPush
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

                    Expression expression = new Expression(pushExpression);
                    isPush = (bool)expression.Evaluate();
                }

                if (isPush)
                {
                    analyse.BetStatus_Id = this._betStatuses.Single(x => x.Name == BetStatusConst.PUSH).Id;
                    analyse.Profit = 0;
                }
                else // If it is not PUSH status, resolve WIN/LOSS status.+
                {
                    string winLossExpression = analyse.BetLogicWinLoss
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

                    Expression expression = new Expression(winLossExpression);
                    bool isWin = (bool)expression.Evaluate();

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

        private void UpdateAnalysisProfit()
        {
            int totalBets = 0, wins = 0, losses = 0;
            decimal invested = 0, profit = 0;

            foreach (Analysis analyse in this._updatedAnalysis)
            {
                totalBets++;

                if (analyse.BetStatus_Id == this._betStatuses.Single(x => x.Name == BetStatusConst.WIN).Id)
                {
                    wins++;
                }
                else if (analyse.BetStatus_Id == this._betStatuses.Single(x => x.Name == BetStatusConst.LOSS).Id)
                {
                    losses++;
                }

                profit += analyse.Profit.Value;
                invested += analyse.Invested;
            }

            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int week = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

            AnalysisProfit analysisProfit = base.AppDataRepository.GetAllAnalysisProfit().Where(x => x.Year == year && x.Month == month && x.Week == week).FirstOrDefault();

            if (analysisProfit != null)
            {
                analysisProfit.TotalBets += totalBets;
                analysisProfit.Wins += wins;
                analysisProfit.Losses += losses;
                analysisProfit.Profit += profit;
                analysisProfit.Invested += invested;
                analysisProfit.ROI = analysisProfit.Invested > 0 ? Math.Round(analysisProfit.Profit / analysisProfit.Invested, 4) : 0;
            }
            else
            {
                analysisProfit = new AnalysisProfit();
                analysisProfit.Sport_Id = this._sports.Single(x => x.Name == SportConst.NBA).Id;
                analysisProfit.TotalBets = totalBets;
                analysisProfit.Year = year;
                analysisProfit.Month = month;
                analysisProfit.Week = week;
                analysisProfit.FirstDayInWeek = TypeDateTime.GetFirstDayOfWeek(DateTime.Now);
                analysisProfit.Wins = wins;
                analysisProfit.Losses = losses;
                analysisProfit.Invested = invested;
                analysisProfit.Profit = profit;
                analysisProfit.ROI = analysisProfit.Invested > 0 ?Math.Round(analysisProfit.Profit / analysisProfit.Invested, 4) : 0;
            }

            base.AppDataRepository.SaveAnalysisProfit(new List<AnalysisProfit>() { analysisProfit });
        }

        #endregion
    }
}

using BetsKiller.DAL;
using BetsKiller.DAL.AppData;
using BetsKiller.Helper.Constants;
using BetsKiller.Helper.HTML;
using BetsKiller.Helper.Types;
using BetsKiller.Model;
using BetsKiller.ViewModel.Dashboard.ScheduleResults;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BetsKiller.BL.Dashboard.ScheduleResults
{
    public class GetData : ProcessBase
    {
        #region Private

        private ScheduleResultsViewModel _scheduleResultsViewModel;

        private IAppDataRepository _appDataRepository;

        #endregion

        #region Properties

        public ScheduleResultsViewModel ScheduleResultsViewModel
        {
            get { return this._scheduleResultsViewModel; }
        }

        #endregion

        #region Properties - override

        protected override string _successMessage
        {
            get { return "Schedule and results data retrived successfully."; }
        }

        protected override string _failMessage
        {
            get { return "Schedule and results data retrive failed."; }
        }

        #endregion

        #region Constructors

        public GetData()
        {
            this._appDataRepository = new AppDataRepository();
        }

        #endregion

        #region Methods

        protected override void Process()
        {
            this._scheduleResultsViewModel = new ScheduleResultsViewModel();

            // Get and parse NBA schedule and results
            this.GetParseScheduleResultsNBA();
        }

        #endregion

        #region Helper methods

        private void GetParseScheduleResultsNBA()
        {
            DateTime seasonStart = this.GetSeasonStart();

            List<TeamsNBA> nbaTeams = this._appDataRepository.GetAllTeamsNBA().ToList();
            List<ScheduleResultsNBA> scheduleResultsNba = this._appDataRepository.GetAllScheduleResultsNBA().Where(x => x.EventStartDateTime >= seasonStart).ToList();

            this._scheduleResultsViewModel.Teams = nbaTeams.Select(x => x.Abbreviation).ToList();
            this._scheduleResultsViewModel.TeamsGames = new List<ScheduleResultsTeamViewModel>();

            foreach (TeamsNBA team in nbaTeams)
            {
                ScheduleResultsTeamViewModel teamVM = new ScheduleResultsTeamViewModel();
                teamVM.TeamName = team.Abbreviation;
                teamVM.Games = new List<ScheduleResultsGameViewModel>();

                foreach (ScheduleResultsNBA scheduleResult in scheduleResultsNba.Where(x => x.Team.Id == team.Id || x.Opponent.Id == team.Id).OrderBy(x => x.TeamEventNumberInSeason))
                {
                    ScheduleResultsGameViewModel scheduleResultVM = new ScheduleResultsGameViewModel();
                    scheduleResultVM.Date = TypeDateTime.ParseDateTime(scheduleResult.EventStartDateTime);
                    scheduleResultVM.Location = scheduleResult.SiteName;

                    // Depends on which side is parsing team

                    if (scheduleResult.Team.Id == team.Id)
                    {
                        scheduleResultVM.Side = "Home";
                        scheduleResultVM.Opponent = scheduleResult.Opponent.Abbreviation;

                        if (scheduleResult.EventStatus != EventStatusConst.SCHEDULED)
                        {
                            string result = scheduleResult.TeamPointsScored + " - " + scheduleResult.OpponentPointsScored;

                            if (scheduleResult.TeamEventResult == "win")
                            {
                                scheduleResultVM.Result = new CustomHtmlElement(null, null, "color: green", result, null);
                            }
                            else
                            {
                                scheduleResultVM.Result = new CustomHtmlElement(null, null, "color: red", result, null);
                            }
                        }
                        else
                        {
                            scheduleResultVM.Result = new CustomHtmlElement(null, null, "color: orange", EventStatusConst.SCHEDULED, null);
                        }
                    }
                    else // scheduleResult.Opponent.Id == team.Id
                    {
                        scheduleResultVM.Side = "Away";
                        scheduleResultVM.Opponent = scheduleResult.Team.Abbreviation;

                        if (scheduleResult.EventStatus != EventStatusConst.SCHEDULED)
                        {
                            string result = scheduleResult.OpponentPointsScored + " - " + scheduleResult.TeamPointsScored;

                            if (scheduleResult.TeamEventResult == "win")
                            {
                                scheduleResultVM.Result = new CustomHtmlElement(null, null, "color: red", result, null);
                            }
                            else
                            {
                                scheduleResultVM.Result = new CustomHtmlElement(null, null, "color: green", result, null);
                            }
                        }
                        else
                        {
                            scheduleResultVM.Result = new CustomHtmlElement(null, null, "color: orange", EventStatusConst.SCHEDULED, null);
                        }
                    }
                    
                    teamVM.Games.Add(scheduleResultVM);
                }

                this._scheduleResultsViewModel.TeamsGames.Add(teamVM);
            }
        }

        private DateTime GetSeasonStart()
        {
            DateTime seasonStart;

            if (DateTime.Now < new DateTime(DateTime.Now.Year, 7, 1))
            {
                seasonStart = new DateTime(DateTime.Now.Year - 1, 7, 1);
            }
            else
            {
                seasonStart = new DateTime(DateTime.Now.Year, 7, 1);
            }

            return seasonStart;
        }

        #endregion
    }
}

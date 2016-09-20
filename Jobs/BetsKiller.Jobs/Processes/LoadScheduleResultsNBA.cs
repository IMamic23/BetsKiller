using BetsKiller.DAL;
using BetsKiller.DAL.AppData;
using BetsKiller.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BetsKiller.Jobs.Processes
{
    public class LoadScheduleResultsNBA : Load
    {
        #region Private

        private string _since;
        private string _until;

        private List<ScheduleResultsNBA> _scheduleResults;

        private List<TeamsNBA> _teamsNba;
        private string _season;

        #endregion

        #region Constructors

        public LoadScheduleResultsNBA(string season = null, string since = null, string until = null)
        {
            this._season = season;
            this._since = since;
            this._until = until;
        }

        #endregion

        #region Methods

        public void Start()
        {
            // Get teams NBA
            this._teamsNba = base.GetTeamsNBA();

            // Get schedule and results
            this.GetParseScheduleResults();

            // Save schedule and results in DB
            this.SaveScheduleResultsInDB();
        }

        #endregion

        #region Helper methods

        private void GetParseScheduleResults()
        {
            this._scheduleResults = new List<ScheduleResultsNBA>();

            foreach (TeamsNBA team in this._teamsNba)
            {
                // Get schedule and results
                BetsKiller.API.Erikberg.Methods.MethodTeamScheduleResults methodsScheduleResults = new BetsKiller.API.Erikberg.Methods.MethodTeamScheduleResults();
                List<BetsKiller.API.Erikberg.Entities.TeamScheduleResults> dataScheduleResults = methodsScheduleResults.Get(BetsKiller.API.Erikberg.Enums.SportEnum.nba, team.Name.NameErikberg, this._season, this._since, this._until, null);

                foreach (BetsKiller.API.Erikberg.Entities.TeamScheduleResults dataScheduleResult in dataScheduleResults.Where(x => x.TeamEventLocationType == "h"))
                {
                    ScheduleResultsNBA scheduleResult = new ScheduleResultsNBA();

                    scheduleResult.EventId = dataScheduleResult.EventId;
                    scheduleResult.EventStatus = dataScheduleResult.EventStatus;
                    scheduleResult.EventStartDateTime = dataScheduleResult.EventStartDateTime;
                    scheduleResult.EventSeasonType = dataScheduleResult.EventSeasonType;
                    scheduleResult.TeamEventNumberInSeason = dataScheduleResult.TeamEventNumberInSeason;
                    scheduleResult.TeamEventLocationType = dataScheduleResult.TeamEventLocationType;
                    scheduleResult.TeamEventResult = dataScheduleResult.TeamEventResult;
                    scheduleResult.TeamPointsScored = dataScheduleResult.TeamPointsScored;
                    scheduleResult.TeamEventsWon = dataScheduleResult.TeamEventsWon;
                    scheduleResult.TeamEventsLost = dataScheduleResult.TeamEventsLost;
                    scheduleResult.OpponentPointsScored = dataScheduleResult.OpponentPointsScored;
                    scheduleResult.OpponentEventsWon = dataScheduleResult.OpponentEventsWon;
                    scheduleResult.OpponentEventsLost = dataScheduleResult.OpponentEventsLost;
                    scheduleResult.SiteCapacity = dataScheduleResult.Site.Capacity;
                    scheduleResult.SiteSurface = dataScheduleResult.Site.Surface;
                    scheduleResult.SiteName = dataScheduleResult.Site.Name;
                    scheduleResult.SiteState = dataScheduleResult.Site.State;
                    scheduleResult.SiteCity = dataScheduleResult.Site.City;

                    TeamsNBA homeTeam = this._teamsNba.Where(x => x.Name.NameErikberg == dataScheduleResult.Team.TeamId).FirstOrDefault();
                    if (homeTeam != null)
                    {
                        scheduleResult.Team_Id = homeTeam.Id;
                    }

                    TeamsNBA awayTeam = this._teamsNba.Where(x => x.Name.NameErikberg == dataScheduleResult.Opponent.TeamId).FirstOrDefault();
                    if (awayTeam != null)
                    {
                        scheduleResult.Opponent_Id = awayTeam.Id;
                    }

                    this._scheduleResults.Add(scheduleResult);
                }

                //TODO: Erikberg free user too many requests
                Thread.Sleep(base.WAIT_TIME);
            }
        }

        private void SaveScheduleResultsInDB()
        {
            // Save schedule and results to the DB
            base.AppDataRepository.SaveScheduleResultsNBA(this._scheduleResults);
        }

        #endregion
    }
}

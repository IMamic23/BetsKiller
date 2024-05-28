using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BetsKiller.Helper.Constants;
using BetsKiller.Model;

namespace BetsKiller.Jobs.Processes
{
    public class LoadStandings : Load
    {
        #region Private

        private List<Standings> _standings;

        private List<Sport> _sports;
        private List<TeamsNBA> _nbaTeams;

        #endregion

        #region Methods

        public void Start()
        {
            this._standings = new List<Standings>();

            // Get sports and NBA teams
            this._sports = base.GetSports();
            this._nbaTeams = base.GetTeamsNBA();

            // Get NBA standings
            this.GetParseStandingsNBA();

            // Save standings in DB
            this.SaveStandingsInDB();
        }

        #endregion

        #region Helper methods

        private void GetParseStandingsNBA()
        {
            // Get standings
            var methodStandings = new API.Erikberg.Methods.MethodStandings();
            var dataStandings = methodStandings.Get(API.Erikberg.Enums.SportEnum.nba, base.GetCurrentDateErikberg());

            foreach (var dataStanding in dataStandings.Standing)
            {
                var standing = new Standings();
                standing.Rank = dataStanding.Rank;
                standing.OrdinalRank = dataStanding.OrdinalRank;
                standing.Won = dataStanding.Won;
                standing.Lost = dataStanding.Lost;
                standing.AwayWon = dataStanding.AwayWon;
                standing.AwayLost = dataStanding.AwayLost;
                standing.Conference = dataStanding.Conference;
                standing.ConferenceWon = dataStanding.ConferenceWon;
                standing.ConferenceLost = dataStanding.ConferenceLost;
                standing.Division = dataStanding.Division;
                standing.GamesBack = Convert.ToDecimal(dataStanding.GamesBack, CultureInfo.InvariantCulture);
                standing.GamesPlayed = dataStanding.GamesPlayed;
                standing.HomeWon = dataStanding.HomeWon;
                standing.HomeLost = dataStanding.HomeLost;
                standing.LastFive = dataStanding.LastFive;
                standing.LastTen = dataStanding.LastTen;
                standing.PlayoffSeed = dataStanding.PlayoffSeed;
                standing.PointDifferential = dataStanding.PointDifferential;
                standing.PointDifferentialPerGame = dataStanding.PointDifferentialPerGame;
                standing.PointsAgainst = dataStanding.PointsAgainst;
                standing.PointsFor = dataStanding.PointsFor;
                standing.PointsAllowedPerGame = dataStanding.PointsAllowedPerGame;
                standing.PointsScoredPerGame = dataStanding.PointsScoredPerGame;
                standing.StreakTotal = dataStanding.StreakTotal;
                standing.Streak = dataStanding.Streak;
                standing.StreakType = dataStanding.StreakType;
                standing.WinPercentage = dataStanding.WinPercentage;
                standing.Sport_Id = this._sports.Single(x => x.Name == SportConst.NBA).Id;
                standing.TeamNBA_Id = this._nbaTeams.Single(x => x.Name.NameErikberg == dataStanding.TeamId).Id;

                this._standings.Add(standing);
            }
        }

        private void SaveStandingsInDB()
        {
            // Clear standings
            base.AppDataRepository.ClearStandings();

            // Save standings
            base.AppDataRepository.SaveStandings(this._standings);
        }

        #endregion
    }
}

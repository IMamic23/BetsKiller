using System;
using System.Collections.Generic;
using System.Linq;
using BetsKiller.Model;

namespace BetsKiller.Jobs.Processes
{
    public class LoadPowerRankingsNBA : Load
    {
        #region Private

        private List<PowerRankingsNBA> _powerRankings;

        private List<TeamsNBA> _teamsNba;

        #endregion

        #region Methods

        public void Start()
        {
            // Get teams NBA
            this._teamsNba = base.GetTeamsNBA();

            // Get NBA power rankings
            this.GetParsePowerRankings();

            // Save power rankings in DB
            this.SavePowerRankingsInDB();
        }

        #endregion

        #region Helper methods

        private void GetParsePowerRankings()
        {
            this._powerRankings = new List<PowerRankingsNBA>();

            // Get power rankings
            var methodPowerRankings = new BetsKiller.API.NBAcom.Methods.MethodPowerRankings();
            var dataPowerRankings = methodPowerRankings.Get();

            foreach (var dataPowerRanking in dataPowerRankings)
            {
                var powerRanking = new PowerRankingsNBA();

                powerRanking.Rank = Convert.ToInt32(dataPowerRanking.Rank);
                powerRanking.RankLastWeek = Convert.ToInt32(dataPowerRanking.RankLastWeek);
                powerRanking.Score = dataPowerRanking.Score;
                powerRanking.Pace = dataPowerRanking.Pace;
                powerRanking.OffRtg = dataPowerRanking.OffRtg;
                powerRanking.DefRtg = dataPowerRanking.DefRtg;
                powerRanking.NetRtg = dataPowerRanking.NetRtg;
                powerRanking.Description = dataPowerRanking.Description;
                powerRanking.GamesThisWeek = dataPowerRanking.GamesThisWeek;

                powerRanking.Team_Id = this._teamsNba.Single(x => x.Name.NameNBAcom.ToLower() == dataPowerRanking.TeamName.ToLower()).Id;

                this._powerRankings.Add(powerRanking);
            }
        }

        private void SavePowerRankingsInDB()
        {
            // Clear power rankings
            base.AppDataRepository.ClearPowerRankingsNBA();

            // Save power rankings to the DB
            base.AppDataRepository.SavePowerRankingsNBA(this._powerRankings);
        }

        #endregion
    }
}

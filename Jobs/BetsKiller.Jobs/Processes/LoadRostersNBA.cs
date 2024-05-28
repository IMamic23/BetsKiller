using System.Collections.Generic;
using System.Threading;
using BetsKiller.Model;

namespace BetsKiller.Jobs.Processes
{
    public class LoadRostersNBA : Load
    {
        #region Private

        private List<RostersNBA> _rosters;

        private List<TeamsNBA> _teamsNba;

        #endregion

        #region Methods

        public void Start()
        {
            // Get teams NBA
            this._teamsNba = base.GetTeamsNBA();

            // Get rosters
            this.GetParseRosters();

            // Save rosters in DB
            this.SaveRostersInDB();
        }

        #endregion

        #region Helper methods

        private void GetParseRosters()
        {
            this._rosters = new List<RostersNBA>();

            // Get roster for each team
            foreach (TeamsNBA team in this._teamsNba)
            {
                BetsKiller.API.Erikberg.Methods.MethodRoster methodRoster = new BetsKiller.API.Erikberg.Methods.MethodRoster();
                BetsKiller.API.Erikberg.Entities.Roster dataRoster = methodRoster.Get(BetsKiller.API.Erikberg.Enums.SportEnum.nba, team.Name.NameErikberg, null);

                foreach (BetsKiller.API.Erikberg.Entities.Player player in dataRoster.Players)
                {
                    RostersNBA rosterPlayer = new RostersNBA();

                    rosterPlayer.FirstName = player.FirstName;
                    rosterPlayer.LastName = player.LastName;
                    rosterPlayer.DisplayName = player.DisplayName;
                    rosterPlayer.Birthday = player.Birthday;
                    rosterPlayer.Age = player.Age;
                    rosterPlayer.Birthplace = player.Birthplace;
                    rosterPlayer.HeightIn = (decimal)player.HeightIn;
                    rosterPlayer.HeightCm = (decimal)player.HeightCm;
                    rosterPlayer.HeightM = (decimal)player.HeightM;
                    rosterPlayer.HeightFormatted = player.HeightFormatted;
                    rosterPlayer.WeightLb = (decimal)player.WeightLb;
                    rosterPlayer.WeightKg = (decimal)player.WeightKg;
                    rosterPlayer.Position = player.Position;
                    rosterPlayer.UniformNumber = player.UniformNumber;

                    rosterPlayer.Team_Id = team.Id;

                    this._rosters.Add(rosterPlayer);
                }

                //TODO: Erikberg free user too many requests
                Thread.Sleep(base.WAIT_TIME);
            }
        }

        private void SaveRostersInDB()
        {
            // Clear rosters
            base.AppDataRepository.ClearRostersNBA();

            // Save rosters to the DB
            base.AppDataRepository.SaveRostersNBA(this._rosters);
        }

        #endregion
    }
}

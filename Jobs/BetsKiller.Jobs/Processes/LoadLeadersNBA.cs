using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BetsKiller.Model;

namespace BetsKiller.Jobs.Processes
{
    public class LoadLeadersNBA : Load
    {
        #region Private

        private List<LeadersNBA> _leaders;

        private List<LeadersNBACategories> _leaderCategoriesNba;
        private List<TeamsNBA> _teamsNba;

        #endregion

        #region Methods

        public void Start()
        {
            // Get leaders categories NBA and teams NBA
            this._leaderCategoriesNba = base.GetLeadersCategories();
            this._teamsNba = base.GetTeamsNBA();

            // Get leaders
            this.GetParseLeaders();

            // Save leaders in DB
            this.SaveLeadersInDB();
        }

        #endregion

        #region Helper methods

        private void GetParseLeaders()
        {
            this._leaders = new List<LeadersNBA>();

            // Get leaders for each category
            foreach (LeadersNBACategories leadersCategory in this._leaderCategoriesNba)
            {
                BetsKiller.API.Erikberg.Methods.MethodNBALeaders methodNbaLeaders = new BetsKiller.API.Erikberg.Methods.MethodNBALeaders();
                List<BetsKiller.API.Erikberg.Entities.NBALeader> dataLeaders = methodNbaLeaders.Get(BetsKiller.API.Erikberg.Enums.SportEnum.nba, leadersCategory.NameErikberg, null, null, null);

                foreach (BetsKiller.API.Erikberg.Entities.NBALeader dataLeader in dataLeaders)
                {
                    LeadersNBA leader = new LeadersNBA();

                    leader.FirstName = dataLeader.FirstName;
                    leader.LastName = dataLeader.LastName;
                    leader.DisplayName = dataLeader.DisplayName;
                    leader.Rank = dataLeader.Rank;
                    leader.Value = (decimal)dataLeader.Value;

                    leader.Category_Id = leadersCategory.Id;
                    leader.Team_Id = this._teamsNba.Single(x => x.Name.NameErikberg == dataLeader.Team.TeamId).Id;

                    this._leaders.Add(leader);
                }

                //TODO: Erikberg free user too many requests
                Thread.Sleep(base.WAIT_TIME);
            }
        }

        private void SaveLeadersInDB()
        {
            // Clear leaders
            base.AppDataRepository.ClearLeadersNBA();

            // Save leaders to the DB
            base.AppDataRepository.SaveLeadersNBA(this._leaders);
        }

        #endregion
    }
}

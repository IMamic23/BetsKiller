using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BetsKiller.Model;

namespace BetsKiller.Jobs.Processes
{
    public class LoadDraftNBA : Load
    {
        #region Private

        private List<DraftsNBA> _drafts;

        private List<TeamsNBA> _teamsNba;

        #endregion

        #region Methods

        public void Start()
        {
            // Get teams
            this._teamsNba = base.GetTeamsNBA(); 

            // Get drafts data
            this.GetParseDrafts();

            // Save drafts in DB
            this.SaveDraftsInDB();
        }

        #endregion

        #region Helper methods

        private void GetParseDrafts()
        {
            this._drafts = new List<DraftsNBA>();

            // Get drafts
            var methodNbaDraft = new BetsKiller.API.Erikberg.Methods.MethodNBADraft();
            var dataDrafts = methodNbaDraft.Get(BetsKiller.API.Erikberg.Enums.SportEnum.nba, null, null);

            foreach (var dataDraft in dataDrafts)
            {
                var draft = new DraftsNBA();

                draft.FirstName = dataDraft.Player.FirstName;
                draft.LastName = dataDraft.Player.LastName;
                draft.DisplayName = dataDraft.Player.DisplayName;
                draft.Birthday = dataDraft.Player.Birthday;
                draft.Age = dataDraft.Player.Age;
                draft.Birthplace = dataDraft.Player.Birthplace;
                draft.HeightIn = (decimal)dataDraft.Player.HeightIn;
                draft.HeightCm = (decimal)dataDraft.Player.HeightCm;
                draft.HeightM = (decimal)dataDraft.Player.HeightM;
                draft.HeightFormatted = dataDraft.Player.HeightFormatted;
                draft.WeightLb = (decimal)dataDraft.Player.WeightLb;
                draft.WeightKg = (decimal)dataDraft.Player.WeightKg;
                draft.Position = dataDraft.Player.Position;
                draft.UniformNumber = dataDraft.Player.UniformNumber;
                draft.Round = dataDraft.Round;
                draft.Pick = dataDraft.Pick;
                draft.OrdinalPick = dataDraft.OrdinalPick;
                draft.OverallPick = dataDraft.OverallPick;
                draft.OrdinalOverallPick = dataDraft.OrdinalOverallPick;
                draft.GamesPlayed = dataDraft.GamesPlayed;
                draft.Points = dataDraft.Points;
                draft.Assists = dataDraft.Assists;
                draft.DefensiveRebounds = dataDraft.DefensiveRebounds;
                draft.OffensiveRebounds = dataDraft.OffensiveRebounds;
                draft.Steals = dataDraft.Steals;
                draft.Blocks = dataDraft.Blocks;

                draft.Team_Id = this._teamsNba.Single(x => x.Name.NameErikberg == dataDraft.Team.TeamId).Id;

                this._drafts.Add(draft);
            }

            //TODO: Erikberg free user too many requests
            Thread.Sleep(base.WAIT_TIME);
        }

        private void SaveDraftsInDB()
        {
            // Clear drafts
            base.AppDataRepository.ClearDraftsNBA();

            // Save drafts to the DB
            base.AppDataRepository.SaveDraftsNBA(this._drafts);
        }

        #endregion
    }
}

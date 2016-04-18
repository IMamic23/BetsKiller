using BetsKiller.DAL;
using BetsKiller.DAL.AppData;
using BetsKiller.Helper.Constants;
using BetsKiller.Model;
using BetsKiller.ViewModel.Dashboard.Leaders;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.BL.Dashboard.Leaders
{
    public class GetData : ProcessBase
    {
        #region Private

        private LeadersViewModel _leadersViewModel;

        private IAppDataRepository _appDataRepository;

        #endregion

        #region Properties

        public LeadersViewModel LeadersViewModel
        {
            get { return _leadersViewModel; }
        }

        #endregion

        #region Properties - override

        protected override string _successMessage
        {
            get { return "Leaders data retrived successfully."; }
        }

        protected override string _failMessage
        {
            get { return "Leaders data retrive failed."; }
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
            this._leadersViewModel = new LeadersViewModel();

            // Get and parse NBA leaders
            this.GetParseLeadersNBA();
        }

        #endregion

        #region Helper methods

        private void GetParseLeadersNBA()
        {
            this._leadersViewModel = new LeadersViewModel();

            List<LeadersNBA> leaders = this._appDataRepository.GetAllLeadersNBA().ToList();

            this._leadersViewModel.PointsPerGame = this.ParseList(leaders.Where(x => x.Category.Name == LeadersNBACategoriesConst.PointsPerGame).OrderBy(x => x.Rank).Take(10));
            this._leadersViewModel.AssistsPerGame = this.ParseList(leaders.Where(x => x.Category.Name == LeadersNBACategoriesConst.AssistsPerGame).OrderBy(x => x.Rank).Take(10));
            this._leadersViewModel.ReboundsPerGame = this.ParseList(leaders.Where(x => x.Category.Name == LeadersNBACategoriesConst.ReboundsPerGame).OrderBy(x => x.Rank).Take(10));
            this._leadersViewModel.FieldGoal = this.ParseList(leaders.Where(x => x.Category.Name == LeadersNBACategoriesConst.FieldGoal).OrderBy(x => x.Rank).Take(10));
            this._leadersViewModel.FreeThrow = this.ParseList(leaders.Where(x => x.Category.Name == LeadersNBACategoriesConst.FreeThrow).OrderBy(x => x.Rank).Take(10));
            this._leadersViewModel.ThreePointFieldGoal = this.ParseList(leaders.Where(x => x.Category.Name == LeadersNBACategoriesConst.ThreePointFieldGoal).OrderBy(x => x.Rank).Take(10));
            this._leadersViewModel.OffensiveReboundsPerGame = this.ParseList(leaders.Where(x => x.Category.Name == LeadersNBACategoriesConst.OffensiveReboundsPerGame).OrderBy(x => x.Rank).Take(10));
            this._leadersViewModel.DefensiveReboundsPerGame = this.ParseList(leaders.Where(x => x.Category.Name == LeadersNBACategoriesConst.DefensiveReboundsPerGame).OrderBy(x => x.Rank).Take(10));
            this._leadersViewModel.BlocksPerGame = this.ParseList(leaders.Where(x => x.Category.Name == LeadersNBACategoriesConst.BlocksPerGame).OrderBy(x => x.Rank).Take(10));
            this._leadersViewModel.StealsPerGame = this.ParseList(leaders.Where(x => x.Category.Name == LeadersNBACategoriesConst.StealsPerGame).OrderBy(x => x.Rank).Take(10));
            this._leadersViewModel.AssistsToTurnovers = this.ParseList(leaders.Where(x => x.Category.Name == LeadersNBACategoriesConst.AssistsToTurnovers).OrderBy(x => x.Rank).Take(10));
            this._leadersViewModel.StealsToTurnovers = this.ParseList(leaders.Where(x => x.Category.Name == LeadersNBACategoriesConst.StealsToTurnovers).OrderBy(x => x.Rank).Take(10));
            this._leadersViewModel.MinutesPerGame = this.ParseList(leaders.Where(x => x.Category.Name == LeadersNBACategoriesConst.MinutesPerGame).OrderBy(x => x.Rank).Take(10));
            this._leadersViewModel.GamesPlayed = this.ParseList(leaders.Where(x => x.Category.Name == LeadersNBACategoriesConst.GamesPlayed).OrderBy(x => x.Rank).Take(10), true);
            this._leadersViewModel.DoubleDoubles = this.ParseList(leaders.Where(x => x.Category.Name == LeadersNBACategoriesConst.DoubleDoubles).OrderBy(x => x.Rank).Take(10), true);
            this._leadersViewModel.TripleDoubles = this.ParseList(leaders.Where(x => x.Category.Name == LeadersNBACategoriesConst.TripleDoubles).OrderBy(x => x.Rank).Take(10), true);
        }

        private List<LeadersCategoryPlayerViewModel> ParseList(IEnumerable<LeadersNBA> listLeaders, bool wholeNumber = false)
        {
            List<LeadersCategoryPlayerViewModel> listViewModel = new List<LeadersCategoryPlayerViewModel>();

            foreach (LeadersNBA leader in listLeaders)
            {
                LeadersCategoryPlayerViewModel leaderViewModel = new LeadersCategoryPlayerViewModel();

                leaderViewModel.Rank = leader.Rank.ToString();
                leaderViewModel.Player = leader.FirstName + " " + leader.LastName;
                leaderViewModel.Team = leader.Team.Abbreviation;

                if (!wholeNumber)
                {
                    leaderViewModel.Value = Math.Round(leader.Value, 2).ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    leaderViewModel.Value = Math.Round(leader.Value, 0).ToString();
                }

                listViewModel.Add(leaderViewModel);
            }

            return listViewModel;
        }

        #endregion
    }
}

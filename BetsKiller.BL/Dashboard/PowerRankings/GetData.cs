using BetsKiller.DAL;
using BetsKiller.DAL.AppData;
using BetsKiller.Helper.HTML;
using BetsKiller.Model;
using BetsKiller.ViewModel.Dashboard.PowerRankings;
using System.Collections.Generic;
using System.Linq;

namespace BetsKiller.BL.Dashboard.PowerRankings
{
    public class GetData : ProcessBase
    {
        #region Private

        private PowerRankingsViewModel _powerRankingsViewModel;

        private IAppDataRepository _appDataRepository;

        #endregion

        #region Properties

        public PowerRankingsViewModel PowerRankingsViewModel
        {
            get { return this._powerRankingsViewModel; }
        }

        #endregion

        #region Properties - override

        protected override string _successMessage
        {
            get { return "Power rankings data retrived successfully."; }
        }

        protected override string _failMessage
        {
            get { return "Power rankings data retrive failed."; }
        }

        #endregion

        #region Constructors

        public GetData()
        {
            this._powerRankingsViewModel = new PowerRankingsViewModel();

            this._appDataRepository = new AppDataRepository();
        }

        #endregion

        #region Methods

        protected override void Process()
        {
            // Get and parse NBA power rankings
            this.GetParsePowerRankingsNBA();
        }

        #endregion

        #region Helper

        private void GetParsePowerRankingsNBA()
        {
            List<TeamsNBA> nbaTeams = this._appDataRepository.GetAllTeamsNBA().ToList();
            List<PowerRankingsNBA> powerRankings = this._appDataRepository.GetAllPowerRankingsNBA().ToList();

            this._powerRankingsViewModel.Teams = new List<PowerRankingsTeamViewModel>();

            foreach (TeamsNBA team in nbaTeams)
            {
                PowerRankingsNBA powerRanking = powerRankings.Single(x => x.Team.Id == team.Id);

                PowerRankingsTeamViewModel teamVM = new PowerRankingsTeamViewModel();
                teamVM.TeamName = team.Abbreviation;
                teamVM.Rank = new CustomHtmlElement(null, null, "color: red", "color: green").GetElementDifference(powerRanking.Rank.Value, powerRanking.RankLastWeek.Value, true, 0);
                teamVM.RankLastWeek = powerRanking.RankLastWeek.ToString();
                teamVM.Score = powerRanking.Score.Trim('(').Trim(')');
                teamVM.Pace = powerRanking.Pace;
                teamVM.OffRtg = powerRanking.OffRtg;
                teamVM.DefRtg = powerRanking.DefRtg;
                teamVM.NetRtg = powerRanking.NetRtg;
                teamVM.GamesThisWeek = powerRanking.GamesThisWeek;

                this._powerRankingsViewModel.Teams.Add(teamVM);
            }
        }

        #endregion
    }
}

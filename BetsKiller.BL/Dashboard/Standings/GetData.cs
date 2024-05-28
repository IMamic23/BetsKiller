using BetsKiller.DAL;
using BetsKiller.DAL.AppData;
using BetsKiller.Helper.Constants;
using BetsKiller.ViewModel.Dashboard.Standings;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BetsKiller.BL.Dashboard.Standings
{
    public class GetData : ProcessBase
    {
        #region Private

        private StandingsViewModel _standingsViewModel;

        private IAppDataRepository _appDataRepository;

        #endregion

        #region Properties

        public StandingsViewModel StandingsViewModel
        {
            get { return this._standingsViewModel; }
        }

        #endregion

        #region Properties - override

        protected override string _successMessage
        {
            get { return "Standings data retrived successfully."; }
        }

        protected override string _failMessage
        {
            get { return "Standings data retrive failed."; }
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
            this._standingsViewModel = new StandingsViewModel();

            // Get and parse NBA standings
            this.GetParseStandings();
        }

        #endregion

        #region Helper methods

        private void GetParseStandings()
        {
            this._standingsViewModel.Conferences = new List<StandingsConferenceViewModel>();

            List<BetsKiller.Model.Standings> standings = this._appDataRepository.GetAllStandings().ToList();

            this._standingsViewModel.Conferences.Add(new StandingsConferenceViewModel()
            {
                ConferenceName = StandingsConferenceConst.West,
                ConferenceLabel = StandingsConferenceConst.WestLabel,
                ConferenceStyle = StandingsConferenceConst.WestStyle,
                Standings = this.ParseList(standings.Where(x => x.Conference == StandingsConferenceConst.West))
            });

            this._standingsViewModel.Conferences.Add(new StandingsConferenceViewModel()
            {
                ConferenceName = StandingsConferenceConst.East,
                ConferenceLabel = StandingsConferenceConst.EastLabel,
                ConferenceStyle = StandingsConferenceConst.EastStyle,
                Standings = this.ParseList(standings.Where(x => x.Conference == StandingsConferenceConst.East))
            });
        }

        private List<StandingsTeamViewModel> ParseList(IEnumerable<BetsKiller.Model.Standings> standings)
        {
            List<StandingsTeamViewModel> standingsVM = new List<StandingsTeamViewModel>();

            foreach (BetsKiller.Model.Standings standing in standings)
            {
                StandingsTeamViewModel standingVM = new StandingsTeamViewModel();
                standingVM.TeamName = standing.TeamNBA.Abbreviation;
                standingVM.Rank = standing.Rank.ToString();
                standingVM.Won = standing.Won.ToString();
                standingVM.Lost = standing.Lost.ToString();
                standingVM.AwayWon = standing.AwayWon.ToString();
                standingVM.AwayLost = standing.AwayLost.ToString();
                standingVM.ConferenceWon = standing.ConferenceWon.ToString();
                standingVM.ConferenceLost = standing.ConferenceLost.ToString();
                standingVM.Division = standing.Division;
                standingVM.GamesBack = Math.Round(standing.GamesBack, 2).ToString(CultureInfo.InvariantCulture);
                standingVM.GamesPlayed = standing.GamesPlayed.ToString();
                standingVM.HomeWon = standing.HomeWon.ToString();
                standingVM.HomeLost = standing.HomeLost.ToString();
                standingVM.LastFive = standing.LastFive;
                standingVM.LastTen = standing.LastTen;
                standingVM.PlayoffSeed = standing.PlayoffSeed.ToString();
                standingVM.PointDifferential = standing.PointDifferential.ToString();
                standingVM.PointDifferentialPerGame = standing.PointDifferentialPerGame;
                standingVM.PointsAgainst = standing.PointsAgainst.ToString();
                standingVM.PointsFor = standing.PointsFor.ToString();
                standingVM.PointsAllowedPerGame = standing.PointsAllowedPerGame;
                standingVM.PointsScoredPerGame = standing.PointsScoredPerGame;
                standingVM.Streak = standing.Streak;
                standingVM.WinPercentage = standing.WinPercentage;

                standingsVM.Add(standingVM);
            }

            return standingsVM;
        }

        #endregion
    }
}

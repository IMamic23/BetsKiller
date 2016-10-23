using BetsKiller.DAL;
using BetsKiller.DAL.AppData;
using BetsKiller.Helper.Types;
using BetsKiller.ViewModel.Dashboard.BetsTracker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.BL.Dashboard.BetsTracker
{
    public class GetProfile : ProcessBase
    {
        #region Private

        private BetsTrackerBettingProfileDataViewModel _betsTrackerBettingProfileDataViewModel;

        private IAppDataRepository _appDataRepository;

        private string _profileId;

        #endregion

        #region Properties

        public BetsTrackerBettingProfileDataViewModel BetsTrackerBettingProfileDataViewModel
        {
            get { return this._betsTrackerBettingProfileDataViewModel; }
        }

        #endregion

        #region Properties - override

        protected override string _successMessage
        {
            get { return "Bets tracker betting profile data retrived successfully."; }
        }

        protected override string _failMessage
        {
            get { return "Bets tracker betting profile data retrive failed."; }
        }

        #endregion

        #region Constructors

        public GetProfile(string profileId)
        {
            this._betsTrackerBettingProfileDataViewModel = new BetsTrackerBettingProfileDataViewModel();

            this._appDataRepository = new AppDataRepository();

            this._profileId = profileId;
        }

        #endregion

        #region Methods

        protected override void Process()
        {
            this.GetParseBettingProfile();
        }

        #endregion

        #region Helper methods

        private void GetParseBettingProfile()
        {
            // Get bets
            this.BetsTrackerBettingProfileDataViewModel.Bets = new List<BetsTrackerProfileBetViewModel>()
            {
                new BetsTrackerProfileBetViewModel()
                {
                    Type = "single",
                    Id = "99",
                    Timestamp = TypeDateTime.ParseDateTime(DateTime.Now),
                    Game = "Game",
                    BetAmount = "$0.00",
                    Odds = "0,00",
                    BetValue = "U000,0",
                    BetStatus = "XXX",
                    ProfitLoss = "$0.00",
                    Site = "XXX"
                }
            };

            // Get info boxes values
            this.BetsTrackerBettingProfileDataViewModel.InfoBoxesValues = new List<string>()
            {
                "99",
                "99",
                "99",
                "99",
                "99",
                "99"
            };

            // Get statistics
            this.BetsTrackerBettingProfileDataViewModel.Statistics = new List<BetsTrackerProfileStatisticViewModel>()
            {
                new BetsTrackerProfileStatisticViewModel()
                {
                    ActiveTab = true,
                    Name = "daily",
                    Label = "Daily",
                    Data = new List<BetsTrackerProfileStatisticElementViewModel>()
                    {
                        new BetsTrackerProfileStatisticElementViewModel()
                        {
                            Date = "99/99/9999",
                            Wins = "99",
                            Losses = "99",
                            Invested = "99",
                            Profit = "99"
                        }
                    }
                },
                new BetsTrackerProfileStatisticViewModel()
                {
                    ActiveTab = false,
                    Name = "weekly",
                    Label = "Weekly",
                    Data = new List<BetsTrackerProfileStatisticElementViewModel>()
                    {
                        new BetsTrackerProfileStatisticElementViewModel()
                        {
                            Date = "99/99/9999",
                            Wins = "99",
                            Losses = "99",
                            Invested = "99",
                            Profit = "99"
                        }
                    }
                },
                new BetsTrackerProfileStatisticViewModel()
                {
                    ActiveTab = false,
                    Name = "monthly",
                    Label = "Monthly",
                    Data = new List<BetsTrackerProfileStatisticElementViewModel>()
                    {
                        new BetsTrackerProfileStatisticElementViewModel()
                        {
                            Date = "99/99/9999",
                            Wins = "99",
                            Losses = "99",
                            Invested = "99",
                            Profit = "99"
                        }
                    }
                }
            };

            // Get charts
            this.BetsTrackerBettingProfileDataViewModel.Charts = new List<BetsTrackerProfileChartViewModel>()
            {
                new BetsTrackerProfileChartViewModel()
                {
                    ActiveTab = false,
                    Name = "overview",
                    Label = "Overview",
                    TotalValue = "25",
                    MoneylineValue = "25",
                    Handicap = "25",
                    Parlay = "25"
                },
                new BetsTrackerProfileChartViewModel()
                {
                    ActiveTab = false,
                    Name = "chart_last_30_days",
                    Label = "Last 30 days",
                    TotalValue = "25",
                    MoneylineValue = "25",
                    Handicap = "25",
                    Parlay = "25"
                },
                new BetsTrackerProfileChartViewModel()
                {
                    ActiveTab = false,
                    Name = "bet_type",
                    Label = "Bet-type",
                    TotalValue = "25",
                    MoneylineValue = "25",
                    Handicap = "25",
                    Parlay = "25"
                },
                new BetsTrackerProfileChartViewModel()
                {
                    ActiveTab = false,
                    Name = "sport",
                    Label = "Sport",
                    TotalValue = "25",
                    MoneylineValue = "25",
                    Handicap = "25",
                    Parlay = "25"
                }
            };
        }

        #endregion
    }
}

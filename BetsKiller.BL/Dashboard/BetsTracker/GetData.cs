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
    public class GetData : ProcessBase
    {
        #region Private

        private BetsTrackerViewModel _betsTrackerViewModel;

        private IAppDataRepository _appDataRepository;

        #endregion

        #region Properties

        public BetsTrackerViewModel BetsTrackerViewModel
        {
            get { return this._betsTrackerViewModel; }
        }

        #endregion

        #region Properties - override

        protected override string _successMessage
        {
            get { return "Bets tracker data retrived successfully."; }
        }

        protected override string _failMessage
        {
            get { return "Bets tracker data retrive failed."; }
        }

        #endregion

        #region Constructors

        public GetData()
        {
            this._betsTrackerViewModel = new BetsTrackerViewModel();

            this._appDataRepository = new AppDataRepository();
        }

        #endregion

        #region Methods

        protected override void Process()
        {
            // Analysis profit NBA
            this.GetParseAnalysisProfitNBA();

            // Betting profiles NBA
            this.GetParseBettingProfilesNBA();

            // Performance NBA
            this.GetParsePerformanceNBA();

            // Load bets for profile ALL
            this.GetParseDefaultProfileDataNBA();
        }

        #endregion

        #region Helper methods

        private void GetParseAnalysisProfitNBA()
        {
            this.BetsTrackerViewModel.AnalysisProfit = new List<BetsTrackerAnalysisProfitViewModel>()
            {
                new BetsTrackerAnalysisProfitViewModel()
                {
                    Name = "all_time",
                    Label = "All Time",
                    TotalBets = "0",
                    Wins = "0",
                    Losses = "0",
                    AverageOdds = "0",
                    ROI = "0",
                    TotalProfit = "0"
                },
                new BetsTrackerAnalysisProfitViewModel()
                {
                    Name = "last_30_days",
                    Label = "Last 30 Days",
                    TotalBets = "1",
                    Wins = "1",
                    Losses = "1",
                    AverageOdds = "1",
                    ROI = "1",
                    TotalProfit = "1"
                }
            };
        }

        private void GetParseBettingProfilesNBA()
        {
            this.BetsTrackerViewModel.BettingProfiles = new List<BetsTrackerBettingProfileViewModel>()
            {
                new BetsTrackerBettingProfileViewModel()
                {
                    ActiveTab = true,
                    Id = "0",
                    Label = "All",
                    OpenBets = "8"
                },
                new BetsTrackerBettingProfileViewModel()
                {
                    ActiveTab = false,
                    Id = "1",
                    Label = "16.10.2016.",
                    OpenBets = "5",
                },
                new BetsTrackerBettingProfileViewModel()
                {
                    ActiveTab = false,
                    Id = "2",
                    Label = "15.10.2016.",
                    OpenBets = "3"
                },
                new BetsTrackerBettingProfileViewModel()
                {
                    ActiveTab = false,
                    Id = "2",
                    Label = "14.10.2016.",
                    OpenBets = null
                }
            };
        }

        private void GetParsePerformanceNBA()
        {
            this.BetsTrackerViewModel.Performances = new List<BetsTrackerPerformanceViewModel>()
            {
                new BetsTrackerPerformanceViewModel()
                {
                    Profile = "All",
                    ROI = "0",
                    Profit = "0"
                },
                new BetsTrackerPerformanceViewModel()
                {
                    Profile = "16.10.2016",
                    ROI = "1",
                    Profit = "1"
                },
                new BetsTrackerPerformanceViewModel()
                {
                    Profile = "15.10.2016",
                    ROI = "2",
                    Profit = "2"
                },
                new BetsTrackerPerformanceViewModel()
                {
                    Profile = "14.10.2016",
                    ROI = "3",
                    Profit = "3"
                }
            };
        }

        private void GetParseDefaultProfileDataNBA()
        {
            this.BetsTrackerViewModel.DefaultProfile = new BetsTrackerBettingProfileDataViewModel();

            // Get bets
            this.BetsTrackerViewModel.DefaultProfile.Bets = new List<BetsTrackerProfileBetViewModel>()
            {
                new BetsTrackerProfileBetViewModel()
                {
                    Type = "single",
                    Id = "1",
                    Timestamp = TypeDateTime.ParseDateTime(DateTime.Now),
                    Game = "Hawks - Celtics",
                    BetAmount = "$7.45",
                    Odds = "1,91",
                    BetValue = "U202,5",
                    BetStatus = "WIN",
                    ProfitLoss = "$7.00",
                    Site = "Home"
                }
            };

            // Get statistics
            this.BetsTrackerViewModel.DefaultProfile.Statistics = new List<BetsTrackerProfileStatisticViewModel>()
            {
                new BetsTrackerProfileStatisticViewModel()
                {
                    ActiveTab = true,
                    Name = "daily",
                    Label = "Daily",
                    Date = "16/10/2016",
                    Wins = "1",
                    Losses = "1",
                    Invested = "1",
                    Profit = "1"
                },
                new BetsTrackerProfileStatisticViewModel()
                {
                    ActiveTab = false,
                    Name = "weekly",
                    Label = "Weekly",
                    Date = "15/10/2016",
                    Wins = "2",
                    Losses = "2",
                    Invested = "2",
                    Profit = "2"
                },
                new BetsTrackerProfileStatisticViewModel()
                {
                    ActiveTab = false,
                    Name = "monthly",
                    Label = "Monthly",
                    Date = "14/10/2016",
                    Wins = "3",
                    Losses = "3",
                    Invested = "3",
                    Profit = "3"
                }
            };

            // Get charts
            this.BetsTrackerViewModel.DefaultProfile.Charts = new List<BetsTrackerProfileChartViewModel>()
            {
                new BetsTrackerProfileChartViewModel()
                {
                    ActiveTab = true,
                    Name = "overview",
                    Label = "Overview",
                    Value = "Overview chart"
                },
                new BetsTrackerProfileChartViewModel()
                {
                    ActiveTab = false,
                    Name = "chart_last_30_days",
                    Label = "Last 30 days",
                    Value = "Last 30 days chart"
                },
                new BetsTrackerProfileChartViewModel()
                {
                    ActiveTab = false,
                    Name = "bet_type",
                    Label = "Bet-type",
                    Value = "Bet-type chart"
                },
                new BetsTrackerProfileChartViewModel()
                {
                    ActiveTab = false,
                    Name = "sport",
                    Label = "Sport",
                    Value = "Sport chart"
                }
            };
        }

        #endregion
    }
}

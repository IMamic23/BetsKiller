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
            // Game scores NBA
            this.GetParseGameScoresNBA();

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

        private void GetParseGameScoresNBA()
        {
            this.BetsTrackerViewModel.GameScores = new List<BetsTrackerGameScoreViewModel>()
            {
                new BetsTrackerGameScoreViewModel()
                {
                    Date = "15/10",
                    Status = new Helper.HTML.CustomHtmlElement(null, null, "color: red", "Final", null),
                    AwayTeam = "Cleveland",
                    AwayScore = "102",
                    HomeTeam = "Indiana",
                    HomeScore = "99"
                },
                new BetsTrackerGameScoreViewModel()
                {
                    Date = "15/10",
                    Status = new Helper.HTML.CustomHtmlElement(null, null, "color: red", "Final", null),
                    AwayTeam = "Utah",
                    AwayScore = "97",
                    HomeTeam = "Portland",
                    HomeScore = "103"
                },
                new BetsTrackerGameScoreViewModel()
                {
                    Date = "16/10",
                    Status = new Helper.HTML.CustomHtmlElement(null, null, "color: red", "Final", null),
                    AwayTeam = "LAC",
                    AwayScore = "108",
                    HomeTeam = "New Orleans",
                    HomeScore = "99"
                },
                new BetsTrackerGameScoreViewModel()
                {
                    Date = "16/10",
                    Status = new Helper.HTML.CustomHtmlElement(null, null, "color: red", "Final", null),
                    AwayTeam = "Dallas",
                    AwayScore = "87",
                    HomeTeam = "Utah",
                    HomeScore = "89"
                },
                new BetsTrackerGameScoreViewModel()
                {
                    Date = "17/10",
                    Status = new Helper.HTML.CustomHtmlElement(null, null, "color: red", "Final", null),
                    AwayTeam = "Denver",
                    AwayScore = "118",
                    HomeTeam = "Golden State",
                    HomeScore = "128"
                },
                new BetsTrackerGameScoreViewModel()
                {
                    Date = "17/10",
                    Status = new Helper.HTML.CustomHtmlElement(null, null, "color: red", "Final", null),
                    AwayTeam = "Chicago",
                    AwayScore = "88",
                    HomeTeam = "Washington",
                    HomeScore = "104"
                },
                new BetsTrackerGameScoreViewModel()
                {
                    Date = "17/10",
                    Status = new Helper.HTML.CustomHtmlElement(null, null, "color: red", "Final", null),
                    AwayTeam = "LAL",
                    AwayScore = "98",
                    HomeTeam = "Atlanta",
                    HomeScore = "104"
                },
                new BetsTrackerGameScoreViewModel()
                {
                    Date = "17/10",
                    Status = new Helper.HTML.CustomHtmlElement(null, null, "color: red", "Final", null),
                    AwayTeam = "Boston",
                    AwayScore = "103",
                    HomeTeam = "Cleveland",
                    HomeScore = "106"
                }
            };
        }

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
                    OpenBets = "8",
                    LastBetsStatus = "-1,0,1,1,0,0,-1,1,0,1"
                },
                new BetsTrackerBettingProfileViewModel()
                {
                    ActiveTab = false,
                    Id = "1",
                    Label = "16.10.2016.",
                    OpenBets = "5",
                    LastBetsStatus = "-1,0,1,1,0,0,-1,1,0,1"
                },
                new BetsTrackerBettingProfileViewModel()
                {
                    ActiveTab = false,
                    Id = "2",
                    Label = "15.10.2016.",
                    OpenBets = "3",
                    LastBetsStatus = "-1,0,1,1,0,0,-1,1,0,1"
                },
                new BetsTrackerBettingProfileViewModel()
                {
                    ActiveTab = false,
                    Id = "3",
                    Label = "14.10.2016.",
                    OpenBets = null,
                    LastBetsStatus = "-1,0,1,1,0,0,-1,1,0,1"
                },
                new BetsTrackerBettingProfileViewModel()
                {
                    ActiveTab = true,
                    Id = "4",
                    Label = "13.10.2016.",
                    OpenBets = "8",
                    LastBetsStatus = "-1,0,1,1,0,0,-1,1,0,1"
                },
                new BetsTrackerBettingProfileViewModel()
                {
                    ActiveTab = false,
                    Id = "5",
                    Label = "12.10.2016.",
                    OpenBets = "5",
                    LastBetsStatus = "-1,0,1,1,0,0,-1,1,0,1"
                },
                new BetsTrackerBettingProfileViewModel()
                {
                    ActiveTab = false,
                    Id = "6",
                    Label = "11.10.2016.",
                    OpenBets = "3",
                    LastBetsStatus = "-1,0,1,1,0,0,-1,1,0,1"
                },
                new BetsTrackerBettingProfileViewModel()
                {
                    ActiveTab = false,
                    Id = "7",
                    Label = "10.10.2016.",
                    OpenBets = null,
                    LastBetsStatus = "-1,0,1,1,0,0,-1,1,0,1"
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
                },
                new BetsTrackerProfileBetViewModel()
                {
                    Type = "single",
                    Id = "2",
                    Timestamp = TypeDateTime.ParseDateTime(DateTime.Now),
                    Game = "Hawks - Celtics",
                    BetAmount = "$7.45",
                    Odds = "1,91",
                    BetValue = "U202,5",
                    BetStatus = "WIN",
                    ProfitLoss = "$7.00",
                    Site = "Home"
                },
                new BetsTrackerProfileBetViewModel()
                {
                    Type = "single",
                    Id = "3",
                    Timestamp = TypeDateTime.ParseDateTime(DateTime.Now),
                    Game = "Hawks - Celtics",
                    BetAmount = "$7.45",
                    Odds = "1,91",
                    BetValue = "U202,5",
                    BetStatus = "WIN",
                    ProfitLoss = "$7.00",
                    Site = "Home"
                },
                new BetsTrackerProfileBetViewModel()
                {
                    Type = "single",
                    Id = "4",
                    Timestamp = TypeDateTime.ParseDateTime(DateTime.Now),
                    Game = "Hawks - Celtics",
                    BetAmount = "$7.45",
                    Odds = "1,91",
                    BetValue = "U202,5",
                    BetStatus = "WIN",
                    ProfitLoss = "$7.00",
                    Site = "Home"
                },
                new BetsTrackerProfileBetViewModel()
                {
                    Type = "single",
                    Id = "5",
                    Timestamp = TypeDateTime.ParseDateTime(DateTime.Now),
                    Game = "Hawks - Celtics",
                    BetAmount = "$7.45",
                    Odds = "1,91",
                    BetValue = "U202,5",
                    BetStatus = "WIN",
                    ProfitLoss = "$7.00",
                    Site = "Home"
                },
                new BetsTrackerProfileBetViewModel()
                {
                    Type = "single",
                    Id = "6",
                    Timestamp = TypeDateTime.ParseDateTime(DateTime.Now),
                    Game = "Hawks - Celtics",
                    BetAmount = "$7.45",
                    Odds = "1,91",
                    BetValue = "U202,5",
                    BetStatus = "WIN",
                    ProfitLoss = "$7.00",
                    Site = "Home"
                },
                new BetsTrackerProfileBetViewModel()
                {
                    Type = "single",
                    Id = "7",
                    Timestamp = TypeDateTime.ParseDateTime(DateTime.Now),
                    Game = "Hawks - Celtics",
                    BetAmount = "$7.45",
                    Odds = "1,91",
                    BetValue = "U202,5",
                    BetStatus = "WIN",
                    ProfitLoss = "$7.00",
                    Site = "Home"
                },
                new BetsTrackerProfileBetViewModel()
                {
                    Type = "single",
                    Id = "8",
                    Timestamp = TypeDateTime.ParseDateTime(DateTime.Now),
                    Game = "Hawks - Celtics",
                    BetAmount = "$7.45",
                    Odds = "1,91",
                    BetValue = "U202,5",
                    BetStatus = "WIN",
                    ProfitLoss = "$7.00",
                    Site = "Home"
                },
                new BetsTrackerProfileBetViewModel()
                {
                    Type = "single",
                    Id = "9",
                    Timestamp = TypeDateTime.ParseDateTime(DateTime.Now),
                    Game = "Hawks - Celtics",
                    BetAmount = "$7.45",
                    Odds = "1,91",
                    BetValue = "U202,5",
                    BetStatus = "WIN",
                    ProfitLoss = "$7.00",
                    Site = "Home"
                },
                new BetsTrackerProfileBetViewModel()
                {
                    Type = "single",
                    Id = "10",
                    Timestamp = TypeDateTime.ParseDateTime(DateTime.Now),
                    Game = "Hawks - Celtics",
                    BetAmount = "$7.45",
                    Odds = "1,91",
                    BetValue = "U202,5",
                    BetStatus = "WIN",
                    ProfitLoss = "$7.00",
                    Site = "Home"
                },
                new BetsTrackerProfileBetViewModel()
                {
                    Type = "single",
                    Id = "11",
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

            // Get info boxes values
            this.BetsTrackerViewModel.DefaultProfile.InfoBoxesValues = new List<string>()
            {
                "88",
                "88",
                "88",
                "88",
                "88",
                "88"
            };

            // Get statistics
            this.BetsTrackerViewModel.DefaultProfile.Statistics = new List<BetsTrackerProfileStatisticViewModel>()
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
                            Date = "1/1/1",
                            Wins = "1",
                            Losses = "1",
                            Invested = "1",
                            Profit = "1"
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
                            Date = "2/2/2",
                            Wins = "2",
                            Losses = "2",
                            Invested = "2",
                            Profit = "2"
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
                            Date = "3/3/3",
                            Wins = "3",
                            Losses = "3",
                            Invested = "3",
                            Profit = "3"
                        }
                    }
                }
            };

            // Get charts
            this.BetsTrackerViewModel.DefaultProfile.Charts = new List<BetsTrackerProfileChartViewModel>()
            {
                new BetsTrackerProfileChartViewModel()
                {
                    ActiveTab = false,
                    Name = "overview",
                    Label = "Overview",
                    TotalValue = "20",
                    MoneylineValue = "30",
                    Handicap = "10",
                    Parlay = "40"
                },
                new BetsTrackerProfileChartViewModel()
                {
                    ActiveTab = false,
                    Name = "chart_last_30_days",
                    Label = "Last 30 days",
                    TotalValue = "25",
                    MoneylineValue = "25",
                    Handicap = "30",
                    Parlay = "20"
                },
                new BetsTrackerProfileChartViewModel()
                {
                    ActiveTab = false,
                    Name = "bet_type",
                    Label = "Bet-type",
                    TotalValue = "34",
                    MoneylineValue = "16",
                    Handicap = "24",
                    Parlay = "26"
                },
                new BetsTrackerProfileChartViewModel()
                {
                    ActiveTab = false,
                    Name = "sport",
                    Label = "Sport",
                    TotalValue = "17",
                    MoneylineValue = "33",
                    Handicap = "8",
                    Parlay = "42"
                }
            };
        }

        #endregion
    }
}

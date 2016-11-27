using BetsKiller.DAL;
using BetsKiller.DAL.AppData;
using BetsKiller.Helper.Constants;
using BetsKiller.Helper.Operations;
using BetsKiller.Helper.Types;
using BetsKiller.Model;
using BetsKiller.ViewModel.Dashboard.BetsTracker;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
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

        private string _username;

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

        public GetData(string username)
        {
            this._betsTrackerViewModel = new BetsTrackerViewModel();

            this._appDataRepository = new AppDataRepository();

            this._username = username;
        }

        #endregion

        #region Methods

        protected override void Process()
        {
            // Game scores NBA
            this.GetParseGameScoresNBA();

            // Get all user bets by username
            // TODO: mozda ce trebati napraviti u DAL-u include prema Bets
            List<BetProfile> userBetProfiles = this._appDataRepository.GetAllBetProfiles()
                            .Where(x => x.Username == this._username).ToList();

            // Analysis profit NBA
            this.GetParseAnalysisProfitNBA(userBetProfiles);

            // Betting profiles NBA
            this.GetParseBettingProfilesNBA(userBetProfiles);

            // Performance NBA
            this.GetParsePerformanceNBA(userBetProfiles);

            // Load bets for profile ALL
            this.GetParseDefaultProfileDataNBA(userBetProfiles);
        }

        #endregion

        #region Helper methods

        private void GetParseGameScoresNBA()
        {
            this.BetsTrackerViewModel.GameScores = new List<BetsTrackerGameScoreViewModel>();

            // Get data:
            // show games of last 3 days and todays pending games
            DateTime sinceLimit, untilLimit;

            sinceLimit = DateTime.Now.Date.AddDays(-3);
            untilLimit = DateTime.Now.Date.AddDays(1).AddHours(8);

            IEnumerable<ScheduleResultsNBA> scheduleResults = this._appDataRepository.GetAllScheduleResultsNBA()
                                        .Where(x => (sinceLimit <= x.EventStartDateTime.Value) && (x.EventStartDateTime.Value <= untilLimit))
                                        .OrderByDescending(x => x.EventStartDateTime.Value);

            // Parse data
            foreach (ScheduleResultsNBA scheduleResult in scheduleResults)
            {
                BetsTrackerGameScoreViewModel btGameScoreVM = new BetsTrackerGameScoreViewModel();

                btGameScoreVM.Date = TypeDateTime.ParseDateTime(scheduleResult.EventStartDateTime);
                btGameScoreVM.HomeTeam = scheduleResult.Team.Abbreviation;
                btGameScoreVM.AwayTeam = scheduleResult.Opponent.Abbreviation;


                btGameScoreVM.HomeScore = string.Empty;
                btGameScoreVM.AwayScore = string.Empty;

                if (scheduleResult.EventStatus == EventStatusConst.COMPLETED)
                {
                    btGameScoreVM.Status = new Helper.HTML.CustomHtmlElement(null, null, "color: red", scheduleResult.EventStatus.ToString(), null);
                    btGameScoreVM.HomeScore = scheduleResult.TeamPointsScored.ToString();
                    btGameScoreVM.AwayScore = scheduleResult.OpponentPointsScored.ToString();
                }
                else if (scheduleResult.EventStatus == EventStatusConst.SCHEDULED)
                {
                    btGameScoreVM.Status = new Helper.HTML.CustomHtmlElement(null, null, "color: orange", scheduleResult.EventStatus.ToString(), null);
                }
                else
                {
                    btGameScoreVM.Status = new Helper.HTML.CustomHtmlElement(null, null, "color: gray", scheduleResult.EventStatus.ToString(), null);
                }

                this.BetsTrackerViewModel.GameScores.Add(btGameScoreVM);
            }
        }

        private void GetParseAnalysisProfitNBA(List<BetProfile> userBetProfiles)
        {
            int allTotalBets = 0, allWins = 0, allLosses = 0,
                lastTotalBets = 0, lastWins = 0, lastLosses = 0;

            decimal allTotalProfit = 0, allAverageOdds = 1, allInvested = 0,
                    lastTotalProfit = 0, lastAverageOdds = 1, lastInvested = 0;

            DateTime sinceTime = DateTime.Now.Date.AddDays(-30);

            foreach (BetProfile betProfile in userBetProfiles)
            {
                foreach (Bet bet in betProfile.Bets)
                {
                    if (bet.Created.Date >= sinceTime)
                    {
                        lastTotalBets++;

                        if (bet.BetStatus.Name == BetStatusConst.WIN)
                            lastWins++;
                        else if (bet.BetStatus.Name == BetStatusConst.LOSS)
                            lastLosses++;

                        lastAverageOdds *= bet.Odd;
                        lastInvested += bet.Invested;

                        if (bet.Profit.HasValue)
                            lastTotalProfit += bet.Profit.Value;
                    }

                    allTotalBets++;

                    if (bet.BetStatus.Name == BetStatusConst.WIN)
                        allWins++;
                    else if (bet.BetStatus.Name == BetStatusConst.LOSS)
                        allLosses++;

                    allAverageOdds *= bet.Odd;
                    allInvested += bet.Invested;

                    if (bet.Profit.HasValue)
                        allTotalProfit += bet.Profit.Value;
                }
            }

            this.BetsTrackerViewModel.AnalysisProfit = new List<BetsTrackerAnalysisProfitViewModel>()
            {
                new BetsTrackerAnalysisProfitViewModel()
                {
                    Name = "all_time",
                    Label = "All Time",
                    TotalBets = allTotalBets.ToString(),
                    Wins = allWins.ToString(),
                    Losses = allLosses.ToString(),
                    AverageOdds = Math.Round(allAverageOdds, 2).ToString(CultureInfo.InvariantCulture),
                    ROI = allInvested == 0 ? "0" : Percentage.GetPercentage(allTotalProfit / allInvested).ToString(CultureInfo.InvariantCulture),
                    TotalProfit = Math.Round(allTotalProfit, 2).ToString(CultureInfo.InvariantCulture)
                },
                new BetsTrackerAnalysisProfitViewModel()
                {
                    Name = "last_30_days",
                    Label = "Last 30 Days",
                    TotalBets = lastTotalBets.ToString(),
                    Wins = lastWins.ToString(),
                    Losses = lastLosses.ToString(),
                    AverageOdds = Math.Round(lastAverageOdds, 2).ToString(CultureInfo.InvariantCulture),
                    ROI = lastInvested == 0 ? "0" : Percentage.GetPercentage(lastTotalProfit / lastInvested).ToString(CultureInfo.InvariantCulture),
                    TotalProfit = Math.Round(lastTotalProfit, 2).ToString(CultureInfo.InvariantCulture)
                }
            };
        }

        private void GetParseBettingProfilesNBA(List<BetProfile> userBetProfiles)
        {
            this.BetsTrackerViewModel.BettingProfiles = new List<BetsTrackerBettingProfileViewModel>()
            {
                new BetsTrackerBettingProfileViewModel()
                {
                    ActiveTab = true,
                    Id = "0",
                    Label = "All",
                    OpenBets = null,
                    LastBetsStatus = null
                }
            };

            foreach (BetProfile betProfile in userBetProfiles)
            {
                BetsTrackerBettingProfileViewModel bettingProfileVM = new BetsTrackerBettingProfileViewModel();

                bettingProfileVM.ActiveTab = false;
                bettingProfileVM.Id = betProfile.Id.ToString();
                bettingProfileVM.Label = betProfile.Name;
                bettingProfileVM.OpenBets = betProfile.Bets.Count(x => x.BetStatus.Name == BetStatusConst.SCHEDULED).ToString();
                
                foreach (Bet bet in betProfile.Bets.Take(10))
                {
                    if (bet.BetStatus.Name == BetStatusConst.WIN)
                        bettingProfileVM.LastBetsStatus += "1,";
                    else if (bet.BetStatus.Name == BetStatusConst.LOSS)
                        bettingProfileVM.LastBetsStatus += "-1,";
                    else if (bet.BetStatus.Name == BetStatusConst.PUSH)
                        bettingProfileVM.LastBetsStatus += "0,";
                }
                bettingProfileVM.LastBetsStatus = bettingProfileVM.LastBetsStatus.Trim(',');

                this.BetsTrackerViewModel.BettingProfiles.Add(bettingProfileVM);
            }
        }

        private void GetParsePerformanceNBA(List<BetProfile> userBetProfiles)
        {
            this.BetsTrackerViewModel.Performances = new List<BetsTrackerPerformanceViewModel>();

            foreach (BetProfile betProfile in userBetProfiles)
            {
                BetsTrackerPerformanceViewModel performanceVM = new BetsTrackerPerformanceViewModel();

                performanceVM.Profile = betProfile.Name;

                decimal profit = 0, invested = 0;

                foreach (Bet bet in betProfile.Bets)
                {
                    if (bet.Profit.HasValue)
                        profit += bet.Profit.Value;

                    invested += bet.Invested;
                }

                performanceVM.ROI = Percentage.GetPercentage(profit / invested).ToString(CultureInfo.InvariantCulture);
                performanceVM.Profit = Math.Round(profit, 2).ToString(CultureInfo.InvariantCulture);

                this.BetsTrackerViewModel.Performances.Add(performanceVM);
            }
        }

        private void GetParseDefaultProfileDataNBA(List<BetProfile> userBetProfiles)
        {
            this.BetsTrackerViewModel.DefaultProfile = new BetsTrackerBettingProfileDataViewModel();

            // Get profile name
            this.BetsTrackerViewModel.DefaultProfile.ProfileName = "All";

            int totalBets = 0, wins = 0, losses = 0,
                chartOverviewTotal = 0, chartOverviewMoneyline = 0, chartOverviewHandicap = 0, chartOverviewParlay = 0;
            decimal totalProfit = 0, averageOdds = 1, invested = 0;

            // Get bets
            this.BetsTrackerViewModel.DefaultProfile.Bets = new List<BetsTrackerProfileBetViewModel>();

            foreach (BetProfile betProfile in userBetProfiles)
            {
                foreach (Bet bet in betProfile.Bets)
                {
                    // Parse bet view model
                    BetsTrackerProfileBetViewModel betVM = new BetsTrackerProfileBetViewModel();

                    List<BetGame> betGames = bet.BetGames.ToList();

                    if (betGames.Count == 1)
                        betVM.Type = "single";
                    else
                        betVM.Type = "ticket";

                    betVM.Timestamp = TypeDateTime.ParseDateTime(bet.Created);
                    betVM.BetAmount = bet.Invested.ToString(CultureInfo.InvariantCulture);
                    betVM.Odds = Math.Round(bet.Odd, 2).ToString(CultureInfo.InvariantCulture);
                    betVM.BetStatus = bet.BetStatus.Name.ToUpper();

                    betVM.Game = string.Join("\n", betGames.Select(x => x.EventNBA.Team.Name + " - " + x.EventNBA.Opponent.Name));
                    betVM.BetValue = string.Empty;
                    
                    if (bet.Profit.HasValue)
                        betVM.ProfitLoss = Math.Round(bet.Profit.Value, 2).ToString(CultureInfo.InvariantCulture);
                    else
                        betVM.ProfitLoss = string.Empty;

                    this.BetsTrackerViewModel.DefaultProfile.Bets.Add(betVM);

                    // Take analytics
                    totalBets++;

                    if (bet.BetStatus.Name == BetStatusConst.WIN)
                        wins++;
                    else if (bet.BetStatus.Name == BetStatusConst.LOSS)
                        losses++;

                    averageOdds *= bet.Odd;
                    invested += bet.Invested;

                    if (bet.Profit.HasValue)
                        totalProfit += bet.Profit.Value;

                    // Take charts analytics
                    if (bet.BetGames.Count == 1)
                    {
                        string betType = bet.BetGames.First().BetType.Name;

                        if (betType == BetTypeConst.TOTAL_OVER || betType == BetTypeConst.TOTAL_UNDER)
                        {
                            chartOverviewTotal++;
                        }
                        else if (betType == BetTypeConst.TEAM_AWAY_HANDICAP || betType == BetTypeConst.TEAM_HOME_HANDICAP)
                        {
                            chartOverviewHandicap++;
                        }
                        else
                        {
                            chartOverviewTotal++;
                        }
                    }
                    else
                    {
                        chartOverviewParlay++;
                    }
                }
            }

            // Get info boxes values
            this.BetsTrackerViewModel.DefaultProfile.InfoBoxesValues = new List<string>()
            {
                totalBets.ToString(),
                wins.ToString(),
                losses.ToString(),
                Math.Round(averageOdds, 2).ToString(CultureInfo.InvariantCulture),
                invested == 0 ? "0" : Percentage.GetPercentage(totalProfit / invested).ToString(CultureInfo.InvariantCulture),
                Math.Round(totalProfit, 2).ToString(CultureInfo.InvariantCulture)
            };

            // Get statistics
            var dailyBets = this._appDataRepository.GetAllBets()
                .Where(x => x.BetProfiles.Any(y => y.Username == _username && y.Name == "All"))
                .GroupBy(x => DbFunctions.TruncateTime(x.Created))
                .ToList();

            this.BetsTrackerViewModel.DefaultProfile.Statistics = new List<BetsTrackerProfileStatisticViewModel>()
            {
                new BetsTrackerProfileStatisticViewModel()
                {
                    ActiveTab = true,
                    Name = "daily",
                    Label = "Daily",
                    Data = dailyBets.Select(x => new BetsTrackerProfileStatisticElementViewModel()
                    {
                        Date = TypeDateTime.ParseDateTime(x.Key),
                        Wins = x.Where(y => y.BetStatus.Name == BetStatusConst.WIN).Count().ToString(),
                        Losses = x.Where(y => y.BetStatus.Name == BetStatusConst.LOSS).Count().ToString(),
                        Invested = Math.Round(x.Sum(y => y.Invested), 2).ToString(CultureInfo.InvariantCulture),
                        Profit = Math.Round(x.Sum(y => y.Profit.HasValue ? y.Profit.Value : 0), 2).ToString(CultureInfo.InvariantCulture)
                    }).ToList()
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
                    TotalValue = chartOverviewTotal.ToString(),
                    MoneylineValue = chartOverviewMoneyline.ToString(),
                    Handicap = chartOverviewHandicap.ToString(),
                    Parlay = chartOverviewParlay.ToString()
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

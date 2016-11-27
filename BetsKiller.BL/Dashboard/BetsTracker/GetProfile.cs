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
    public class GetProfile : ProcessBase
    {
        #region Private

        private BetsTrackerBettingProfileDataViewModel _betsTrackerBettingProfileDataViewModel;

        private IAppDataRepository _appDataRepository;

        private int _profileId;
        private string _username;

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

        public GetProfile(string profileId, string username)
        {
            this._betsTrackerBettingProfileDataViewModel = new BetsTrackerBettingProfileDataViewModel();

            this._appDataRepository = new AppDataRepository();

            this._profileId = Convert.ToInt32(profileId);
            this._username = username;
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
            this._betsTrackerBettingProfileDataViewModel = new BetsTrackerBettingProfileDataViewModel();
            
            int totalBets = 0, wins = 0, losses = 0,
            chartOverviewTotal = 0, chartOverviewMoneyline = 0, chartOverviewHandicap = 0, chartOverviewParlay = 0;
            decimal totalProfit = 0, averageOdds = 1, invested = 0;

            // Get bets
            this._betsTrackerBettingProfileDataViewModel.Bets = new List<BetsTrackerProfileBetViewModel>();

            List<Bet> bets = new List<Bet>();
            if (this._profileId == 0)
            {
                this._appDataRepository.GetAllBetProfiles()
                    .Where(x => x.Username == this._username)
                    .OrderBy(x => DbFunctions.TruncateTime(x.Created))
                    .ToList()
                    .ForEach(x => bets.AddRange(x.Bets));

                this._betsTrackerBettingProfileDataViewModel.ProfileName = "All";
            }
            else
            {
                BetProfile betProfile = this._appDataRepository.GetAllBetProfiles()
                                            .Where(x => x.Id == this._profileId && x.Username == this._username)
                                            .OrderBy(x => DbFunctions.TruncateTime(x.Created))
                                            .SingleOrDefault();

                bets.AddRange(betProfile.Bets);

                this._betsTrackerBettingProfileDataViewModel.ProfileName = betProfile.Name;
            }

            foreach (Bet bet in bets)
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

                this._betsTrackerBettingProfileDataViewModel.Bets.Add(betVM);

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

            // Get info boxes values
            this._betsTrackerBettingProfileDataViewModel.InfoBoxesValues = new List<string>()
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

            this._betsTrackerBettingProfileDataViewModel.Statistics = new List<BetsTrackerProfileStatisticViewModel>()
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
            this._betsTrackerBettingProfileDataViewModel.Charts = new List<BetsTrackerProfileChartViewModel>()
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

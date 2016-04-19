using BetsKiller.DAL;
using BetsKiller.DAL.AppData;
using BetsKiller.Helper.Constants;
using BetsKiller.Helper.HTML;
using BetsKiller.Helper.HTML.Elements;
using BetsKiller.Helper.Operations;
using BetsKiller.Model;
using BetsKiller.ViewModel.Dashboard.Index;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.BL.Dashboard.Index
{
    public class GetData : ProcessBase
    {
        #region Private

        private DashboardViewModel _dashboardViewModel;

        private IAppDataRepository _appDataRepository;

        #endregion

        #region Properties

        public DashboardViewModel DashboardViewModel
        {
            get { return this._dashboardViewModel; }
        }

        #endregion

        #region Properties - override

        protected override string _successMessage
        {
            get { return "Dashboard data retrived successfully."; }
        }

        protected override string _failMessage
        {
            get { return "Dashboard data retrive failed."; }
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
            this._dashboardViewModel = new DashboardViewModel();

            // Get and parse headlines for NBA
            this.GetParseHeadlinesNBA();

            // Get and parse todays games for NBA
            this.GetParseTodaysGamesNBA();

            // Get and parse todays analysis for NBA
            this.GetParseTodaysAnalysisNBA();

            // Get and parse last analysis for nba
            this.GetParseLastAnalysisNBA();

            // Get and parse analysis profits for NBA
            this.GetParseAnalysisProfitNBA();

            // Get and parse last three months profit chart for NBA
            this.GetParseLastThreeMonthsProfitChartNBA();

            // Get and parse last year profit chart for NBA
            this.GetParseLastYearProfitChartNBA();

            // Get and parse analyse types
            this.GetParseAnalyseTypes();

            // Get and parse bet types
            this.GetParseBetTypes();
        }

        #endregion

        #region Helper methods

        private void GetParseHeadlinesNBA()
        {
            #region Headlines FEED

            this._dashboardViewModel.HeadlinesFeedNBA = new List<HeadlineNBAViewModel>();

            // Get data
            List<NewsFeed> newsFeed = this._appDataRepository.GetAllNewsFeed().OrderByDescending(x => x.Created).ToList();

            // Parse data
            foreach (NewsFeed news in newsFeed)
            {
                HeadlineNBAViewModel headline = new HeadlineNBAViewModel();

                headline.Id = news.Id.ToString();
                headline.Title = news.Title;
                headline.Description = news.Description;

                this._dashboardViewModel.HeadlinesFeedNBA.Add(headline);
            }

            #endregion

            #region Headlines PUBLISHED

            this._dashboardViewModel.HeadlinesPublishedNBA = new List<HeadlineNBAViewModel>();

            // Get data
            List<NewsPublish> newsPublish = this._appDataRepository.GetAllNewsPublish().OrderByDescending(x => x.Published).ToList();

            // Parse data
            foreach (NewsPublish news in newsPublish)
            {
                HeadlineNBAViewModel headline = new HeadlineNBAViewModel();

                headline.Id = news.Id.ToString();
                headline.Title = news.Title;
                headline.Description = news.Description;
                headline.Published = news.Published.ToString();

                this._dashboardViewModel.HeadlinesPublishedNBA.Add(headline);
            }

            #endregion
        }

        private void GetParseTodaysGamesNBA()
        {
            this._dashboardViewModel.TodaysGamesNBA = new List<TodaysGamesNBAViewModel>();

            // Get data:
            // - if it is period of day between 00:00 - 06:00 show games from this night
            // - if it is other period show for today and first 8 hours of tomorrow.
            DateTime sinceLimit, untilLimit;
            if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour <= 6)
            {
                sinceLimit = DateTime.Now.Date;
                untilLimit = DateTime.Now.Date.AddHours(6);
            }
            else
            {
                sinceLimit = DateTime.Now.Date.AddHours(17); // Since current day evening at 17:00
                untilLimit = DateTime.Now.Date.AddDays(1).AddHours(8); // Until next day morning in 08:00
            }

            IEnumerable<ScheduleResultsNBA> scheduleResults = this._appDataRepository.GetAllScheduleResultsNBA()
                            .Where(x => (sinceLimit <= x.EventStartDateTime.Value) && (x.EventStartDateTime.Value <= untilLimit))
                            .OrderBy(x => x.EventStartDateTime.Value);

            // Parse data
            foreach (ScheduleResultsNBA scheduleResult in scheduleResults)
            {
                TodaysGamesNBAViewModel todayGameVM = new TodaysGamesNBAViewModel();

                todayGameVM.EventId = scheduleResult.EventId;
                todayGameVM.EventStart = ((DateTime)scheduleResult.EventStartDateTime).ToString("dd.MM.yyyy. HH:mm");
                todayGameVM.EventStatus = scheduleResult.EventStatus;
                todayGameVM.SeasonType = scheduleResult.EventSeasonType;
                todayGameVM.SiteCapacity = scheduleResult.SiteCapacity.ToString();
                todayGameVM.SiteSurface = scheduleResult.SiteSurface;
                todayGameVM.SiteName = scheduleResult.SiteName;
                todayGameVM.SiteState = scheduleResult.SiteState;
                todayGameVM.SiteCity = scheduleResult.SiteCity;

                todayGameVM.HomeTeamName = scheduleResult.Team.Name.Name;
                todayGameVM.HomeTeamSU = scheduleResult.Team.HomeSU;
                todayGameVM.HomeTeamATS = scheduleResult.Team.HomeATS;
                todayGameVM.HomeTeamOU = scheduleResult.Team.HomeOU;
                todayGameVM.HomeTeamAbbreviation = scheduleResult.Team.Abbreviation;

                todayGameVM.AwayTeamName = scheduleResult.Opponent.Name.Name;
                todayGameVM.AwayTeamSU = scheduleResult.Opponent.AwaySU;
                todayGameVM.AwayTeamATS = scheduleResult.Opponent.AwayATS;
                todayGameVM.AwayTeamOU = scheduleResult.Opponent.AwayOU;
                todayGameVM.AwayTeamAbbreviation = scheduleResult.Opponent.Abbreviation;

                if (scheduleResult.EventStatus == EventStatusConst.COMPLETED)
                {
                    todayGameVM.HomePointsScored = scheduleResult.TeamPointsScored.ToString();
                    todayGameVM.HomePeriodScores = " (" + scheduleResult.TeamPeriodScores + ")";

                    todayGameVM.AwayPointsScored = scheduleResult.OpponentPointsScored.ToString();
                    todayGameVM.AwayPeriodScores = " (" + scheduleResult.OpponentPeriodScores + ")";
                }

                this._dashboardViewModel.TodaysGamesNBA.Add(todayGameVM);
            }
        }

        private void GetParseTodaysAnalysisNBA()
        {
            this._dashboardViewModel.TodaysFreeAnalysisNBA = new List<AnalysisViewModel>();
            this._dashboardViewModel.TodaysPremiumAnalysisNBA = new List<AnalysisViewModel>();

            // Get period of taking data
            DateTime since, until;

            // If it is a period of 00:00 - 09:15 show analaysis of day before from 18h because job is not run until 09:15.
            TimeSpan timeSince = new TimeSpan(0, 0, 0);
            TimeSpan timeUntil = new TimeSpan(9, 15, 0);
            if (timeSince <= DateTime.Now.TimeOfDay && DateTime.Now.TimeOfDay <= timeUntil)
            {
                since = DateTime.Today.AddHours(-6);
            }
            else
            {
                since = DateTime.Today;
            }
            
            until = DateTime.Today.AddDays(1).AddHours(8); // Until tomorrow in 8 o'clock in the morning.

            // Get data
            IEnumerable<Analysis> analysisNba = this._appDataRepository.GetAllAnalysis()
                            .Where(x => x.Sport.Name == SportConst.NBA && x.EventNBA.EventStartDateTime >= since && x.EventNBA.EventStartDateTime <= until && x.BetStatus.Name == BetStatusConst.SCHEDULED)
                            .OrderByDescending(x => x.EventNBA.EventStartDateTime);

            // Parse data
            foreach (Analysis analyse in analysisNba)
            {
                AnalysisViewModel analyseVM = new AnalysisViewModel();

                analyseVM.Id = analyse.Id.ToString();
                analyseVM.HomeTeamName = analyse.EventNBA.Team.Name.Name;
                analyseVM.HomeTeamAbbreviation = analyse.EventNBA.Team.Abbreviation;

                analyseVM.AwayTeamName = analyse.EventNBA.Opponent.Name.Name;
                analyseVM.AwayTeamAbbreviation = analyse.EventNBA.Opponent.Abbreviation;

                analyseVM.BetType = analyse.BetType.Name;
                analyseVM.Pick = Math.Round(analyse.Bet, 1).ToString(CultureInfo.InvariantCulture);
                analyseVM.Confidence = new CustomHtmlElement(null, null, "font-size: small; margin-left: 10px", "(" + Math.Round(analyse.Confidence, 2).ToString(CultureInfo.InvariantCulture) + "%)", FiveStarsRating.GetElements(Math.Round(analyse.Confidence, 2)));
                analyseVM.Odds = Math.Round(analyse.Odd, 3).ToString(CultureInfo.InvariantCulture);
                analyseVM.Invested = Math.Round(analyse.Invested, 2).ToString(CultureInfo.InvariantCulture) + " (units)";
                analyseVM.Description = analyse.Description;

                if (analyse.AnalyseType.Name == AnalyseTypeConst.FREE)
                {
                    this._dashboardViewModel.TodaysFreeAnalysisNBA.Add(analyseVM);
                }
                else // analyse.AnalyseType.Name == AnalyseTypeConst.PREMIUM
                {
                    this._dashboardViewModel.TodaysPremiumAnalysisNBA.Add(analyseVM);
                }
            }
        }

        private void GetParseLastAnalysisNBA()
        {
            this._dashboardViewModel.LastFreeAnalysisNBA = new List<AnalysisViewModel>();
            this._dashboardViewModel.LastPremiumAnalysisNBA = new List<AnalysisViewModel>();

            // Get data
            List<Analysis> analysisNba = new List<Analysis>();

            // Free analysis
            analysisNba.AddRange(this._appDataRepository.GetAllAnalysis()
                                    .Where(x => x.Sport.Name == SportConst.NBA && x.BetStatus.Name != BetStatusConst.SCHEDULED && x.AnalyseType.Name == AnalyseTypeConst.FREE)
                                    .OrderByDescending(x => x.EventNBA.EventStartDateTime)
                                    .Take(10));

            // Premium analysis
            analysisNba.AddRange(this._appDataRepository.GetAllAnalysis()
                                    .Where(x => x.Sport.Name == SportConst.NBA && x.BetStatus.Name != BetStatusConst.SCHEDULED && x.AnalyseType.Name == AnalyseTypeConst.PAID)
                                    .OrderByDescending(x => x.EventNBA.EventStartDateTime)
                                    .Take(10));

            // Parse data
            foreach (Analysis analyse in analysisNba)
            {
                AnalysisViewModel analyseVM = new AnalysisViewModel();

                analyseVM.HomeTeamAbbreviation = analyse.EventNBA.Team.Abbreviation;

                analyseVM.AwayTeamAbbreviation = analyse.EventNBA.Opponent.Abbreviation;

                analyseVM.Pick = analyse.BetType.Name + " " + Math.Round(analyse.Bet, 2).ToString(CultureInfo.InvariantCulture);

                analyseVM.Result = analyse.Result;

                analyseVM.Profit = new CustomHtmlElement(null, null, "color: red", "color: green").GetElementByValue(analyse.Profit != null ? (decimal)analyse.Profit : 0);
                analyseVM.Profit.Value = (analyse.Profit != null ? Math.Round((decimal)analyse.Profit, 2) : 0).ToString(CultureInfo.InvariantCulture) + " units";

                analyseVM.Date = ((DateTime)analyse.EventNBA.EventStartDateTime).ToShortDateString();

                analyseVM.Status = new CustomHtmlElement();
                analyseVM.Status.Value = analyse.BetStatus.Name;
                if (analyse.BetStatus.Name == "WIN")
                    analyseVM.Status.Style = "color: green; font-weight: bold";
                else if (analyse.BetStatus.Name == "LOSS")
                    analyseVM.Status.Style = "color: red; font-weight: bold";

                if (analyse.AnalyseType.Name == AnalyseTypeConst.FREE)
                {
                    this._dashboardViewModel.LastFreeAnalysisNBA.Add(analyseVM);
                }
                else // analyse.AnalyseType.Name == AnalyseTypeConst.PREMIUM
                {
                    this._dashboardViewModel.LastPremiumAnalysisNBA.Add(analyseVM);
                }
            }
        }

        private void GetParseAnalysisProfitNBA()
        {
            this._dashboardViewModel.AnalysisProfitNBA = new AnalysisProfitViewModel();

            // Get data
            IEnumerable<AnalysisProfit> listAnalysisProfit = this._appDataRepository.GetAllAnalysisProfit()
                                       .Where(x => x.Sport.Name == SportConst.NBA)
                                       .OrderBy(x => x.FirstDayInWeek);

            // Collect and parse data
            AnalysisProfitViewModel analysisProfitVM = new AnalysisProfitViewModel();

            AnalysisProfit overall = new AnalysisProfit();
            AnalysisProfit lastWeek = new AnalysisProfit();

            for (int i = 0; i < listAnalysisProfit.Count(); i++)
            {
                overall.TotalBets += listAnalysisProfit.ElementAt(i).TotalBets;
                overall.Wins += listAnalysisProfit.ElementAt(i).Wins;
                overall.Losses += listAnalysisProfit.ElementAt(i).Losses;

                overall.Invested += Math.Round(listAnalysisProfit.ElementAt(i).Invested, 2);
                overall.Profit += Math.Round(listAnalysisProfit.ElementAt(i).Profit, 2);

                if (i == listAnalysisProfit.Count() - 1) // Last element, not necessery to calculate all
                {
                    overall.ROI = Math.Round(overall.Profit / overall.Invested, 4);
                }

                if (i == listAnalysisProfit.Count() - 2)
                {
                    lastWeek.TotalBets = overall.TotalBets;
                    lastWeek.Wins = overall.Wins;
                    lastWeek.Losses = overall.Losses;
                    lastWeek.Invested = overall.Invested;
                    lastWeek.Profit = overall.Profit;
                    lastWeek.ROI = Math.Round(overall.Profit / overall.Invested, 4);
                }
            }

            analysisProfitVM.TotalBets = overall.TotalBets.ToString();
            analysisProfitVM.Wins = overall.Wins.ToString();
            analysisProfitVM.Losses = overall.Losses.ToString();
            analysisProfitVM.TotalInvested = Math.Round(overall.Invested, 2).ToString(CultureInfo.InvariantCulture);
            analysisProfitVM.ROI = Percentage.GetPercentage(overall.ROI).ToString(CultureInfo.InvariantCulture);
            analysisProfitVM.Profit = Math.Round(overall.Profit, 2).ToString(CultureInfo.InvariantCulture);

            CustomHtmlElement customElement = new CustomHtmlElement("fa fa-arrow-down count_bottom", "fa fa-arrow-up count_bottom", "color: red", "color: green");

            analysisProfitVM.TotalBetsPctFromLastWeek = customElement.GetElementByValue(Percentage.GetRawPercentage((decimal)overall.TotalBets, lastWeek.TotalBets));
            analysisProfitVM.WinsPctFromLastWeek = customElement.GetElementByValue(Percentage.GetRawPercentage((decimal)overall.Wins, lastWeek.Wins));
            analysisProfitVM.LossesPctFromLastWeek = customElement.GetElementByValue(Percentage.GetRawPercentage((decimal)overall.Losses, lastWeek.Losses), true);
            analysisProfitVM.TotalInvestedPctFromLastWeek = customElement.GetElementByValue(Percentage.GetRawPercentage(overall.Invested, lastWeek.Invested));
            analysisProfitVM.ROIPctFromLastWeek = customElement.GetElementByValue(Percentage.GetRawPercentage(overall.ROI, lastWeek.ROI));
            analysisProfitVM.ProfitPctFromLastWeek = customElement.GetElementByValue(Percentage.GetRawPercentage(overall.Profit, lastWeek.Profit));

            decimal unitDollars = Convert.ToDecimal(ConfigurationManager.AppSettings["UnitDollars"], CultureInfo.InvariantCulture);
            analysisProfitVM.TotalInvestedMoney = "($" + string.Format(CultureInfo.InvariantCulture, "{0:N}", Math.Round(overall.Invested * unitDollars, 2)) + ")";
            analysisProfitVM.ProfitMoney = "($" + string.Format(CultureInfo.InvariantCulture, "{0:N}", Math.Round(overall.Profit * unitDollars, 2)) + ")";

            this._dashboardViewModel.AnalysisProfitNBA = analysisProfitVM;
        }

        private void GetParseLastThreeMonthsProfitChartNBA()
        {
            this._dashboardViewModel.LastThreeMonthsProfitChart = new List<ProfitChartViewModel>();

            // Get data
            DateTime beforeThreeMonths = DateTime.Now.AddMonths(-3);

            IEnumerable<AnalysisProfit> listAnalysisProfit = this._appDataRepository.GetAllAnalysisProfit()
                                       .Where(x => x.Sport.Name == SportConst.NBA && x.FirstDayInWeek >= beforeThreeMonths)
                                       .OrderBy(x => x.FirstDayInWeek);

            // Parse data
            foreach (AnalysisProfit analyseProfit in listAnalysisProfit)
            {
                ProfitChartViewModel profitChartVM = new ProfitChartViewModel();

                DateTime lastDayInWeek = analyseProfit.FirstDayInWeek.AddDays(6);
                if (lastDayInWeek > DateTime.Now.Date)
                {
                    lastDayInWeek = DateTime.Now.Date;
                }

                profitChartVM.LastDayInWeek = lastDayInWeek.ToString("yyyy-MM-dd");
                profitChartVM.Week = analyseProfit.Year + " W" + analyseProfit.Week;
                profitChartVM.Profit = analyseProfit.Profit.ToString(CultureInfo.InvariantCulture);
                profitChartVM.Wins = analyseProfit.Wins.ToString();
                profitChartVM.Losses = analyseProfit.Losses.ToString();

                this._dashboardViewModel.LastThreeMonthsProfitChart.Add(profitChartVM);
            }
        }

        private void GetParseLastYearProfitChartNBA()
        {
            this._dashboardViewModel.LastYearProfitChart = new List<ProfitChartViewModel>();

            // Get data
            DateTime beforeYear = DateTime.Now.AddYears(-1);

            IEnumerable<AnalysisProfit> listAnalysisProfit = this._appDataRepository.GetAllAnalysisProfit()
                                       .Where(x => x.Sport.Name == SportConst.NBA && x.FirstDayInWeek >= beforeYear)
                                       .OrderBy(x => x.FirstDayInWeek);

            // Parse data
            decimal profit = 0;
            int wins = 0,
                losses = 0;

            foreach (AnalysisProfit analyseProfit in listAnalysisProfit)
            {
                ProfitChartViewModel profitChartVM = new ProfitChartViewModel();

                profit += analyseProfit.Profit;
                wins += analyseProfit.Wins;
                losses += analyseProfit.Losses;

                DateTime lastDayInWeek = analyseProfit.FirstDayInWeek.AddDays(6);
                if (lastDayInWeek > DateTime.Now.Date)
                {
                    lastDayInWeek = DateTime.Now.Date;
                }

                profitChartVM.LastDayInWeek = lastDayInWeek.ToString("yyyy-MM-dd");
                profitChartVM.Week = analyseProfit.Year + " W" + analyseProfit.Week;
                profitChartVM.Profit = profit.ToString(CultureInfo.InvariantCulture);
                profitChartVM.Wins = wins.ToString();
                profitChartVM.Losses = losses.ToString();

                this._dashboardViewModel.LastYearProfitChart.Add(profitChartVM);
            }
        }

        private void GetParseAnalyseTypes()
        {
            this._dashboardViewModel.AnalyseTypes = new List<AnalyseTypeViewModel>();

            IEnumerable<AnalyseType> analyseTypes = this._appDataRepository.GetAllAnalyseType();

            foreach (AnalyseType analyseType in analyseTypes)
            {
                AnalyseTypeViewModel analyseTypeVM = new AnalyseTypeViewModel();

                analyseTypeVM.Id = analyseType.Id.ToString();
                analyseTypeVM.Label = analyseType.Name;

                this._dashboardViewModel.AnalyseTypes.Add(analyseTypeVM);
            }
        }

        private void GetParseBetTypes()
        {
            this._dashboardViewModel.BetTypes = new List<BetTypeViewModel>();

            IEnumerable<BetType> betTypes = this._appDataRepository.GetAllBetType();

            foreach (BetType betType in betTypes)
            {
                BetTypeViewModel betTypeVM = new BetTypeViewModel();

                betTypeVM.Id = betType.Id.ToString();
                betTypeVM.Label = betType.Name;

                this._dashboardViewModel.BetTypes.Add(betTypeVM);
            }
        }

        #endregion
    }
}

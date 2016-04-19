using BetsKiller.DAL;
using BetsKiller.DAL.AppData;
using BetsKiller.Model;
using BetsKiller.ViewModel.Home;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BetsKiller.BL.Home
{
    public class GetData : ProcessBase
    {
        #region Private

        private HomeViewModel _homeViewModel;

        private IAppDataRepository _appDataRepository;

        #endregion

        #region Properties

        public HomeViewModel HomeViewModel
        {
            get { return this._homeViewModel; }
        }

        #endregion

        #region Properties - override

        protected override string _successMessage
        {
            get { return "Home data retrived successfully."; }
        }

        protected override string _failMessage
        {
            get { return "Home data retrive failed."; }
        }

        #endregion

        #region Constructors

        public GetData()
        {
            this._appDataRepository = new AppDataRepository();
            this._homeViewModel = new HomeViewModel();
        }

        #endregion

        #region Methods

        protected override void Process()
        {
            // Get and parse analysis profits
            this.GetParseAnalysis();

            // Get and parse quotes
            this.GetParseQuotes();

            // Get and parse images
            this.GetParseImages();
        }

        #endregion

        #region Helper methods

        private void GetParseAnalysis()
        {
            IEnumerable<AnalysisProfit> listAnalysisProfit = this._appDataRepository.GetAllAnalysisProfit().ToList();

            // Parse data
            decimal profit = 0,
                    roi = 0,
                    invested = 0;

            int predictions = 0;

            foreach (AnalysisProfit analyseProfit in listAnalysisProfit)
            {
                profit += analyseProfit.Profit;
                predictions += analyseProfit.TotalBets;
                invested += analyseProfit.Invested;
            }

            if (invested == 0 || profit == 0)
            {
                roi = 0;
            }
            else
            {
                roi = invested / profit;
            }

            decimal unitDollars = Convert.ToDecimal(ConfigurationManager.AppSettings["UnitDollars"], CultureInfo.InvariantCulture);
            this._homeViewModel.Profit = string.Format(CultureInfo.InvariantCulture, "{0:N}", Math.Round(profit * unitDollars, 2));
            this._homeViewModel.Predictions = predictions.ToString();
            this._homeViewModel.ROI = Math.Round(roi, 2).ToString(CultureInfo.InvariantCulture);
        }

        private void GetParseQuotes()
        {
            int randomNumber = new Random().Next(1, 18);
            string quote = string.Empty;

            switch (randomNumber)
            {
                case 1:
                    quote = "If you would be wealthy, think of saving as well as getting.";
                    break;

                case 2:
                    quote = "I’m a great believer in luck, and I find the harder I work the more I have of it.";
                    break;

                case 3:
                    quote = "The only sure thing about luck is that it will change.";
                    break;

                case 4:
                    quote = "It is better to have a permanent income than to be fascinating.";
                    break;

                case 5:
                    quote = "In gambling the many must lose in order that the few may win.";
                    break;

                case 6:
                    quote = "The safest way to double your money is to fold it over once and put it in your pocket.";
                    break;

                case 7:
                    quote = "Luck is what happens when preparation meets opportunity.";
                    break;

                case 8:
                    quote = "Gambling: The sure way of getting nothing from something.";
                    break;

                case 9:
                    quote = "Remember this: The house doesn’t beat the player. It just gives him the opportunity to beat himself.";
                    break;

                case 10:
                    quote = "Gambling is a way of buying hope on credit.";
                    break;

                case 11:
                    quote = "A dollar won is twice as sweet as a dollar earned...";
                    break;

                case 12:
                    quote = "You don’t gamble to win. You gamble so you can gamble the next day.";
                    break;

                case 13:
                    quote = "A gambler never makes the same mistake twice. It’s usually three or more times.";
                    break;

                case 14:
                    quote = "A gambler plays even when the odds are immutable and against him.";
                    break;

                case 15:
                    quote = "You’ll always miss 100% of the shots you don’t take.";
                    break;

                case 16:
                    quote = "Gambling is the future on the internet. You can only look at so many dirty pictures.";
                    break;

                case 17:
                    quote = "Every dog has his day.";
                    break;
            }

            this._homeViewModel.Quote = quote;
        }

        private void GetParseImages()
        {
            this._homeViewModel.Images = new List<HomeImageViewModel>()
            {
                new HomeImageViewModel()
                {
                    Path = "Content/Images/bball_01.jpg",
                    Caption = "Leading Sports Predictions Software",
                    Description = "Increase your Chances For Winning and making Money Online, with our Revolutionary Software based on Advanced Statistical Analysis"
                },
                new HomeImageViewModel()
                {
                    Path = "Content/Images/baseball_01.jpg",
                    Caption = "MLB and NBA season long odds, stats and news",
                    Description = "Daily updated, for Most Popular Sports on the planet"
                },
                new HomeImageViewModel()
                {
                    Path = "Content/Images/bball_02.jpg",
                    Caption = "NBA and MLB games analysis and picks every day",
                    Description = "We are using Advanced Software and our knowledge about NBA and MLB to determine which games are Best To Bet On"
                },
                new HomeImageViewModel()
                {
                    Path = "Content/Images/baseball_02.jpg",
                    Caption = "Basic and advanced Bets Tracker tool - COMING SOON",
                    Description = "We are bringing you Best Tool you can get to track all your bets, Bets Stats and winnings daily"
                },
                new HomeImageViewModel()
                {
                    Path = "Content/Images/bball_03.jpg",
                    Caption = "Games Analyser - COMING SOON",
                    Description = "Tool to analyse MLB and NBA games, determine which game is Bets To Bet On based on rankings, stats and trends"
                }
                //,
                //new HomeImageViewModel()
                //{
                //    Path = "Content/Images/baseball_03.jpg",
                //    Caption = "6",
                //    Description = "6"
                //},
                //new HomeImageViewModel()
                //{
                //    Path = "Content/Images/bball_04.jpg",
                //    Caption = "7",
                //    Description = "7"
                //}
            };
        }

        #endregion
    }
}

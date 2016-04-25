using BetsKiller.DAL;
using BetsKiller.DAL.AppData;
using BetsKiller.Helper.Constants;
using BetsKiller.Helper.HTML;
using BetsKiller.Helper.Operations;
using BetsKiller.Helper.Types;
using BetsKiller.Model;
using BetsKiller.ViewModel.Dashboard.SportsPicks;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.BL.Dashboard.SportsPicks
{
    public class GetData : ProcessBase
    {
        #region Private

        private SportsPicksViewModel _sportsPicksViewModel;

        private IAppDataRepository _appDataRepository;

        #endregion

        #region Properties

        public SportsPicksViewModel SportsPicksViewModel
        {
            get { return this._sportsPicksViewModel; }
        }

        #endregion

        #region Properties - override

        protected override string _successMessage
        {
            get { return "Sports picks data retrived successfully."; }
        }

        protected override string _failMessage
        {
            get { return "Sports picks data retrive failed."; }
        }

        #endregion

        #region Constructors

        public GetData()
        {
            this._appDataRepository = new AppDataRepository();
            this._sportsPicksViewModel = new SportsPicksViewModel();
        }

        #endregion

        #region Methods

        protected override void Process()
        {
            this._sportsPicksViewModel.Types = new List<SportsPicksTypeViewModel>();

            // Get and parse NBA sport free picks
            this.GetParseSportsFreePicksNBA();

            // Get and parse NBA sport premium picks
            this.GetParseSportsPremiumPicksNBA();
        }

        #endregion

        #region Helper methods

        private void GetParseSportsFreePicksNBA()
        {
            SportsPicksTypeViewModel type = new SportsPicksTypeViewModel();
            type.Caption = new CustomHtmlElement("span", "label label-info", "margin-right: 5px; background-color: #00c0ef", "Free", null);

            IEnumerable<Analysis> freeAnalysis = this._appDataRepository.GetAllAnalysis()
                                                    .Where(x => x.Sport.Name == SportConst.NBA && x.AnalyseType.Name == AnalyseTypeConst.FREE && x.EventNBA.EventStatus != EventStatusConst.SCHEDULED)
                                                    .OrderByDescending(x => x.EventNBA.EventStartDateTime);

            this.CalculateAnalysis(type, freeAnalysis);

            this._sportsPicksViewModel.Types.Add(type);
        }

        private void GetParseSportsPremiumPicksNBA()
        {
            SportsPicksTypeViewModel type = new SportsPicksTypeViewModel();
            type.Caption = new CustomHtmlElement("span", "label label-warning", "margin-right: 5px", "Premium", null);

            IEnumerable<Analysis> premiumAnalysis = this._appDataRepository.GetAllAnalysis()
                                                    .Where(x => x.Sport.Name == SportConst.NBA && x.AnalyseType.Name == AnalyseTypeConst.PAID && x.EventNBA.EventStatus != EventStatusConst.SCHEDULED)
                                                    .OrderByDescending(x => x.EventNBA.EventStartDateTime);

            this.CalculateAnalysis(type, premiumAnalysis);

            this._sportsPicksViewModel.Types.Add(type);
        }

        private void CalculateAnalysis(SportsPicksTypeViewModel type, IEnumerable<Analysis> analysis)
        {
            type.Analysis = new List<SportsPicksAnalyseViewModel>();

            // Parse stats
            decimal profit = 0,
                    totalInvested = 0;
            int totalBets = 0,
                wins = 0,
                losses = 0;

            foreach (Analysis analyse in analysis)
            {
                // Parse stats
                profit += analyse.Profit.HasValue ? analyse.Profit.Value : 0;
                totalInvested += analyse.Invested;
                totalBets++;

                if (analyse.BetStatus.Name == BetStatusConst.WIN)
                    wins++;
                else if (analyse.BetStatus.Name == BetStatusConst.LOSS)
                    losses++;

                // Parse analysis
                SportsPicksAnalyseViewModel analyseVM = new SportsPicksAnalyseViewModel();

                analyseVM.HomeTeamAbbreviation = analyse.EventNBA.Team.Abbreviation;

                analyseVM.AwayTeamAbbreviation = analyse.EventNBA.Opponent.Abbreviation;

                analyseVM.Pick = analyse.BetType.Name + " " + Math.Round(analyse.Bet, 2).ToString(CultureInfo.InvariantCulture);

                analyseVM.Result = analyse.Result;

                analyseVM.Invested = Math.Round(analyse.Invested, 2).ToString(CultureInfo.InvariantCulture);

                analyseVM.Profit = new CustomHtmlElement(null, null, "color: red", "color: green").GetElementByValue(analyse.Profit != null ? (decimal)analyse.Profit : 0);
                analyseVM.Profit.Value = (analyse.Profit != null ? Math.Round((decimal)analyse.Profit, 2) : 0).ToString(CultureInfo.InvariantCulture) + " units";

                analyseVM.Date = TypeDateTime.ParseDateTime(analyse.EventNBA.EventStartDateTime);

                analyseVM.Status = new CustomHtmlElement();
                analyseVM.Status.Value = analyse.BetStatus.Name;
                if (analyse.BetStatus.Name == "WIN")
                    analyseVM.Status.Style = "color: green; font-weight: bold";
                else if (analyse.BetStatus.Name == "LOSS")
                    analyseVM.Status.Style = "color: red; font-weight: bold";

                type.Analysis.Add(analyseVM);
            }

            type.Stats = new SportsPicksStatsViewModel();
            type.Stats.TotalBets = totalBets.ToString();
            type.Stats.Wins = wins.ToString();
            type.Stats.Losses = losses.ToString();
            type.Stats.TotalInvested = Math.Round(totalInvested, 2).ToString(CultureInfo.InvariantCulture);
            type.Stats.ROI = Percentage.GetPercentage(profit / totalInvested).ToString(CultureInfo.InvariantCulture);
            type.Stats.Profit = Math.Round(profit, 2).ToString(CultureInfo.InvariantCulture);
        }

        #endregion
    }
}

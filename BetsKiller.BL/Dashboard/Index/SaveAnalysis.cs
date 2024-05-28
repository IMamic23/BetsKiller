using BetsKiller.DAL;
using BetsKiller.DAL.AppData;
using BetsKiller.Helper.Constants;
using BetsKiller.Model;
using BetsKiller.ViewModel.Dashboard.Index;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BetsKiller.BL.Dashboard.Index
{
    public class SaveAnalysis : ProcessBase
    {
        #region Private

        private SaveAnalysisViewModel _saveAnalysisViewModel;

        private IAppDataRepository _appDataRepository;

        private Analysis _analysis;

        #endregion

        #region Properties - override

        protected override string _successMessage
        {
            get { return "Analysis saved successfully."; }
        }

        protected override string _failMessage
        {
            get { return "Analysis save failed."; }
        }

        #endregion

        #region Constructors

        public SaveAnalysis(SaveAnalysisViewModel saveAnalysisViewModel)
        {
            this._saveAnalysisViewModel = saveAnalysisViewModel;

            this._appDataRepository = new AppDataRepository();
        }

        #endregion

        #region Methods

        protected override void Process()
        {
            this.ParseData();

            this.SaveData();
        }

        #endregion

        #region Helper methods

        private void ParseData()
        {
            this._analysis = new Analysis();
            this._analysis.Sport_Id = this._appDataRepository.GetAllSports().Single(x => x.Name == SportConst.NBA).Id;
            this._analysis.BetType_Id = this._saveAnalysisViewModel.BetType;
            this._analysis.Bet = this._saveAnalysisViewModel.Pick;

            string betFormatted = this._analysis.Bet.ToString(CultureInfo.InvariantCulture);

            // BetType == total - over
            List<BetType> betTypes = this._appDataRepository.GetAllBetType().ToList();
            if (this._analysis.BetType_Id == betTypes.Single(x => x.Name == BetTypeConst.TOTAL_OVER).Id)
            {
                this._analysis.BetLogicPush = "X+Y=" + betFormatted;
                this._analysis.BetLogicWinLoss = "X+Y>" + betFormatted;
            }
            // BetType == total - under
            else if (this._analysis.BetType_Id == betTypes.Single(x => x.Name == BetTypeConst.TOTAL_UNDER).Id)
            {
                this._analysis.BetLogicPush = "X+Y=" + betFormatted;
                this._analysis.BetLogicWinLoss = "X+Y<" + betFormatted;
            }
            // BetType == team
            else if (this._analysis.BetType_Id == betTypes.Single(x => x.Name == BetTypeConst.TEAM).Id)
            {
                this._analysis.BetLogicPush = null;

                if (this._analysis.Bet == 1)
                {
                    this._analysis.BetLogicWinLoss = "X-Y>0";
                }
                else // this._analysis.Bet == "2"
                {
                    this._analysis.BetLogicWinLoss = "Y-X>0";
                }
            }
            // BetType == team - home - handicap
            else if (this._analysis.BetType_Id == betTypes.Single(x => x.Name == BetTypeConst.TEAM_HOME_HANDICAP).Id)
            {
                this._analysis.BetLogicPush = "X+(" + betFormatted + ")-Y=0";
                this._analysis.BetLogicWinLoss = "X+(" + betFormatted + ")-Y>0";
            }
            // BetType == team - away - handicap
            else if (this._analysis.BetType_Id == betTypes.Single(x => x.Name == BetTypeConst.TEAM_AWAY_HANDICAP).Id)
            {
                this._analysis.BetLogicPush = "Y+(" + betFormatted + ")-X=0";
                this._analysis.BetLogicWinLoss = "Y+(" + betFormatted + ")-X>0";
            }

            this._analysis.BetStatus_Id = this._appDataRepository.GetAllBetStatus().Single(x => x.Name == BetStatusConst.SCHEDULED).Id;
            this._analysis.AnalyseType_Id = this._saveAnalysisViewModel.AnalyseType;
            this._analysis.EventNBA_Id = this._appDataRepository.GetAllScheduleResultsNBA().Single(x => x.EventId == this._saveAnalysisViewModel.Game).Id;
            this._analysis.Created = DateTime.Now;
            this._analysis.Changed = DateTime.Now;
            this._analysis.Description = this._saveAnalysisViewModel.Details;
            this._analysis.OfferTotal = this._saveAnalysisViewModel.OfferTotal;
            this._analysis.OfferLine = this._saveAnalysisViewModel.OfferLine;
            this._analysis.Invested = this._saveAnalysisViewModel.Invested;
            this._analysis.Odd = this._saveAnalysisViewModel.Odd;
            this._analysis.Confidence = this._saveAnalysisViewModel.Confidence;
        }

        private void SaveData()
        {
            this._appDataRepository.SaveAnalysis(new List<Analysis> { this._analysis });
        }

        #endregion
    }
}

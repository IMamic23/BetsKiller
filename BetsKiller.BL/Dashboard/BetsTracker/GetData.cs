using BetsKiller.DAL;
using BetsKiller.DAL.AppData;
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
            // Get and parse NBA bets tracker data
            this.GetParseBetsTrackerNBA();
        }

        #endregion

        #region Helper methods

        private void GetParseBetsTrackerNBA()
        {

        }

        #endregion
    }
}

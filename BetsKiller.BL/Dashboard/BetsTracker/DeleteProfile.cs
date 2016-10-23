using BetsKiller.DAL;
using BetsKiller.DAL.AppData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.BL.Dashboard.BetsTracker
{
    public class DeleteProfile : ProcessBase
    {
        #region Private

        private IAppDataRepository _appDataRepository;

        private string _profileId;

        #endregion

        #region Properties - override

        protected override string _successMessage
        {
            get { return "Bets tracker betting profile deleted successfully."; }
        }

        protected override string _failMessage
        {
            get { return "Bets tracker betting profile deleting failed."; }
        }

        #endregion

        #region Constructors

        public DeleteProfile(string profileId)
        {
            this._appDataRepository = new AppDataRepository();

            this._profileId = profileId;
        }

        #endregion

        #region Methods

        protected override void Process()
        {
            // TODO:
        }

        #endregion
    }
}

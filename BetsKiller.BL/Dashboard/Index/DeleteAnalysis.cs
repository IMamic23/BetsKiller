using BetsKiller.DAL;
using BetsKiller.DAL.AppData;

namespace BetsKiller.BL.Dashboard.Index
{
    public class DeleteAnalysis : ProcessBase
    {
        #region Private

        private int _idAnalysis;

        private IAppDataRepository _appDataRepository;

        #endregion

        #region Properties - override

        protected override string _successMessage
        {
            get { return "Analysis deleted successfully."; }
        }

        protected override string _failMessage
        {
            get { return "Analysis delete failed."; }
        }

        #endregion

        #region Constructors

        public DeleteAnalysis(int idAnalysis)
        {
            this._idAnalysis = idAnalysis;

            this._appDataRepository = new AppDataRepository();
        }

        #endregion

        #region Methods

        protected override void Process()
        {
            this.DeleteData();
        }

        #endregion

        #region Helper methods

        private void DeleteData()
        {
            this._appDataRepository.DeleteAnalysis(this._idAnalysis);
        }

        #endregion
    }
}

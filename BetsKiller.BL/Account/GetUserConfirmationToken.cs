using BetsKiller.DAL;
using BetsKiller.DAL.UserManagement;
using BetsKiller.Model.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.BL.Account
{
    public class GetUserConfirmationToken : ProcessBase
    {
        #region Private

        private int _userId;

        private IUserManagementRepository _userManagementRepository;
        private string _confirmationToken;

        #endregion

        #region Properties

        public string UserConfirmationToken
        {
            get { return this._confirmationToken; }
        }

        #endregion

        #region Properties - override

        protected override string _successMessage
        {
            get { return "Get users data retrived successfully."; }
        }

        protected override string _failMessage
        {
            get { return "Get users data retrive failed."; }
        }

        #endregion

        #region Constructors

        public GetUserConfirmationToken(int userId)
        {
            this._userId = userId;
            this._userManagementRepository = new UserManagementRepository();
        }

        #endregion

        #region Methods

        protected override void Process()
        {
            this.GetData();
        }

        #endregion

        #region Helper

        private void GetData()
        {
            this._confirmationToken = this._userManagementRepository.GetAllMemberships().Single(x => x.UserId == this._userId).ConfirmationToken;
        }

        #endregion
    }
}

using BetsKiller.DAL;
using BetsKiller.DAL.UserManagement;
using BetsKiller.Model.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.BL.UserManagement
{
    public class AddUserActionHistory : ProcessBase
    {
        #region Private

        private int _userId;
        private ActionType _actionType;
        private DateTime _dateTime;

        private IUserManagementRepository _userManagementRepository;

        #endregion

        #region Properties - override

        protected override string _successMessage
        {
            get { return "Saved user action history."; }
        }

        protected override string _failMessage
        {
            get { return "Failed to save user action history."; }
        }

        #endregion

        #region Constructors

        public AddUserActionHistory(int userId, ActionType actionType)
        {
            this._userId = userId;
            this._actionType = actionType;
            this._dateTime = DateTime.Now;
            this._userManagementRepository = new UserManagementRepository();   
        }

        #endregion

        #region Methods

        protected override void Process()
        {
            // Save action history
            this.SaveHistory();
        }

        #endregion

        #region Enums

        public enum ActionType
        {
            LOGIN,
            LOGOUT,
            REGISTER,
            MAIL_CONFIRMATION_RESENT,
            MAIL_CONFIRMED,
            MAIL_PASSWORD_FORGOT_SENT,
            PASSWORD_RESET,
            CHANGED_PASSWORD
        }

        #endregion

        #region Helper

        private void SaveHistory()
        {
            UserActionHistory userActionHistory = new UserActionHistory()
            {
                UserId = this._userId,
                Action = this._actionType.ToString(),
                DateTime = this._dateTime
            };

            this._userManagementRepository.AddUserActionHistoryItem(userActionHistory);
        }

        #endregion
    }
}

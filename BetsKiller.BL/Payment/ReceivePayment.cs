using BetsKiller.DAL;
using BetsKiller.DAL.UserManagement;
using BetsKiller.Helper.Constants;
using BetsKiller.Model.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace BetsKiller.BL.Payment
{
    /// <summary>
    /// Item name: BetsKiller Monthly Subscription
    /// Item ID: 1001
    /// 
    /// Option name 1: "Premium" 25
    /// Option name 2: "Ultimate" 1
    /// 
    /// BetsKiller username:
    /// </summary>
    public class ReceivePayment : ProcessBase
    {
        #region Private

        private string _userEmail;
        private decimal _moneyPaid;
        private string _paymentDescription;

        private IUserManagementRepository _userManagementRepository;

        private string _oldRole;
        private string _newRole;
        private UserProfile _userProfile;

        #endregion

        #region Properties - override

        protected override string _successMessage
        {
            get { return "Payment successfully."; }
        }

        protected override string _failMessage
        {
            get { return "Payment failed."; }
        }

        #endregion

        #region Constructors

        public ReceivePayment(string userEmail, PaymentTypeEnum paymentType, decimal moneyPaid, string paymentDescription)
        {
            this._userManagementRepository = new UserManagementRepository();
            this._userEmail = userEmail;
            
            if (paymentType == PaymentTypeEnum.Premium)
            {
                this._newRole = RolesConst.Premium;
            }
            else // paymentType == PaymentTypeEnum.Ultimate)
            {
                this._newRole = RolesConst.Ultimate;
            }

            this._moneyPaid = moneyPaid;
            this._paymentDescription = paymentDescription;
        }

        #endregion

        #region Methods

        protected override void Process()
        {
            // Check if user exists
            this.CheckUserExists();

            // Get user current role
            this.GetCurrentRole();

            // Set new user role
            this.SetNewRole();

            this.SetNewProfileData();

            // Save payment history
            this.SaveHistory();
        }

        #endregion

        #region Helper methods

        private void CheckUserExists()
        {
            IEnumerable<UserProfile> users = this._userManagementRepository.SelectUserProfiles(this._userEmail, null, null);
            if (users.Count() > 0)
            {
                this._userProfile = users.First();
            }
            else
            {
                throw new Exception("[IMPORTANT] User doesn't exist so wait him to register!");
            }
        }

        private void GetCurrentRole()
        {
            this._oldRole = Roles.GetRolesForUser(this._userEmail).First();
        }

        private void SetNewRole()
        {
            if (this._oldRole != this._newRole)
            {
                Roles.RemoveUserFromRole(this._userEmail, this._oldRole);
                Roles.AddUserToRole(this._userEmail, this._newRole);
            }
        }

        private void SetNewProfileData()
        {
            if (this._userProfile.RoleActiveTo.HasValue)
            {
                if (this._userProfile.RoleActiveTo.Value < DateTime.Now.Date)
                {
                    // Role finished
                    this._userProfile.RoleActiveFrom = DateTime.Now;
                    this._userProfile.RoleActiveTo = DateTime.Now.AddMonths(1);
                }
                else
                {
                    this._userProfile.RoleActiveTo = this._userProfile.RoleActiveTo.Value.AddMonths(1);
                }
            }
            else
            {
                this._userProfile.RoleActiveFrom = DateTime.Now;
                this._userProfile.RoleActiveTo = DateTime.Now.AddMonths(1);
            }

            this._userManagementRepository.EditUserProfile(this._userProfile);
        }

        private void SaveHistory()
        {
            IEnumerable<Role> roles = this._userManagementRepository.SelectRoles();

            UserRoleHistory userRoleHistory = new UserRoleHistory();
            userRoleHistory.Old_RoleId = roles.Single(x => x.RoleName == this._oldRole).RoleId;
            userRoleHistory.New_RoleId = roles.Single(x => x.RoleName == this._newRole).RoleId;

            // Save user history
            userRoleHistory.UserId = this._userProfile.UserId;
            userRoleHistory.Changed = DateTime.Now;
            userRoleHistory.MoneyPaid = this._moneyPaid;
            userRoleHistory.PaymentDescription = this._paymentDescription;

            IEnumerable<PaymentSource> paymentSources = this._userManagementRepository.GetPaymentSources();
            userRoleHistory.PaymentSource_Id = paymentSources.Single(x => x.Name == PaymentSourcesConst.PayPal).Id;

            this._userManagementRepository.AddUserRoleHistoryItem(userRoleHistory);
        }

        #endregion

        #region PaymentType enum

        public enum PaymentTypeEnum
        {
            Premium,
            Ultimate
        }

        #endregion

    }
}
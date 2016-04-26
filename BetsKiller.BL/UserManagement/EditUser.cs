using BetsKiller.DAL;
using BetsKiller.DAL.UserManagement;
using BetsKiller.Helper.Types;
using BetsKiller.Model.UserManagement;
using BetsKiller.ViewModel.UserManagement;
using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using WebMatrix.WebData;

namespace BetsKiller.BL.UserManagement
{
    public class EditUser : ProcessBase
    {
        #region Private

        private UserEditViewModel _userEditViewModel;

        private IUserManagementRepository _userManagementRepository;

        private UserRoleHistory _userRoleHistory;

        #endregion

        #region Properties

        public UserEditViewModel UserEditViewModel
        {
            get { return this._userEditViewModel; }
        }

        #endregion

        #region Properties - override

        protected override string _successMessage
        {
            get { return "Edited user successfully."; }
        }

        protected override string _failMessage
        {
            get { return "Editing user failed."; }
        }

        #endregion

        #region Constructors

        public EditUser(string userName)
        {
            this._userEditViewModel = new UserEditViewModel();

            if (!string.IsNullOrEmpty(userName))
            {
                GetUserProfiles userProfiles = new GetUserProfiles(new UsersProfilesSearchViewModel() { Email = userName });
                userProfiles.Start();

                if (userProfiles.UserProfiles.Count > 0)
                {
                    this._userEditViewModel = new UserEditViewModel();
                    this._userEditViewModel.Email = userProfiles.UserProfiles.First().Email;
                    this._userEditViewModel.FullName = userProfiles.UserProfiles.First().FullName;
                    this._userEditViewModel.RoleActiveFrom = userProfiles.UserProfiles.First().RoleActiveFrom;
                    this._userEditViewModel.RoleActiveTo = userProfiles.UserProfiles.First().RoleActiveTo;

                    GetRoles getUserRoles = new GetRoles(GetRoles.AddPredefinedType.None, this._userEditViewModel.Email);
                    getUserRoles.Start();
                    this._userEditViewModel.Roles = getUserRoles.Roles.Select(x => x.Text).ToList();

                    GetPaymentSources getPaymentSources = new GetPaymentSources();
                    getPaymentSources.Start();
                    this._userEditViewModel.AllPaymentSources = getPaymentSources.PaymentSources;


                    GetRoles getRoles = new GetRoles(GetRoles.AddPredefinedType.None);
                    getRoles.Start();
                    this._userEditViewModel.AllRoles = getRoles.Roles;
                }
            }
        }

        public EditUser(UserEditViewModel userEditViewModel)
        {
            this._userEditViewModel = userEditViewModel;
            this._userManagementRepository = new UserManagementRepository();
            this._userRoleHistory = new UserRoleHistory();
        }

        #endregion

        #region Methods

        protected override void Process()
        {
            if (!this.Validate())
                return;

            this.ParseRoles();

            this.ParsePassword();

            this.ParseUserProfile();
        }

        #endregion

        #region Helpers

        private bool Validate()
        {
            if (this._userEditViewModel == null || string.IsNullOrWhiteSpace(this._userEditViewModel.Email) || !WebSecurity.UserExists(this._userEditViewModel.Email))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void ParseRoles()
        {
            GetRoles getRoles = new GetRoles(GetRoles.AddPredefinedType.None);
            getRoles.Start();
            this._userEditViewModel.AllRoles = getRoles.Roles;

            // Get current roles
            string[] currentRoles = Roles.GetRolesForUser(this._userEditViewModel.Email);

            if (this._userEditViewModel.Roles != null && currentRoles != null)
            {
                // Find roles to remove user from
                foreach (string removeRole in currentRoles.Except(this._userEditViewModel.Roles))
                {
                    if (Roles.IsUserInRole(this._userEditViewModel.Email, removeRole))
                    {
                        Roles.RemoveUserFromRole(this._userEditViewModel.Email, removeRole);
                    }
                }

                // Find new roles to add to user
                foreach (string addRole in this._userEditViewModel.Roles.Except(currentRoles))
                {
                    if (Roles.RoleExists(addRole) && !Roles.IsUserInRole(this._userEditViewModel.Email, addRole))
                    {
                        Roles.AddUserToRole(this._userEditViewModel.Email, addRole);
                    }
                }
            }

            // Save roles history
            IEnumerable<Role> roles = this._userManagementRepository.SelectRoles();
            this._userRoleHistory.Old_RoleId = roles.Single(x => x.RoleName == currentRoles.First()).RoleId;
            this._userRoleHistory.New_RoleId = roles.Single(x => x.RoleName == this._userEditViewModel.Roles.First()).RoleId;
        }

        private void ParsePassword()
        {
            // Set new password if it is provided
            if (!string.IsNullOrWhiteSpace(this._userEditViewModel.Password))
            {
                string newToken = WebSecurity.GeneratePasswordResetToken(this._userEditViewModel.Email);
                WebSecurity.ResetPassword(newToken, this._userEditViewModel.Password);
            }
        }

        private void ParseUserProfile()
        {
            IEnumerable<UserProfile> users = this._userManagementRepository.SelectUserProfiles(this._userEditViewModel.Email, null, null);
            UserProfile user = users.First();

            user.FullName = this._userEditViewModel.FullName;
            user.RoleActiveFrom = Convert.ToDateTime(this._userEditViewModel.RoleActiveFrom);
            user.RoleActiveTo = Convert.ToDateTime(this._userEditViewModel.RoleActiveTo);

            this._userManagementRepository.EditUserProfile(user);

            // Save user history
            this._userRoleHistory.UserId = user.UserId;
            this._userRoleHistory.Changed = DateTime.Now;
            this._userRoleHistory.MoneyPaid = Convert.ToDecimal(this._userEditViewModel.MoneyPaid, CultureInfo.InvariantCulture);
            this._userRoleHistory.PaymentDescription = this._userEditViewModel.PaymentDescription;

            IEnumerable<PaymentSource> paymentSources = this._userManagementRepository.GetPaymentSources();
            this._userRoleHistory.PaymentSource_Id = paymentSources.Single(x => x.Name == this._userEditViewModel.PaymentSources.First()).Id;

            if (this._userRoleHistory.Old_RoleId != this._userRoleHistory.New_RoleId)
            {
                this._userManagementRepository.AddUserRoleHistoryItem(this._userRoleHistory);
            }
        }

        #endregion
    }
}

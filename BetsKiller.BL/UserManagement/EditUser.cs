using BetsKiller.DAL;
using BetsKiller.DAL.UserManagement;
using BetsKiller.Model.UserManagement;
using BetsKiller.ViewModel.UserManagement;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
            get { return _userEditViewModel; }
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
            _userEditViewModel = new UserEditViewModel();

            if (!string.IsNullOrEmpty(userName))
            {
                GetUserProfiles userProfiles = new GetUserProfiles(new UsersProfilesSearchViewModel() { Email = userName });
                userProfiles.Start();

                if (userProfiles.UserProfiles.Count > 0)
                {
                    _userEditViewModel = new UserEditViewModel();
                    _userEditViewModel.Email = userProfiles.UserProfiles.First().Email;
                    _userEditViewModel.FullName = userProfiles.UserProfiles.First().FullName;
                    _userEditViewModel.RoleActiveFrom = userProfiles.UserProfiles.First().RoleActiveFrom;
                    _userEditViewModel.RoleActiveTo = userProfiles.UserProfiles.First().RoleActiveTo;

                    GetRoles getUserRoles = new GetRoles(GetRoles.AddPredefinedType.None, _userEditViewModel.Email);
                    getUserRoles.Start();
                    _userEditViewModel.Roles = getUserRoles.Roles.Select(x => x.Text).ToList();

                    GetPaymentSources getPaymentSources = new GetPaymentSources();
                    getPaymentSources.Start();
                    _userEditViewModel.AllPaymentSources = getPaymentSources.PaymentSources;


                    GetRoles getRoles = new GetRoles(GetRoles.AddPredefinedType.None);
                    getRoles.Start();
                    _userEditViewModel.AllRoles = getRoles.Roles;
                }
            }
        }

        public EditUser(UserEditViewModel userEditViewModel)
        {
            _userEditViewModel = userEditViewModel;
            _userManagementRepository = new UserManagementRepository();
            _userRoleHistory = new UserRoleHistory();
        }

        #endregion

        #region Methods

        protected override void Process()
        {
            if (!Validate())
                return;

            ParseRoles();

            ParsePassword();

            ParseUserProfile();
        }

        #endregion

        #region Helpers

        private bool Validate()
        {
            if (_userEditViewModel == null || string.IsNullOrWhiteSpace(_userEditViewModel.Email) || !WebSecurity.UserExists(_userEditViewModel.Email))
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
            _userEditViewModel.AllRoles = getRoles.Roles;

            // Get current roles
            string[] currentRoles = Roles.GetRolesForUser(_userEditViewModel.Email);

            if (_userEditViewModel.Roles != null && currentRoles != null)
            {
                // Find roles to remove user from
                foreach (string removeRole in currentRoles.Except(_userEditViewModel.Roles))
                {
                    if (Roles.IsUserInRole(_userEditViewModel.Email, removeRole))
                    {
                        Roles.RemoveUserFromRole(_userEditViewModel.Email, removeRole);
                    }
                }

                // Find new roles to add to user
                foreach (string addRole in _userEditViewModel.Roles.Except(currentRoles))
                {
                    if (Roles.RoleExists(addRole) && !Roles.IsUserInRole(_userEditViewModel.Email, addRole))
                    {
                        Roles.AddUserToRole(_userEditViewModel.Email, addRole);
                    }
                }
            }

            // Save roles history
            IEnumerable<Role> roles = _userManagementRepository.SelectRoles();
            _userRoleHistory.Old_RoleId = roles.Single(x => x.RoleName == currentRoles.First()).RoleId;
            _userRoleHistory.New_RoleId = roles.Single(x => x.RoleName == _userEditViewModel.Roles.First()).RoleId;
        }

        private void ParsePassword()
        {
            // Set new password if it is provided
            if (!string.IsNullOrWhiteSpace(_userEditViewModel.Password))
            {
                string newToken = WebSecurity.GeneratePasswordResetToken(_userEditViewModel.Email);
                WebSecurity.ResetPassword(newToken, _userEditViewModel.Password);
            }
        }

        private void ParseUserProfile()
        {
            IEnumerable<UserProfile> users = _userManagementRepository.SelectUserProfiles(_userEditViewModel.Email, null, null);
            UserProfile user = users.First();

            user.FullName = _userEditViewModel.FullName;
            user.RoleActiveFrom = Convert.ToDateTime(_userEditViewModel.RoleActiveFrom);

            if (string.IsNullOrEmpty(_userEditViewModel.RoleActiveTo))
            {
                user.RoleActiveTo = null;
            }
            else
            {
                Convert.ToDateTime(_userEditViewModel.RoleActiveTo);
            }

            _userManagementRepository.EditUserProfile(user);

            // Save user history
            _userRoleHistory.UserId = user.UserId;
            _userRoleHistory.Changed = DateTime.Now;
            _userRoleHistory.MoneyPaid = Convert.ToDecimal(_userEditViewModel.MoneyPaid, CultureInfo.InvariantCulture);
            _userRoleHistory.PaymentDescription = _userEditViewModel.PaymentDescription;

            IEnumerable<PaymentSource> paymentSources = _userManagementRepository.GetPaymentSources();
            _userRoleHistory.PaymentSource_Id = paymentSources.Single(x => x.Name == _userEditViewModel.PaymentSources.First()).Id;

            if (_userRoleHistory.Old_RoleId != _userRoleHistory.New_RoleId)
            {
                _userManagementRepository.AddUserRoleHistoryItem(_userRoleHistory);
            }
        }

        #endregion
    }
}

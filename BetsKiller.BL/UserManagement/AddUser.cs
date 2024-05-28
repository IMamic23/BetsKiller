using BetsKiller.ViewModel.UserManagement;
using System;
using System.Globalization;
using System.Web.Security;
using WebMatrix.WebData;

namespace BetsKiller.BL.UserManagement
{
    public class AddUser : ProcessBase
    {
        #region Private

        private UserAddViewModel _inputUserAddViewModel;

        #endregion

        #region Properties

        public UserAddViewModel UserAddViewModel
        {
            get { return _inputUserAddViewModel; }
        }

        #endregion

        #region Properties - override

        protected override string _successMessage
        {
            get { return "Added user successfully."; }
        }

        protected override string _failMessage
        {
            get { return "Adding user failed."; }
        }

        #endregion

        #region Constructors

        public AddUser()
        {
            _inputUserAddViewModel = new UserAddViewModel();

            LoadRoles();
        }

        public AddUser(UserAddViewModel userAddViewModel)
        {
            _inputUserAddViewModel = userAddViewModel;

            LoadRoles();
        }

        #endregion

        #region Methods

        protected override void Process()
        {
            CreateUser();

            AddRolesToUser();
        }

        #endregion

        #region Helper

        private void LoadRoles()
        {
            GetRoles getRoles = new GetRoles(GetRoles.AddPredefinedType.None);
            getRoles.Start();
            _inputUserAddViewModel.AllRoles = getRoles.Roles;
        }

        private void CreateUser()
        {
            WebSecurity.CreateUserAndAccount(
                _inputUserAddViewModel.Email,
                _inputUserAddViewModel.Password,
                new
                {
                    FullName = _inputUserAddViewModel.FullName,
                    RoleActiveFrom = Convert.ToDateTime(_inputUserAddViewModel.RoleActiveFrom, CultureInfo.InvariantCulture),
                    RoleActiveTo = _inputUserAddViewModel.RoleActiveTo != null ? (object)Convert.ToDateTime(_inputUserAddViewModel.RoleActiveTo, CultureInfo.InvariantCulture) : null
                }
            );
        }

        private void AddRolesToUser()
        {
            foreach (string role in _inputUserAddViewModel.Roles)
            {
                Roles.AddUserToRole(_inputUserAddViewModel.Email, role);
            }
        }

        #endregion
    }
}

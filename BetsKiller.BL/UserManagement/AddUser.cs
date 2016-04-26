using BetsKiller.DAL;
using BetsKiller.DAL.UserManagement;
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
    public class AddUser : ProcessBase
    {
        #region Private

        private UserAddViewModel _inputUserAddViewModel;

        #endregion

        #region Properties

        public UserAddViewModel UserAddViewModel
        {
            get { return this._inputUserAddViewModel; }
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
            this._inputUserAddViewModel = new UserAddViewModel();

            this.LoadRoles();
        }

        public AddUser(UserAddViewModel userAddViewModel)
        {
            this._inputUserAddViewModel = userAddViewModel;

            this.LoadRoles();
        }

        #endregion

        #region Methods

        protected override void Process()
        {
            this.CreateUser();

            this.AddRolesToUser();
        }

        #endregion

        #region Helper

        private void LoadRoles()
        {
            GetRoles getRoles = new GetRoles(GetRoles.AddPredefinedType.None);
            getRoles.Start();
            this._inputUserAddViewModel.AllRoles = getRoles.Roles;
        }

        private void CreateUser()
        {
            WebSecurity.CreateUserAndAccount(
                this._inputUserAddViewModel.Email,
                this._inputUserAddViewModel.Password,
                new
                {
                    FullName = this._inputUserAddViewModel.FullName,
                    RoleActiveFrom = Convert.ToDateTime(this._inputUserAddViewModel.RoleActiveFrom, CultureInfo.InvariantCulture),
                    RoleActiveTo = this._inputUserAddViewModel.RoleActiveTo != null ? (object)Convert.ToDateTime(this._inputUserAddViewModel.RoleActiveTo, CultureInfo.InvariantCulture) : null
                }
            );
        }

        private void AddRolesToUser()
        {
            foreach (string role in this._inputUserAddViewModel.Roles)
            {
                Roles.AddUserToRole(this._inputUserAddViewModel.Email, role);
            }
        }

        #endregion
    }
}

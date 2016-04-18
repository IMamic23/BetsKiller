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
    public class AddUser
    {
        #region Private

        private UserAddViewModel _inputUserAddViewModel;

        private Status _status;

        #endregion

        #region Properties

        public UserAddViewModel UserAddViewModel
        {
            get { return this._inputUserAddViewModel; }
        }

        public Status ProcessStatus
        {
            get { return this._status; }
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

        public void Start()
        {
            try
            {
                this.CreateUser();

                this.AddRolesToUser();

                this._status = Status.Success;
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex, "BL - AddUser");
                this._status = Status.Error;
            }
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
                new { 
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

        #region Enums

        public enum Status
        {
            Error,
            Success
        }

        #endregion
    }
}

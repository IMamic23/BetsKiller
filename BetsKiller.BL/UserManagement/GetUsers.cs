using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetsKiller.ViewModel.UserManagement;
using BetsKiller.DAL;
using BetsKiller.DAL.UserManagement;
using System.Web.Mvc;
using BetsKiller.Model.UserManagement;
using NLog;
using BetsKiller.Helper.Constants;

namespace BetsKiller.BL.UserManagement
{
    public class GetUsers
    {
        #region Private

        private UsersListViewModel _usersListViewModel;

        private Status _status;

        #endregion

        #region Properties

        public UsersListViewModel UsersListViewModels 
        {
            get { return this._usersListViewModel; }
        }

        public Status ProcessStatus
        {
            get { return this._status; }
        }

        #endregion

        #region Constructors

        public GetUsers()
        {
            this._usersListViewModel = new UsersListViewModel();
            this._usersListViewModel.UserProfileSearchViewModel = new UsersProfilesSearchViewModel();
        }

        public GetUsers(UsersProfilesSearchViewModel searchViewModel)
        {
            this._usersListViewModel = new UsersListViewModel();
            this._usersListViewModel.UserProfileSearchViewModel = searchViewModel;
        }

        #endregion

        #region Methods

        public void Start()
        {
            try
            {
                this.GetRolesData();

                this.GetUserProfiles();

                this._status = Status.Success;
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex, "BL - GetUsers");
                this._status = Status.Error;
            }
        }

        #endregion

        #region Helper

        private void GetRolesData()
        {
            GetRoles getRoles = new GetRoles(GetRoles.AddPredefinedType.Any);
            getRoles.Start();

            this._usersListViewModel.AllRoles = getRoles.Roles;
        }

        private void GetUserProfiles()
        {
            GetUserProfiles getUserProfiles = new GetUserProfiles(this._usersListViewModel.UserProfileSearchViewModel);
            getUserProfiles.Start();

            this._usersListViewModel.UserProfileViewModels = getUserProfiles.UserProfiles.Where(x => x.Email != UsersConst.AdminUsername).ToList();
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

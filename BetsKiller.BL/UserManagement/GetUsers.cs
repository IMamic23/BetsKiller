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
    public class GetUsers : ProcessBase
    {
        #region Private

        private UsersListViewModel _usersListViewModel;

        #endregion

        #region Properties

        public UsersListViewModel UsersListViewModels
        {
            get { return this._usersListViewModel; }
        }

        #endregion

        #region Properties - override

        protected override string _successMessage
        {
            get { return "Get users successfully."; }
        }

        protected override string _failMessage
        {
            get { return "Get users failed."; }
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

        protected override void Process()
        {
            this.GetRolesData();

            this.GetUserProfiles();
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
    }
}

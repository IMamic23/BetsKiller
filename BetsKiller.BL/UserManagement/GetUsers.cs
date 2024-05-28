using System.Linq;
using BetsKiller.ViewModel.UserManagement;
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
            get { return _usersListViewModel; }
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
            _usersListViewModel = new UsersListViewModel();
            _usersListViewModel.UserProfileSearchViewModel = new UsersProfilesSearchViewModel();
        }

        public GetUsers(UsersProfilesSearchViewModel searchViewModel)
        {
            _usersListViewModel = new UsersListViewModel();
            _usersListViewModel.UserProfileSearchViewModel = searchViewModel;
        }

        #endregion

        #region Methods

        protected override void Process()
        {
            GetRolesData();

            GetUserProfiles();
        }

        #endregion

        #region Helper

        private void GetRolesData()
        {
            GetRoles getRoles = new GetRoles(GetRoles.AddPredefinedType.Any);
            getRoles.Start();

            _usersListViewModel.AllRoles = getRoles.Roles;
        }

        private void GetUserProfiles()
        {
            GetUserProfiles getUserProfiles = new GetUserProfiles(_usersListViewModel.UserProfileSearchViewModel);
            getUserProfiles.Start();

            _usersListViewModel.UserProfileViewModels = getUserProfiles.UserProfiles.Where(x => x.Email != UsersConst.AdminUsername).ToList();
        }

        #endregion
    }
}

using BetsKiller.DAL;
using BetsKiller.DAL.UserManagement;
using BetsKiller.Helper.Types;
using BetsKiller.Model.UserManagement;
using BetsKiller.ViewModel.UserManagement;
using System.Collections.Generic;
using System.Linq;

namespace BetsKiller.BL.UserManagement
{
    public class GetUserProfiles : ProcessBase
    {
        #region Private

        private UsersProfilesSearchViewModel _searchViewModel;

        private List<UserProfileViewModel> _userProfilesViewModel;
        private IUserManagementRepository _userManagementRepository;

        #endregion

        #region Properties

        public List<UserProfileViewModel> UserProfiles
        {
            get { return _userProfilesViewModel; }
        }

        #endregion

        #region Properties - override

        protected override string _successMessage
        {
            get { return "Get user profiles successfully."; }
        }

        protected override string _failMessage
        {
            get { return "Get user profiles failed."; }
        }

        #endregion

        #region Constructors

        public GetUserProfiles(UsersProfilesSearchViewModel searchViewModel)
        {
            _searchViewModel = searchViewModel;
            _userProfilesViewModel = new List<UserProfileViewModel>();
            _userManagementRepository = new UserManagementRepository();
        }

        #endregion

        #region Methods

        protected override void Process()
        {
            GetData();
        }

        #endregion

        #region Helper

        private void GetData()
        {
            IEnumerable<UserProfile> users = _userManagementRepository.SelectUserProfiles(_searchViewModel.Email,
                                                                           _searchViewModel.RoleName,
                                                                           _searchViewModel.ResultLimit);

            foreach (UserProfile userProfile in users)
            {
                UserProfileViewModel userProfileViewModel = new UserProfileViewModel()
                {
                    UserId = userProfile.UserId,
                    Email = userProfile.UserName,
                    FullName = userProfile.FullName,
                    RoleActiveFrom = TypeDateTime.ParseDateTime(userProfile.RoleActiveFrom),
                    RoleActiveTo = TypeDateTime.ParseDateTime(userProfile.RoleActiveTo),
                    Roles = string.Join(", ", userProfile.Roles.OrderBy(x => x.RoleName).Select(x => x.RoleName))
                };

                _userProfilesViewModel.Add(userProfileViewModel);
            }
        }

        #endregion
    }
}

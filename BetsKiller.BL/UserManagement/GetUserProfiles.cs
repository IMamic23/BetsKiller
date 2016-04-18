using BetsKiller.DAL;
using BetsKiller.DAL.UserManagement;
using BetsKiller.Model.UserManagement;
using BetsKiller.ViewModel.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.BL.UserManagement
{
    public class GetUserProfiles
    {
        #region Private

        private UsersProfilesSearchViewModel _searchViewModel;

        private List<UserProfileViewModel> _userProfilesViewModel;

        #endregion

        #region Properties

        public List<UserProfileViewModel> UserProfiles
        {
            get { return this._userProfilesViewModel; }
        }

        #endregion

        #region Constructors

        public GetUserProfiles(UsersProfilesSearchViewModel searchViewModel)
        {
            this._searchViewModel = searchViewModel;
            this._userProfilesViewModel = new List<UserProfileViewModel>();
        }

        #endregion

        #region Methods

        public void Start()
        {
            this.GetData();
        }

        #endregion

        #region Helper

        private void GetData()
        {
            using (IUserManagementRepository repository = new UserManagementRepository())
            {
                IEnumerable<UserProfile> users = repository.SelectUserProfiles(this._searchViewModel.Email,
                                                                               this._searchViewModel.RoleName,
                                                                               this._searchViewModel.ResultLimit);

                foreach (UserProfile userProfile in users)
                {
                    UserProfileViewModel userProfileViewModel = new UserProfileViewModel()
                    {
                        UserId = userProfile.UserId,
                        Email = userProfile.UserName,
                        FullName = userProfile.FullName,
                        RoleActiveFrom = userProfile.RoleActiveFrom.ToShortDateString(),
                        RoleActiveTo = userProfile.RoleActiveTo.HasValue ? userProfile.RoleActiveTo.Value.ToShortDateString() : string.Empty,
                        Roles = string.Join(", ", userProfile.Roles.OrderBy(x => x.RoleName).Select(x => x.RoleName))
                    };

                    this._userProfilesViewModel.Add(userProfileViewModel);
                }
            }
        }

        #endregion
    }
}

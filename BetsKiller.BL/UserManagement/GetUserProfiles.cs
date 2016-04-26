using BetsKiller.DAL;
using BetsKiller.DAL.UserManagement;
using BetsKiller.Helper.Types;
using BetsKiller.Model.UserManagement;
using BetsKiller.ViewModel.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            get { return this._userProfilesViewModel; }
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
            this._searchViewModel = searchViewModel;
            this._userProfilesViewModel = new List<UserProfileViewModel>();
            this._userManagementRepository = new UserManagementRepository();
        }

        #endregion

        #region Methods

        protected override void Process()
        {
            this.GetData();
        }

        #endregion

        #region Helper

        private void GetData()
        {
            IEnumerable<UserProfile> users = this._userManagementRepository.SelectUserProfiles(this._searchViewModel.Email,
                                                                           this._searchViewModel.RoleName,
                                                                           this._searchViewModel.ResultLimit);

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

                this._userProfilesViewModel.Add(userProfileViewModel);
            }
        }

        #endregion
    }
}

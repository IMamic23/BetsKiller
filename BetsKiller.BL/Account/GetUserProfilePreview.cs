using BetsKiller.BL.UserManagement;
using BetsKiller.ViewModel.Account;
using BetsKiller.ViewModel.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.BL.Account
{
    public class GetUserProfilePreview
    {
        #region Private

        private string _username;
        
        private UserProfilePreviewViewModel _userProfilePreviewViewModel;

        #endregion

        #region Properties

        public UserProfilePreviewViewModel UserProfilePreviewViewModel
        {
            get { return this._userProfilePreviewViewModel; }
        }

        #endregion

        #region Constructors

        public GetUserProfilePreview(string username)
        {
            this._username = username;
            this._userProfilePreviewViewModel = new UserProfilePreviewViewModel();
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
            GetUserProfiles getUserProfiles = new GetUserProfiles(new ViewModel.UserManagement.UsersProfilesSearchViewModel()
            {
                Email = this._username,
                ResultLimit = null,
                RoleName = null
            });

            getUserProfiles.Start();

            UserProfileViewModel userProfile = getUserProfiles.UserProfiles.First();

            this._userProfilePreviewViewModel.FullName = userProfile.FullName;
            this._userProfilePreviewViewModel.Email = userProfile.Email;
            this._userProfilePreviewViewModel.RoleActive = userProfile.Roles.ToString().ToUpper();
            this._userProfilePreviewViewModel.RoleActiveUntil = string.IsNullOrEmpty(userProfile.RoleActiveTo) ? "UNLIMITED" : userProfile.RoleActiveTo;
        }

        #endregion
    }
}

using System.ComponentModel.DataAnnotations;

namespace BetsKiller.ViewModel.UserManagement
{
    public class UsersProfilesSearchViewModel
    {
        #region Properties

        [EmailAddress]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        public string Email { get; set; }

        public string RoleName { get; set; }

        public int? ResultLimit { get; set; }

        #endregion

        #region Constructor

        public UsersProfilesSearchViewModel()
        {
            // Result limit for user profile search, if it is not defined.
            this.ResultLimit = 100;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BetsKiller.ViewModel.UserManagement
{
    public class UsersListViewModel
    {
        #region Properties

        public UsersProfilesSearchViewModel UserProfileSearchViewModel { get; set; }
        public List<SelectListItem> AllRoles { get; set; }
        public List<UserProfileViewModel> UserProfileViewModels { get; set; }

        #endregion

        #region Constructors

        public UsersListViewModel()
        {
            this.UserProfileSearchViewModel = new UsersProfilesSearchViewModel();
        }

        #endregion
    }
}

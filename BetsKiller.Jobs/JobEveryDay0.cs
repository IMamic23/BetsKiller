using BetsKiller.DAL;
using BetsKiller.DAL.UserManagement;
using BetsKiller.Helper.Constants;
using BetsKiller.Model.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.Jobs
{
    public class JobEveryDay0 : JobBase
    {
        #region Private

        private IUserManagementRepository _repository;

        #endregion

        #region Constructors

        public JobEveryDay0()
            : base()
        {
            this._repository = new UserManagementRepository();
        }

        #endregion

        #region Methods

        protected override void Job()
        {
            // Get all user profiles
            IEnumerable<UserProfile> userProfiles = this._repository.SelectUserProfiles(null, null, null).ToList();
            IEnumerable<Role> roles = this._repository.SelectRoles().ToList();

            foreach (UserProfile userProfile in userProfiles)
            {
                if (userProfile.RoleActiveTo.HasValue 
                    && userProfile.RoleActiveTo.Value.Date < DateTime.Now.Date
                    && userProfile.Roles.Where(x => x.RoleName  == RolesConst.Admin || x.RoleName == RolesConst.ContentManager || x.RoleName == RolesConst.Free).Count() == 0)
                {
                    // Set user role to FREE 
                    userProfile.Roles = new List<Role>();
                    userProfile.Roles.Add(roles.Single(x => x.RoleName == RolesConst.Free));

                    // Set user role activity unlimited
                    userProfile.RoleActiveTo = null;

                    // Update user
                    this._repository.EditUserProfile(userProfile);
                }
            }
        }

        #endregion
    }
}

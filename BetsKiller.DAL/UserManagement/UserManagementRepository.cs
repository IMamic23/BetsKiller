using BetsKiller.Model.UserManagement;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.DAL.UserManagement
{
    public class UserManagementRepository : IUserManagementRepository
    {
        #region Private

        /// <summary>
        /// Disposing state.
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// UserManagement DB context.
        /// </summary>
        private readonly UserManagementContext _context;

        #endregion

        #region Constructors

        public UserManagementRepository()
        {
            this._context = new UserManagementContext();
        }

        #endregion

        #region Methods

        public IEnumerable<UserProfile> SelectUserProfiles(string userName, string roleName, int? resultLimit)
        {
            IQueryable<UserProfile> users = this._context.UserProfiles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(userName))
            {
                users = users.Where(user => user.UserName.ToUpper().Contains(userName.ToUpper()));
            }

            if (!string.IsNullOrWhiteSpace(roleName))
            {
                users = users.Where(user => user.Roles.Select(role => role.RoleName).Contains(roleName));
            }

            if (resultLimit != null)
            {
                return users.Take((int)resultLimit);
            }
            else
            {
                return users;
            }
        }

        public IEnumerable<Role> SelectRoles()
        {
            return this._context.Roles.OrderBy(x => x.RoleName);
        }
        
        public void EditUserProfile(UserProfile userProfile)
        {
            UserProfile entity = this._context.UserProfiles.FirstOrDefault(x => x.UserId == userProfile.UserId);
            this._context.Entry(entity).CurrentValues.SetValues(userProfile);

            this._context.SaveChanges();
        }

        public void AddUserRoleHistoryItem(UserRoleHistory userRoleHistory)
        {
            this._context.UsersRolesHistory.Add(userRoleHistory);

            this._context.SaveChanges();
        }

        public IEnumerable<PaymentSource> GetPaymentSources()
        {
            return this._context.PaymentSources;
        }

        public IQueryable<Membership> GetAllMemberships()
        {
            return this._context.Memberships;
        }

        public void AddUserActionHistoryItem(UserActionHistory userActionHistory)
        {
            this._context.UsersActionsHistory.Add(userActionHistory);

            this._context.SaveChanges();
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Disposing method.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    this._context.Dispose();
                }
            }
            this._disposed = true;
        }

        /// <summary>
        /// Disposing method.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}

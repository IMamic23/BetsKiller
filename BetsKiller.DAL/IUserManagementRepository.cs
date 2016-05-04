using BetsKiller.Model.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.DAL
{
    public interface IUserManagementRepository : IDisposable
    {
        IEnumerable<UserProfile> SelectUserProfiles(string userName, string roleName, int? resultLimit);

        IEnumerable<Role> SelectRoles();

        void EditUserProfile(UserProfile userProfile);

        void AddUserRoleHistoryItem(UserRoleHistory userRoleHistory);

        IEnumerable<PaymentSource> GetPaymentSources();

        IQueryable<Membership> GetAllMemberships();
    }
}

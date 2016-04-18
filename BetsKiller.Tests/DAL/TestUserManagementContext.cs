using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BetsKiller.DAL;
using BetsKiller.DAL.UserManagement;
using BetsKiller.Model.UserManagement;
using System.Collections.Generic;
using System.Linq;

namespace BetsKiller.Tests.DAL
{
    [TestClass]
    public class TestUserManagementContext
    {
        #region Test data

        private UserProfile _adminUser = new UserProfile()
        {
            UserId = 1,
            UserName = "betsadminkiller",
            Roles = new List<Role>()
            {
                new Role()
                {
                    RoleId = 1,
                    RoleName = "Admin"
                }
            }
        };

        #endregion

        [TestMethod]
        public void GetAllUsers()
        {
            List<UserProfile> expected;
            using (IUserManagementRepository repository = new UserManagementRepository())
            {
                expected = repository.SelectUserProfiles(null, null, null).ToList();
            }

            if (expected == null || expected.Count == 0)
                Assert.Fail();
        }

        [TestMethod]
        public void GetSpecificUserByUserName()
        {
            List<UserProfile> expected;
            using (IUserManagementRepository repository = new UserManagementRepository())
            {
                expected = repository.SelectUserProfiles(this._adminUser.UserName, null, null).ToList();
            }

            if (expected.FirstOrDefault().UserId != this._adminUser.UserId)
                Assert.Fail();
        }

        [TestMethod]
        public void GetSpecificUserByRoleName()
        {
            List<UserProfile> expected;
            using (IUserManagementRepository repository = new UserManagementRepository())
            {
                expected = repository.SelectUserProfiles(null, this._adminUser.Roles.FirstOrDefault().RoleName, null).ToList();
            }

            if (expected.FirstOrDefault().UserId != this._adminUser.UserId)
                Assert.Fail();
        }

        [TestMethod]
        public void GetSpecificUserResultLimit()
        {
            List<UserProfile> expected;
            using (IUserManagementRepository repository = new UserManagementRepository())
            {
                expected = repository.SelectUserProfiles(null, null, 1).ToList();
            }

            if (expected.Count != 1)
                Assert.Fail();
        }
    }
}

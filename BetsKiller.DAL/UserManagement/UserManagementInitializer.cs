using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BetsKiller.Model.UserManagement;

namespace BetsKiller.DAL.UserManagement
{
    public class UserManagementInitializer : CreateDatabaseIfNotExists<UserManagementContext>
    {
        #region Protected

        /// <summary>
        /// Adding test data when DB is creating.
        /// </summary>
        protected override void Seed(UserManagementContext context)
        {
            #region Roles

            context.Roles.AddRange(new List<Role>()
            {
                new Role()
                {
                    RoleName = "Free"
                },
                new Role()
                {
                    RoleName = "Premium"
                },
                new Role()
                {
                    RoleName = "Ultimate"
                },
                new Role()
                {
                    RoleName = "Admin"
                },
                new Role()
                {
                    RoleName = "ContentManager"
                }
            });

            #endregion

            #region Payment sources

            context.PaymentSources.AddRange(new List<PaymentSource>()
            {
                new PaymentSource()
                {
                    Name = "PayPal",
                    Description = "PayPal subscription allowing client to pay a set fee monthly to access service."
                }
            });

            #endregion

            base.Seed(context);
        }

        #endregion
    }
}

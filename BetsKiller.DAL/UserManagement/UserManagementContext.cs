using BetsKiller.Model.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BetsKiller.DAL.UserManagement
{
    public class UserManagementContext : DbContext
    {
        #region Constructors

        static UserManagementContext()
        {
            // Do not attempt to create database
            //Database.SetInitializer<UserManagementContext>(null);
            
            // DB initialization - creating DB from domain model if not exists.
            Database.SetInitializer<UserManagementContext>(new UserManagementInitializer());
            using (UserManagementContext dbContext = new UserManagementContext())
                dbContext.Database.Initialize(false);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Tables in DB
        /// </summary>
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<OAuthMembership> OAuthMemberships { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoleHistory> UsersRolesHistory { get; set; }
        public DbSet<PaymentSource> PaymentSources { get; set; }

        #endregion

        #region Override

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Table names are not plural in DB, remove the convention
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Set 1:N relationship between UserProfile and Role
            modelBuilder.Entity<UserProfile>()
                .HasMany(x => x.Roles)
                .WithMany(x => x.UserProfiles)
                .Map(m =>
                {
                    m.MapLeftKey("UserId");
                    m.MapRightKey("RoleId");
                    m.ToTable("webpages_UsersInRoles");
                });

            // Setting precision of decimal numbers
            modelBuilder.Entity<UserRoleHistory>().Property(x => x.MoneyPaid).HasPrecision(18, 4);

            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}

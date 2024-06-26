﻿using BetsKiller.Model;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BetsKiller.DAL.AppData
{
    public class AppDataContext : DbContext
    {
        #region Constructors

        static AppDataContext()
        {
            using (AppDataContext dbContext = new AppDataContext())
            {
                if (dbContext.Database.Exists() && !dbContext.Database.CompatibleWithModel(false))
                {
                    // Update all pending migrations to database
                    DbMigrator dbMigrator = new DbMigrator(new BetsKiller.DAL.Migrations.Configuration());
                    foreach (string migration in dbMigrator.GetPendingMigrations())
                    {
                        dbMigrator.Update(migration);
                    }
                }
                else
                {
                    // Create DB model if not exists
                    Database.SetInitializer<AppDataContext>(new AppDataInitializer());
                }
                
                dbContext.Database.Initialize(false);
            }
        }

        #endregion

        #region Properties

        public DbSet<Sport> Sport { get; set; }
        public DbSet<BetType> BetType { get; set; }
        public DbSet<BetStatus> BetStatus { get; set; }
        public DbSet<AnalyseType> AnalyseType { get; set; }
        public DbSet<Analysis> Analysis { get; set; }
        public DbSet<TeamsNBA> TeamsNBA { get; set; }
        public DbSet<AnalysisProfit> AnalysisProfit { get; set; }
        public DbSet<LeadersNBA> LeadersNBA { get; set; }
        public DbSet<LeadersNBACategories> LeadersNBACategories { get; set; }
        public DbSet<TeamsNBANames> TeamsNBANames { get; set; }
        public DbSet<RostersNBA> RostersNBA { get; set; }
        public DbSet<DraftsNBA> DraftsNBA { get; set; }
        public DbSet<ScheduleResultsNBA> ScheduleResultsNBA { get; set; }
        public DbSet<NewsFeed> NewsFeed { get; set; }
        public DbSet<NewsPublish> NewsPublish { get; set; }
        public DbSet<Injuries> Injuries { get; set; }
        public DbSet<PowerRankingsNBA> PowerRankingsNBA { get; set; }
        public DbSet<Standings> Standings { get; set; }

        #endregion

        #region Override

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Table names are not plural in DB, remove the convention
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Setting precision of decimal numbers
            modelBuilder.Entity<AnalysisProfit>().Property(x => x.Invested).HasPrecision(18, 4);
            modelBuilder.Entity<AnalysisProfit>().Property(x => x.ROI).HasPrecision(18, 4);
            modelBuilder.Entity<AnalysisProfit>().Property(x => x.Profit).HasPrecision(18, 4);
            modelBuilder.Entity<Analysis>().Property(x => x.OfferLine).HasPrecision(18, 4);
            modelBuilder.Entity<Analysis>().Property(x => x.OfferTotal).HasPrecision(18, 4);
            modelBuilder.Entity<Analysis>().Property(x => x.Invested).HasPrecision(18, 4);
            modelBuilder.Entity<Analysis>().Property(x => x.Odd).HasPrecision(18, 4);
            modelBuilder.Entity<Analysis>().Property(x => x.Profit).HasPrecision(18, 4);
            modelBuilder.Entity<Analysis>().Property(x => x.Confidence).HasPrecision(18, 4);
            modelBuilder.Entity<LeadersNBA>().Property(x => x.Value).HasPrecision(18, 4);
            modelBuilder.Entity<RostersNBA>().Property(x => x.HeightIn).HasPrecision(18, 4);
            modelBuilder.Entity<RostersNBA>().Property(x => x.HeightCm).HasPrecision(18, 4);
            modelBuilder.Entity<RostersNBA>().Property(x => x.HeightM).HasPrecision(18, 4);
            modelBuilder.Entity<RostersNBA>().Property(x => x.WeightLb).HasPrecision(18, 4);
            modelBuilder.Entity<RostersNBA>().Property(x => x.WeightKg).HasPrecision(18, 4);
            modelBuilder.Entity<DraftsNBA>().Property(x => x.HeightIn).HasPrecision(18, 4);
            modelBuilder.Entity<DraftsNBA>().Property(x => x.HeightCm).HasPrecision(18, 4);
            modelBuilder.Entity<DraftsNBA>().Property(x => x.HeightM).HasPrecision(18, 4);
            modelBuilder.Entity<DraftsNBA>().Property(x => x.WeightLb).HasPrecision(18, 4);
            modelBuilder.Entity<DraftsNBA>().Property(x => x.WeightKg).HasPrecision(18, 4);
            modelBuilder.Entity<Standings>().Property(x => x.GamesBack).HasPrecision(18, 4);

            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}

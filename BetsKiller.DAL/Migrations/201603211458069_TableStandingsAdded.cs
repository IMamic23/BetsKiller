namespace BetsKiller.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableStandingsAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Standings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rank = c.Int(nullable: false),
                        OrdinalRank = c.String(),
                        Won = c.Int(nullable: false),
                        Lost = c.Int(nullable: false),
                        AwayWon = c.Int(nullable: false),
                        AwayLost = c.Int(nullable: false),
                        Conference = c.String(),
                        ConferenceWon = c.Int(nullable: false),
                        ConferenceLost = c.Int(nullable: false),
                        Division = c.String(),
                        GamesBack = c.Decimal(nullable: false, precision: 18, scale: 4),
                        GamesPlayed = c.Int(nullable: false),
                        HomeWon = c.Int(nullable: false),
                        HomeLost = c.Int(nullable: false),
                        LastFive = c.String(),
                        LastTen = c.String(),
                        PlayoffSeed = c.Int(nullable: false),
                        PointDifferential = c.Int(nullable: false),
                        PointDifferentialPerGame = c.String(),
                        PointsAgainst = c.Int(nullable: false),
                        PointsFor = c.Int(nullable: false),
                        PointsAllowedPerGame = c.String(),
                        PointsScoredPerGame = c.String(),
                        StreakTotal = c.Int(nullable: false),
                        Streak = c.String(),
                        StreakType = c.String(),
                        WinPercentage = c.String(),
                        Sport_Id = c.Int(),
                        TeamNBA_Id = c.Int(),
                        TeamMLB_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sport", t => t.Sport_Id)
                .ForeignKey("dbo.TeamsNBA", t => t.TeamNBA_Id)
                .Index(t => t.Sport_Id)
                .Index(t => t.TeamNBA_Id);
            
            DropColumn("dbo.TeamsNBANames", "TeamLogoSmallPath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TeamsNBANames", "TeamLogoSmallPath", c => c.String());
            DropForeignKey("dbo.Standings", "TeamNBA_Id", "dbo.TeamsNBA");
            DropForeignKey("dbo.Standings", "Sport_Id", "dbo.Sport");
            DropIndex("dbo.Standings", new[] { "TeamNBA_Id" });
            DropIndex("dbo.Standings", new[] { "Sport_Id" });
            DropTable("dbo.Standings");
        }
    }
}

namespace BetsKiller.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnalyseResultSolve",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AnalyseType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Analysis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false),
                        Changed = c.DateTime(),
                        Description = c.String(),
                        OfferTotal = c.String(),
                        OfferLine = c.String(),
                        Bet = c.String(),
                        BetLogicPush = c.String(),
                        BetLogicWinLoss = c.String(),
                        Invested = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Odd = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Profit = c.Decimal(precision: 18, scale: 2),
                        Result = c.String(),
                        Confidence = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Sport_Id = c.Int(),
                        BetTypeTeam_Id = c.Int(),
                        BetStatus_Id = c.Int(),
                        AnalyseType_Id = c.Int(),
                        AnalyseResultSolve_Id = c.Int(),
                        EventNBA_Id = c.Int(),
                        EventMLB_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AnalyseResultSolve", t => t.AnalyseResultSolve_Id)
                .ForeignKey("dbo.AnalyseType", t => t.AnalyseType_Id)
                .ForeignKey("dbo.BetStatus", t => t.BetStatus_Id)
                .ForeignKey("dbo.BetType", t => t.BetTypeTeam_Id)
                .ForeignKey("dbo.ScheduleResultsNBA", t => t.EventNBA_Id)
                .ForeignKey("dbo.Sport", t => t.Sport_Id)
                .Index(t => t.Sport_Id)
                .Index(t => t.BetTypeTeam_Id)
                .Index(t => t.BetStatus_Id)
                .Index(t => t.AnalyseType_Id)
                .Index(t => t.AnalyseResultSolve_Id)
                .Index(t => t.EventNBA_Id);
            
            CreateTable(
                "dbo.BetStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BetType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ScheduleResultsNBA",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventId = c.String(),
                        EventStatus = c.String(),
                        EventStartDateTime = c.DateTime(),
                        EventSeasonType = c.String(),
                        TeamEventNumberInSeason = c.Int(nullable: false),
                        TeamEventLocationType = c.String(),
                        TeamEventResult = c.String(),
                        TeamPointsScored = c.Int(nullable: false),
                        TeamPeriodScores = c.String(),
                        TeamEventsWon = c.Int(nullable: false),
                        TeamEventsLost = c.Int(nullable: false),
                        OpponentPointsScored = c.Int(nullable: false),
                        OpponentPeriodScores = c.String(),
                        OpponentEventsWon = c.Int(nullable: false),
                        OpponentEventsLost = c.Int(nullable: false),
                        SiteCapacity = c.Int(nullable: false),
                        SiteSurface = c.String(),
                        SiteName = c.String(),
                        SiteState = c.String(),
                        SiteCity = c.String(),
                        Team_Id = c.Int(),
                        Opponent_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeamsNBA", t => t.Opponent_Id)
                .ForeignKey("dbo.TeamsNBA", t => t.Team_Id)
                .Index(t => t.Team_Id)
                .Index(t => t.Opponent_Id);
            
            CreateTable(
                "dbo.TeamsNBA",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Abbreviation = c.String(),
                        Active = c.Boolean(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Conference = c.String(),
                        Division = c.String(),
                        SiteName = c.String(),
                        City = c.String(),
                        State = c.String(),
                        BothSU = c.String(),
                        BothATS = c.String(),
                        BothOU = c.String(),
                        BothAvgLine = c.String(),
                        BothAvgTotal = c.String(),
                        BothTeamFG = c.String(),
                        BothTeamFGP = c.String(),
                        BothTeamFT = c.String(),
                        BothTeamFTP = c.String(),
                        BothTeam3s = c.String(),
                        BothTeam3sP = c.String(),
                        BothTeamBLKS = c.String(),
                        BothTeamORBND = c.String(),
                        BothTeamRBND = c.String(),
                        BothTeamFouls = c.String(),
                        BothTeamAST = c.String(),
                        BothTeamTOvers = c.String(),
                        BothTeamQ1 = c.String(),
                        BothTeamQ2 = c.String(),
                        BothTeamQ3 = c.String(),
                        BothTeamQ4 = c.String(),
                        BothTeamFinal = c.String(),
                        BothOppFG = c.String(),
                        BothOppFGP = c.String(),
                        BothOppFT = c.String(),
                        BothOppFTP = c.String(),
                        BothOpp3s = c.String(),
                        BothOpp3sP = c.String(),
                        BothOppBLKS = c.String(),
                        BothOppORBND = c.String(),
                        BothOppRBND = c.String(),
                        BothOppFouls = c.String(),
                        BothOppAST = c.String(),
                        BothOppTOvers = c.String(),
                        BothOppQ1 = c.String(),
                        BothOppQ2 = c.String(),
                        BothOppQ3 = c.String(),
                        BothOppQ4 = c.String(),
                        BothOppFinal = c.String(),
                        BothFGA = c.String(),
                        BothFGM = c.String(),
                        BothTPA = c.String(),
                        BothTPM = c.String(),
                        BothTO = c.String(),
                        BothAST = c.String(),
                        BothFGP = c.String(),
                        BothTPP = c.String(),
                        BothFTP = c.String(),
                        BothPFT = c.String(),
                        BothPTP = c.String(),
                        BothWP = c.String(),
                        HomeSU = c.String(),
                        HomeATS = c.String(),
                        HomeOU = c.String(),
                        HomeAvgLine = c.String(),
                        HomeAvgTotal = c.String(),
                        HomeTeamFG = c.String(),
                        HomeTeamFGP = c.String(),
                        HomeTeamFT = c.String(),
                        HomeTeamFTP = c.String(),
                        HomeTeam3s = c.String(),
                        HomeTeam3sP = c.String(),
                        HomeTeamBLKS = c.String(),
                        HomeTeamORBND = c.String(),
                        HomeTeamRBND = c.String(),
                        HomeTeamFouls = c.String(),
                        HomeTeamAST = c.String(),
                        HomeTeamTOvers = c.String(),
                        HomeTeamQ1 = c.String(),
                        HomeTeamQ2 = c.String(),
                        HomeTeamQ3 = c.String(),
                        HomeTeamQ4 = c.String(),
                        HomeTeamFinal = c.String(),
                        HomeOppFG = c.String(),
                        HomeOppFGP = c.String(),
                        HomeOppFT = c.String(),
                        HomeOppFTP = c.String(),
                        HomeOpp3s = c.String(),
                        HomeOpp3sP = c.String(),
                        HomeOppBLKS = c.String(),
                        HomeOppORBND = c.String(),
                        HomeOppRBND = c.String(),
                        HomeOppFouls = c.String(),
                        HomeOppAST = c.String(),
                        HomeOppTOvers = c.String(),
                        HomeOppQ1 = c.String(),
                        HomeOppQ2 = c.String(),
                        HomeOppQ3 = c.String(),
                        HomeOppQ4 = c.String(),
                        HomeOppFinal = c.String(),
                        HomeFGA = c.String(),
                        HomeFGM = c.String(),
                        HomeTPA = c.String(),
                        HomeTPM = c.String(),
                        HomeTO = c.String(),
                        HomeAST = c.String(),
                        HomeFGP = c.String(),
                        HomeTPP = c.String(),
                        HomeFTP = c.String(),
                        HomePFT = c.String(),
                        HomePTP = c.String(),
                        HomeWP = c.String(),
                        AwaySU = c.String(),
                        AwayATS = c.String(),
                        AwayOU = c.String(),
                        AwayAvgLine = c.String(),
                        AwayAvgTotal = c.String(),
                        AwayTeamFG = c.String(),
                        AwayTeamFGP = c.String(),
                        AwayTeamFT = c.String(),
                        AwayTeamFTP = c.String(),
                        AwayTeam3s = c.String(),
                        AwayTeam3sP = c.String(),
                        AwayTeamBLKS = c.String(),
                        AwayTeamORBND = c.String(),
                        AwayTeamRBND = c.String(),
                        AwayTeamFouls = c.String(),
                        AwayTeamAST = c.String(),
                        AwayTeamTOvers = c.String(),
                        AwayTeamQ1 = c.String(),
                        AwayTeamQ2 = c.String(),
                        AwayTeamQ3 = c.String(),
                        AwayTeamQ4 = c.String(),
                        AwayTeamFinal = c.String(),
                        AwayOppFG = c.String(),
                        AwayOppFGP = c.String(),
                        AwayOppFT = c.String(),
                        AwayOppFTP = c.String(),
                        AwayOpp3s = c.String(),
                        AwayOpp3sP = c.String(),
                        AwayOppBLKS = c.String(),
                        AwayOppORBND = c.String(),
                        AwayOppRBND = c.String(),
                        AwayOppFouls = c.String(),
                        AwayOppAST = c.String(),
                        AwayOppTOvers = c.String(),
                        AwayOppQ1 = c.String(),
                        AwayOppQ2 = c.String(),
                        AwayOppQ3 = c.String(),
                        AwayOppQ4 = c.String(),
                        AwayOppFinal = c.String(),
                        AwayFGA = c.String(),
                        AwayFGM = c.String(),
                        AwayTPA = c.String(),
                        AwayTPM = c.String(),
                        AwayTO = c.String(),
                        AwayAST = c.String(),
                        AwayFGP = c.String(),
                        AwayTPP = c.String(),
                        AwayFTP = c.String(),
                        AwayPFT = c.String(),
                        AwayPTP = c.String(),
                        AwayWP = c.String(),
                        Name_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeamsNBANames", t => t.Name_Id)
                .Index(t => t.Name_Id);
            
            CreateTable(
                "dbo.TeamsNBANames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NameErikberg = c.String(),
                        NameSportsdatabase = c.String(),
                        NameRotoworld = c.String(),
                        NameNBAcom = c.String(),
                        TeamLogoSmallPath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sport",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AnalysisProfit",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TotalBets = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        Week = c.Int(nullable: false),
                        FirstDayInWeek = c.DateTime(nullable: false),
                        Wins = c.Int(nullable: false),
                        Losses = c.Int(nullable: false),
                        Invested = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ROI = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Profit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Sport_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sport", t => t.Sport_Id)
                .Index(t => t.Sport_Id);
            
            CreateTable(
                "dbo.DraftsNBA",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DisplayName = c.String(),
                        Birthday = c.DateTime(),
                        Age = c.Int(nullable: false),
                        Birthplace = c.String(),
                        HeightIn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HeightCm = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HeightM = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HeightFormatted = c.String(),
                        WeightLb = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WeightKg = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Position = c.String(),
                        UniformNumber = c.Int(nullable: false),
                        Round = c.Int(nullable: false),
                        Pick = c.Int(nullable: false),
                        OrdinalPick = c.String(),
                        OverallPick = c.Int(nullable: false),
                        OrdinalOverallPick = c.String(),
                        GamesPlayed = c.Int(nullable: false),
                        Points = c.Int(nullable: false),
                        Assists = c.Int(nullable: false),
                        DefensiveRebounds = c.Int(nullable: false),
                        OffensiveRebounds = c.Int(nullable: false),
                        Steals = c.Int(nullable: false),
                        Blocks = c.Int(nullable: false),
                        Team_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeamsNBA", t => t.Team_Id)
                .Index(t => t.Team_Id);
            
            CreateTable(
                "dbo.Injuries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayerName = c.String(),
                        PlayerPosition = c.String(),
                        PlayerStatus = c.String(),
                        Date = c.String(),
                        Type = c.String(),
                        Returns = c.String(),
                        Report = c.String(),
                        ReportUpdateDate = c.String(),
                        Sport_Id = c.Int(),
                        TeamNBA_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sport", t => t.Sport_Id)
                .ForeignKey("dbo.TeamsNBA", t => t.TeamNBA_Id)
                .Index(t => t.Sport_Id)
                .Index(t => t.TeamNBA_Id);
            
            CreateTable(
                "dbo.LeadersNBA",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DisplayName = c.String(),
                        Rank = c.Int(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category_Id = c.Int(),
                        Team_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LeadersNBACategories", t => t.Category_Id)
                .ForeignKey("dbo.TeamsNBA", t => t.Team_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Team_Id);
            
            CreateTable(
                "dbo.LeadersNBACategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameErikberg = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Link = c.String(),
                        Description = c.String(),
                        PublishDate = c.String(),
                        Sport_Id = c.Int(),
                        NewsSources_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NewsSources", t => t.NewsSources_Id)
                .ForeignKey("dbo.Sport", t => t.Sport_Id)
                .Index(t => t.Sport_Id)
                .Index(t => t.NewsSources_Id);
            
            CreateTable(
                "dbo.NewsSources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PowerRankingsNBA",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rank = c.Int(),
                        RankLastWeek = c.Int(),
                        Score = c.String(),
                        Pace = c.String(),
                        OffRtg = c.String(),
                        DefRtg = c.String(),
                        NetRtg = c.String(),
                        Description = c.String(),
                        GamesThisWeek = c.String(),
                        Team_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeamsNBA", t => t.Team_Id)
                .Index(t => t.Team_Id);
            
            CreateTable(
                "dbo.RostersNBA",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DisplayName = c.String(),
                        Birthday = c.DateTime(),
                        Age = c.Int(nullable: false),
                        Birthplace = c.String(),
                        HeightIn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HeightCm = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HeightM = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HeightFormatted = c.String(),
                        WeightLb = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WeightKg = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Position = c.String(),
                        UniformNumber = c.Int(nullable: false),
                        Team_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeamsNBA", t => t.Team_Id)
                .Index(t => t.Team_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RostersNBA", "Team_Id", "dbo.TeamsNBA");
            DropForeignKey("dbo.PowerRankingsNBA", "Team_Id", "dbo.TeamsNBA");
            DropForeignKey("dbo.News", "Sport_Id", "dbo.Sport");
            DropForeignKey("dbo.News", "NewsSources_Id", "dbo.NewsSources");
            DropForeignKey("dbo.LeadersNBA", "Team_Id", "dbo.TeamsNBA");
            DropForeignKey("dbo.LeadersNBA", "Category_Id", "dbo.LeadersNBACategories");
            DropForeignKey("dbo.Injuries", "TeamNBA_Id", "dbo.TeamsNBA");
            DropForeignKey("dbo.Injuries", "Sport_Id", "dbo.Sport");
            DropForeignKey("dbo.DraftsNBA", "Team_Id", "dbo.TeamsNBA");
            DropForeignKey("dbo.AnalysisProfit", "Sport_Id", "dbo.Sport");
            DropForeignKey("dbo.Analysis", "Sport_Id", "dbo.Sport");
            DropForeignKey("dbo.Analysis", "EventNBA_Id", "dbo.ScheduleResultsNBA");
            DropForeignKey("dbo.ScheduleResultsNBA", "Team_Id", "dbo.TeamsNBA");
            DropForeignKey("dbo.ScheduleResultsNBA", "Opponent_Id", "dbo.TeamsNBA");
            DropForeignKey("dbo.TeamsNBA", "Name_Id", "dbo.TeamsNBANames");
            DropForeignKey("dbo.Analysis", "BetTypeTeam_Id", "dbo.BetType");
            DropForeignKey("dbo.Analysis", "BetStatus_Id", "dbo.BetStatus");
            DropForeignKey("dbo.Analysis", "AnalyseType_Id", "dbo.AnalyseType");
            DropForeignKey("dbo.Analysis", "AnalyseResultSolve_Id", "dbo.AnalyseResultSolve");
            DropIndex("dbo.RostersNBA", new[] { "Team_Id" });
            DropIndex("dbo.PowerRankingsNBA", new[] { "Team_Id" });
            DropIndex("dbo.News", new[] { "NewsSources_Id" });
            DropIndex("dbo.News", new[] { "Sport_Id" });
            DropIndex("dbo.LeadersNBA", new[] { "Team_Id" });
            DropIndex("dbo.LeadersNBA", new[] { "Category_Id" });
            DropIndex("dbo.Injuries", new[] { "TeamNBA_Id" });
            DropIndex("dbo.Injuries", new[] { "Sport_Id" });
            DropIndex("dbo.DraftsNBA", new[] { "Team_Id" });
            DropIndex("dbo.AnalysisProfit", new[] { "Sport_Id" });
            DropIndex("dbo.TeamsNBA", new[] { "Name_Id" });
            DropIndex("dbo.ScheduleResultsNBA", new[] { "Opponent_Id" });
            DropIndex("dbo.ScheduleResultsNBA", new[] { "Team_Id" });
            DropIndex("dbo.Analysis", new[] { "EventNBA_Id" });
            DropIndex("dbo.Analysis", new[] { "AnalyseResultSolve_Id" });
            DropIndex("dbo.Analysis", new[] { "AnalyseType_Id" });
            DropIndex("dbo.Analysis", new[] { "BetStatus_Id" });
            DropIndex("dbo.Analysis", new[] { "BetTypeTeam_Id" });
            DropIndex("dbo.Analysis", new[] { "Sport_Id" });
            DropTable("dbo.RostersNBA");
            DropTable("dbo.PowerRankingsNBA");
            DropTable("dbo.NewsSources");
            DropTable("dbo.News");
            DropTable("dbo.LeadersNBACategories");
            DropTable("dbo.LeadersNBA");
            DropTable("dbo.Injuries");
            DropTable("dbo.DraftsNBA");
            DropTable("dbo.AnalysisProfit");
            DropTable("dbo.Sport");
            DropTable("dbo.TeamsNBANames");
            DropTable("dbo.TeamsNBA");
            DropTable("dbo.ScheduleResultsNBA");
            DropTable("dbo.BetType");
            DropTable("dbo.BetStatus");
            DropTable("dbo.Analysis");
            DropTable("dbo.AnalyseType");
            DropTable("dbo.AnalyseResultSolve");
        }
    }
}

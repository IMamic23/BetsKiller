namespace BetsKiller.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BetsTrackerTablesAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BetProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Bets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Invested = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Odd = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Profit = c.Decimal(precision: 18, scale: 2),
                        BetStatus_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BetStatus", t => t.BetStatus_Id)
                .Index(t => t.BetStatus_Id);
            
            CreateTable(
                "dbo.BetProfiles_Bets",
                c => new
                    {
                        BetProfile_Id = c.Int(nullable: false),
                        Bet_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BetProfile_Id, t.Bet_Id })
                .ForeignKey("dbo.BetProfiles", t => t.BetProfile_Id, cascadeDelete: true)
                .ForeignKey("dbo.Bets", t => t.Bet_Id, cascadeDelete: true)
                .Index(t => t.BetProfile_Id)
                .Index(t => t.Bet_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BetProfiles_Bets", "Bet_Id", "dbo.Bets");
            DropForeignKey("dbo.BetProfiles_Bets", "BetProfile_Id", "dbo.BetProfiles");
            DropForeignKey("dbo.Bets", "BetStatus_Id", "dbo.BetStatus");
            DropIndex("dbo.BetProfiles_Bets", new[] { "Bet_Id" });
            DropIndex("dbo.BetProfiles_Bets", new[] { "BetProfile_Id" });
            DropIndex("dbo.Bets", new[] { "BetStatus_Id" });
            DropTable("dbo.BetProfiles_Bets");
            DropTable("dbo.Bets");
            DropTable("dbo.BetProfiles");
        }
    }
}

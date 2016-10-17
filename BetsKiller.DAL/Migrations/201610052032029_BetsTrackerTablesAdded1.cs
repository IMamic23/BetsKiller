namespace BetsKiller.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BetsTrackerTablesAdded1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BetGames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false),
                        Changed = c.DateTime(),
                        Bet = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BetLogicPush = c.String(),
                        BetLogicWinLoss = c.String(),
                        Odd = c.Decimal(nullable: false, precision: 18, scale: 4),
                        Result = c.String(),
                        Bet_Id = c.Int(),
                        Sport_Id = c.Int(),
                        BetType_Id = c.Int(),
                        BetStatus_Id = c.Int(),
                        EventNBA_Id = c.Int(),
                        EventMLB_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BetStatus", t => t.BetStatus_Id)
                .ForeignKey("dbo.Bets", t => t.Bet_Id)
                .ForeignKey("dbo.BetType", t => t.BetType_Id)
                .ForeignKey("dbo.ScheduleResultsNBA", t => t.EventNBA_Id)
                .ForeignKey("dbo.Sport", t => t.Sport_Id)
                .Index(t => t.Bet_Id)
                .Index(t => t.Sport_Id)
                .Index(t => t.BetType_Id)
                .Index(t => t.BetStatus_Id)
                .Index(t => t.EventNBA_Id);
            
            AlterColumn("dbo.Bets", "Invested", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("dbo.Bets", "Odd", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("dbo.Bets", "Profit", c => c.Decimal(precision: 18, scale: 4));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BetGames", "Sport_Id", "dbo.Sport");
            DropForeignKey("dbo.BetGames", "EventNBA_Id", "dbo.ScheduleResultsNBA");
            DropForeignKey("dbo.BetGames", "BetType_Id", "dbo.BetType");
            DropForeignKey("dbo.BetGames", "Bet_Id", "dbo.Bets");
            DropForeignKey("dbo.BetGames", "BetStatus_Id", "dbo.BetStatus");
            DropIndex("dbo.BetGames", new[] { "EventNBA_Id" });
            DropIndex("dbo.BetGames", new[] { "BetStatus_Id" });
            DropIndex("dbo.BetGames", new[] { "BetType_Id" });
            DropIndex("dbo.BetGames", new[] { "Sport_Id" });
            DropIndex("dbo.BetGames", new[] { "Bet_Id" });
            AlterColumn("dbo.Bets", "Profit", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Bets", "Odd", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Bets", "Invested", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropTable("dbo.BetGames");
        }
    }
}

namespace BetsKiller.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BetsTrackerViewUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bets", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.BetProfiles", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.BetProfiles", "Username", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BetProfiles", "Username");
            DropColumn("dbo.BetProfiles", "Created");
            DropColumn("dbo.Bets", "Created");
        }
    }
}

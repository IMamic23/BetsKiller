namespace BetsKiller.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnalysisTableChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Analysis", "OfferTotal", c => c.Decimal(precision: 18, scale: 4));
            AlterColumn("dbo.Analysis", "OfferLine", c => c.Decimal(precision: 18, scale: 4));
            AlterColumn("dbo.Analysis", "Odd", c => c.Decimal(precision: 18, scale: 4));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Analysis", "OfferLine", c => c.String());
            AlterColumn("dbo.Analysis", "OfferTotal", c => c.String());
            AlterColumn("dbo.Analysis", "Odd", c => c.Decimal());
        }
    }
}

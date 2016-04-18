namespace BetsKiller.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DecimalNumbers : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Analysis", "OfferTotal", c => c.Decimal(precision: 18, scale: 4));
            AlterColumn("dbo.Analysis", "OfferLine", c => c.Decimal(precision: 18, scale: 4));
            AlterColumn("dbo.Analysis", "Invested", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("dbo.Analysis", "Odd", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("dbo.Analysis", "Profit", c => c.Decimal(precision: 18, scale: 4));
            AlterColumn("dbo.Analysis", "Confidence", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("dbo.AnalysisProfit", "Invested", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("dbo.AnalysisProfit", "ROI", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("dbo.AnalysisProfit", "Profit", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("dbo.DraftsNBA", "HeightIn", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("dbo.DraftsNBA", "HeightCm", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("dbo.DraftsNBA", "HeightM", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("dbo.DraftsNBA", "WeightLb", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("dbo.DraftsNBA", "WeightKg", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("dbo.LeadersNBA", "Value", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("dbo.RostersNBA", "HeightIn", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("dbo.RostersNBA", "HeightCm", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("dbo.RostersNBA", "HeightM", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("dbo.RostersNBA", "WeightLb", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("dbo.RostersNBA", "WeightKg", c => c.Decimal(nullable: false, precision: 18, scale: 4));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RostersNBA", "WeightKg", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.RostersNBA", "WeightLb", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.RostersNBA", "HeightM", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.RostersNBA", "HeightCm", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.RostersNBA", "HeightIn", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.LeadersNBA", "Value", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.DraftsNBA", "WeightKg", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.DraftsNBA", "WeightLb", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.DraftsNBA", "HeightM", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.DraftsNBA", "HeightCm", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.DraftsNBA", "HeightIn", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AnalysisProfit", "Profit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AnalysisProfit", "ROI", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AnalysisProfit", "Invested", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Analysis", "Confidence", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Analysis", "Profit", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Analysis", "Odd", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Analysis", "Invested", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Analysis", "OfferLine", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Analysis", "OfferTotal", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}

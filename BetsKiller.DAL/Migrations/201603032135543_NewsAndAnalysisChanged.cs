namespace BetsKiller.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewsAndAnalysisChanged : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.News", "NewsSources_Id", "dbo.NewsSources");
            DropIndex("dbo.News", new[] { "NewsSources_Id" });
            AddColumn("dbo.News", "Created", c => c.DateTime());
            AddColumn("dbo.News", "IsPublished", c => c.Boolean(nullable: false));
            AddColumn("dbo.News", "Published", c => c.DateTime());
            AlterColumn("dbo.Analysis", "Bet", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.News", "PublishDate");
            DropColumn("dbo.News", "NewsSources_Id");
            DropTable("dbo.NewsSources");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.NewsSources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.News", "NewsSources_Id", c => c.Int());
            AddColumn("dbo.News", "PublishDate", c => c.String());
            AlterColumn("dbo.Analysis", "Bet", c => c.String());
            DropColumn("dbo.News", "Published");
            DropColumn("dbo.News", "IsPublished");
            DropColumn("dbo.News", "Created");
            CreateIndex("dbo.News", "NewsSources_Id");
            AddForeignKey("dbo.News", "NewsSources_Id", "dbo.NewsSources", "Id");
        }
    }
}

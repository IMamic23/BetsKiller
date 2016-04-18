namespace BetsKiller.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewsPublishing : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.News", newName: "NewsFeed");
            CreateTable(
                "dbo.NewsPublish",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Link = c.String(),
                        Description = c.String(),
                        Published = c.DateTime(nullable: false),
                        Sport_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Sport_Id);
            
            AlterColumn("dbo.NewsFeed", "Created", c => c.DateTime(nullable: false));
            DropColumn("dbo.NewsFeed", "IsPublished");
            DropColumn("dbo.NewsFeed", "Published");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NewsFeed", "Published", c => c.DateTime());
            AddColumn("dbo.NewsFeed", "IsPublished", c => c.Boolean(nullable: false));
            DropIndex("dbo.NewsPublish", new[] { "Sport_Id" });
            AlterColumn("dbo.NewsFeed", "Created", c => c.DateTime());
            DropTable("dbo.NewsPublish");
            RenameTable(name: "dbo.NewsFeed", newName: "News");
        }
    }
}

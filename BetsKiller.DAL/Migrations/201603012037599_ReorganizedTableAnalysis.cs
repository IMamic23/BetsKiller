namespace BetsKiller.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReorganizedTableAnalysis : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Analysis", "AnalyseResultSolve_Id", "dbo.AnalyseResultSolve");
            DropIndex("dbo.Analysis", new[] { "AnalyseResultSolve_Id" });
            RenameColumn(table: "dbo.Analysis", name: "BetTypeTeam_Id", newName: "BetType_Id");
            RenameIndex(table: "dbo.Analysis", name: "IX_BetTypeTeam_Id", newName: "IX_BetType_Id");
            DropColumn("dbo.Analysis", "AnalyseResultSolve_Id");
            DropTable("dbo.AnalyseResultSolve");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AnalyseResultSolve",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Analysis", "AnalyseResultSolve_Id", c => c.Int());
            RenameIndex(table: "dbo.Analysis", name: "IX_BetType_Id", newName: "IX_BetTypeTeam_Id");
            RenameColumn(table: "dbo.Analysis", name: "BetType_Id", newName: "BetTypeTeam_Id");
            CreateIndex("dbo.Analysis", "AnalyseResultSolve_Id");
            AddForeignKey("dbo.Analysis", "AnalyseResultSolve_Id", "dbo.AnalyseResultSolve", "Id");
        }
    }
}

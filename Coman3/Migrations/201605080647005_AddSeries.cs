namespace Coman3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSeries : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Series",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        ShortDescription = c.String(),
                        LongDescription = c.String(),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Seasons",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Serie_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Series", t => t.Serie_Id)
                .Index(t => t.Serie_Id);
            
            CreateTable(
                "dbo.Episodes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Season_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Seasons", t => t.Season_Id)
                .Index(t => t.Season_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Seasons", "Serie_Id", "dbo.Series");
            DropForeignKey("dbo.Episodes", "Season_Id", "dbo.Seasons");
            DropIndex("dbo.Episodes", new[] { "Season_Id" });
            DropIndex("dbo.Seasons", new[] { "Serie_Id" });
            DropTable("dbo.Episodes");
            DropTable("dbo.Seasons");
            DropTable("dbo.Series");
        }
    }
}

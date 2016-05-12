namespace Coman3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTagsGenres : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Serie_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Series", t => t.Serie_Id)
                .Index(t => t.Serie_Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Serie_Id = c.Guid(),
                        Season_Id = c.Guid(),
                        Episode_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Series", t => t.Serie_Id)
                .ForeignKey("dbo.Seasons", t => t.Season_Id)
                .ForeignKey("dbo.Episodes", t => t.Episode_Id)
                .Index(t => t.Serie_Id)
                .Index(t => t.Season_Id)
                .Index(t => t.Episode_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "Episode_Id", "dbo.Episodes");
            DropForeignKey("dbo.Tags", "Season_Id", "dbo.Seasons");
            DropForeignKey("dbo.Tags", "Serie_Id", "dbo.Series");
            DropForeignKey("dbo.Genres", "Serie_Id", "dbo.Series");
            DropIndex("dbo.Tags", new[] { "Episode_Id" });
            DropIndex("dbo.Tags", new[] { "Season_Id" });
            DropIndex("dbo.Tags", new[] { "Serie_Id" });
            DropIndex("dbo.Genres", new[] { "Serie_Id" });
            DropTable("dbo.Tags");
            DropTable("dbo.Genres");
        }
    }
}

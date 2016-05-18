namespace Coman3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFavs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Favourites",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Episode_Id = c.Guid(),
                        Season_Id = c.Guid(),
                        Serie_Id = c.Guid(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Episodes", t => t.Episode_Id)
                .ForeignKey("dbo.Seasons", t => t.Season_Id)
                .ForeignKey("dbo.Series", t => t.Serie_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Episode_Id)
                .Index(t => t.Season_Id)
                .Index(t => t.Serie_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Favourites", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Favourites", "Serie_Id", "dbo.Series");
            DropForeignKey("dbo.Favourites", "Season_Id", "dbo.Seasons");
            DropForeignKey("dbo.Favourites", "Episode_Id", "dbo.Episodes");
            DropIndex("dbo.Favourites", new[] { "User_Id" });
            DropIndex("dbo.Favourites", new[] { "Serie_Id" });
            DropIndex("dbo.Favourites", new[] { "Season_Id" });
            DropIndex("dbo.Favourites", new[] { "Episode_Id" });
            DropTable("dbo.Favourites");
        }
    }
}

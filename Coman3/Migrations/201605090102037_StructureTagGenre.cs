namespace Coman3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StructureTagGenre : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Genres", "Serie_Id", "dbo.Series");
            DropForeignKey("dbo.Tags", "Serie_Id", "dbo.Series");
            DropForeignKey("dbo.Tags", "Season_Id", "dbo.Seasons");
            DropForeignKey("dbo.Tags", "Episode_Id", "dbo.Episodes");
            DropIndex("dbo.Genres", new[] { "Serie_Id" });
            DropIndex("dbo.Tags", new[] { "Serie_Id" });
            DropIndex("dbo.Tags", new[] { "Season_Id" });
            DropIndex("dbo.Tags", new[] { "Episode_Id" });
            AddColumn("dbo.Episodes", "Tag_Id", c => c.Guid());
            AddColumn("dbo.Seasons", "Tag_Id", c => c.Guid());
            AddColumn("dbo.Series", "Genre_Id", c => c.Guid());
            AddColumn("dbo.Series", "Tag_Id", c => c.Guid());
            CreateIndex("dbo.Episodes", "Tag_Id");
            CreateIndex("dbo.Seasons", "Tag_Id");
            CreateIndex("dbo.Series", "Genre_Id");
            CreateIndex("dbo.Series", "Tag_Id");
            AddForeignKey("dbo.Series", "Genre_Id", "dbo.Genres", "Id");
            AddForeignKey("dbo.Episodes", "Tag_Id", "dbo.Tags", "Id");
            AddForeignKey("dbo.Seasons", "Tag_Id", "dbo.Tags", "Id");
            AddForeignKey("dbo.Series", "Tag_Id", "dbo.Tags", "Id");
            DropColumn("dbo.Genres", "Serie_Id");
            DropColumn("dbo.Tags", "Serie_Id");
            DropColumn("dbo.Tags", "Season_Id");
            DropColumn("dbo.Tags", "Episode_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tags", "Episode_Id", c => c.Guid());
            AddColumn("dbo.Tags", "Season_Id", c => c.Guid());
            AddColumn("dbo.Tags", "Serie_Id", c => c.Guid());
            AddColumn("dbo.Genres", "Serie_Id", c => c.Guid());
            DropForeignKey("dbo.Series", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.Seasons", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.Episodes", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.Series", "Genre_Id", "dbo.Genres");
            DropIndex("dbo.Series", new[] { "Tag_Id" });
            DropIndex("dbo.Series", new[] { "Genre_Id" });
            DropIndex("dbo.Seasons", new[] { "Tag_Id" });
            DropIndex("dbo.Episodes", new[] { "Tag_Id" });
            DropColumn("dbo.Series", "Tag_Id");
            DropColumn("dbo.Series", "Genre_Id");
            DropColumn("dbo.Seasons", "Tag_Id");
            DropColumn("dbo.Episodes", "Tag_Id");
            CreateIndex("dbo.Tags", "Episode_Id");
            CreateIndex("dbo.Tags", "Season_Id");
            CreateIndex("dbo.Tags", "Serie_Id");
            CreateIndex("dbo.Genres", "Serie_Id");
            AddForeignKey("dbo.Tags", "Episode_Id", "dbo.Episodes", "Id");
            AddForeignKey("dbo.Tags", "Season_Id", "dbo.Seasons", "Id");
            AddForeignKey("dbo.Tags", "Serie_Id", "dbo.Series", "Id");
            AddForeignKey("dbo.Genres", "Serie_Id", "dbo.Series", "Id");
        }
    }
}

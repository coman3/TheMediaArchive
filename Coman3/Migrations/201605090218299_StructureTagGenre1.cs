namespace Coman3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StructureTagGenre1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Series", "Genre_Id", "dbo.Genres");
            DropForeignKey("dbo.Episodes", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.Seasons", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.Series", "Tag_Id", "dbo.Tags");
            DropIndex("dbo.Episodes", new[] { "Tag_Id" });
            DropIndex("dbo.Seasons", new[] { "Tag_Id" });
            DropIndex("dbo.Series", new[] { "Genre_Id" });
            DropIndex("dbo.Series", new[] { "Tag_Id" });
            AddColumn("dbo.Episodes", "Tags", c => c.String());
            AddColumn("dbo.Episodes", "Name", c => c.String());
            AddColumn("dbo.Seasons", "Tags", c => c.String());
            AddColumn("dbo.Series", "Genres", c => c.String());
            AddColumn("dbo.Series", "Tags", c => c.String());
            AddColumn("dbo.Series", "Name", c => c.String());
            DropColumn("dbo.Episodes", "Title");
            DropColumn("dbo.Episodes", "Tag_Id");
            DropColumn("dbo.Seasons", "Tag_Id");
            DropColumn("dbo.Series", "Title");
            DropColumn("dbo.Series", "Genre_Id");
            DropColumn("dbo.Series", "Tag_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Series", "Tag_Id", c => c.Guid());
            AddColumn("dbo.Series", "Genre_Id", c => c.Guid());
            AddColumn("dbo.Series", "Title", c => c.String());
            AddColumn("dbo.Seasons", "Tag_Id", c => c.Guid());
            AddColumn("dbo.Episodes", "Tag_Id", c => c.Guid());
            AddColumn("dbo.Episodes", "Title", c => c.String());
            DropColumn("dbo.Series", "Name");
            DropColumn("dbo.Series", "Tags");
            DropColumn("dbo.Series", "Genres");
            DropColumn("dbo.Seasons", "Tags");
            DropColumn("dbo.Episodes", "Name");
            DropColumn("dbo.Episodes", "Tags");
            CreateIndex("dbo.Series", "Tag_Id");
            CreateIndex("dbo.Series", "Genre_Id");
            CreateIndex("dbo.Seasons", "Tag_Id");
            CreateIndex("dbo.Episodes", "Tag_Id");
            AddForeignKey("dbo.Series", "Tag_Id", "dbo.Tags", "Id");
            AddForeignKey("dbo.Seasons", "Tag_Id", "dbo.Tags", "Id");
            AddForeignKey("dbo.Episodes", "Tag_Id", "dbo.Tags", "Id");
            AddForeignKey("dbo.Series", "Genre_Id", "dbo.Genres", "Id");
        }
    }
}

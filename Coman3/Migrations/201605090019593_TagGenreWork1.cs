namespace Coman3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TagGenreWork1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Series", "GenresString");
            DropColumn("dbo.Series", "TagsString");
            DropColumn("dbo.Series", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Series", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Series", "TagsString", c => c.String());
            AddColumn("dbo.Series", "GenresString", c => c.String());
        }
    }
}

namespace Coman3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TagGenreWork : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Series", "GenresString", c => c.String());
            AddColumn("dbo.Series", "TagsString", c => c.String());
            AddColumn("dbo.Series", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Series", "Discriminator");
            DropColumn("dbo.Series", "TagsString");
            DropColumn("dbo.Series", "GenresString");
        }
    }
}

namespace Coman3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNameToSeason : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Seasons", "Name", c => c.String());
            AddColumn("dbo.Seasons", "Number", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Seasons", "Number");
            DropColumn("dbo.Seasons", "Name");
        }
    }
}

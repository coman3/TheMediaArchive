namespace Coman3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSeasonEpisodeCountToSerie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Series", "SeasonCount", c => c.Int(nullable: false));
            AddColumn("dbo.Series", "EpisodeCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Series", "EpisodeCount");
            DropColumn("dbo.Series", "SeasonCount");
        }
    }
}

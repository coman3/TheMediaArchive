namespace Coman3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVideoLink : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VideoLinks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Link = c.String(),
                        SuccessRate = c.Int(nullable: false),
                        Name = c.String(),
                        Episode_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Episodes", t => t.Episode_Id)
                .Index(t => t.Episode_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VideoLinks", "Episode_Id", "dbo.Episodes");
            DropIndex("dbo.VideoLinks", new[] { "Episode_Id" });
            DropTable("dbo.VideoLinks");
        }
    }
}

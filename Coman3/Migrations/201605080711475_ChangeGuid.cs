namespace Coman3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeGuid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Seasons", "Serie_Id", "dbo.Series");
            DropForeignKey("dbo.Episodes", "Season_Id", "dbo.Seasons");
            DropPrimaryKey("dbo.Series");
            DropPrimaryKey("dbo.Seasons");
            DropPrimaryKey("dbo.Episodes");
            AlterColumn("dbo.Series", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Seasons", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Episodes", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Series", "Id");
            AddPrimaryKey("dbo.Seasons", "Id");
            AddPrimaryKey("dbo.Episodes", "Id");
            AddForeignKey("dbo.Seasons", "Serie_Id", "dbo.Series", "Id");
            AddForeignKey("dbo.Episodes", "Season_Id", "dbo.Seasons", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Episodes", "Season_Id", "dbo.Seasons");
            DropForeignKey("dbo.Seasons", "Serie_Id", "dbo.Series");
            DropPrimaryKey("dbo.Episodes");
            DropPrimaryKey("dbo.Seasons");
            DropPrimaryKey("dbo.Series");
            AlterColumn("dbo.Episodes", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Seasons", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Series", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.Episodes", "Id");
            AddPrimaryKey("dbo.Seasons", "Id");
            AddPrimaryKey("dbo.Series", "Id");
            AddForeignKey("dbo.Episodes", "Season_Id", "dbo.Seasons", "Id");
            AddForeignKey("dbo.Seasons", "Serie_Id", "dbo.Series", "Id");
        }
    }
}

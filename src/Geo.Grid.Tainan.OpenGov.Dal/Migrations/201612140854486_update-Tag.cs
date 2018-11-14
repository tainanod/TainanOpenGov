namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAG", "FORUM_ID", c => c.Guid(nullable: false, defaultValue: new Guid("9258DBF9-80EC-E411-82D7-BCEE7BD92C28")));
            AddColumn("dbo.TAG", "SORT", c => c.Int(nullable: false));
            CreateIndex("dbo.TAG", "FORUM_ID");
            AddForeignKey("dbo.TAG", "FORUM_ID", "dbo.FORUM", "FORUM_ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TAG", "FORUM_ID", "dbo.FORUM");
            DropIndex("dbo.TAG", new[] { "FORUM_ID" });
            DropColumn("dbo.TAG", "SORT");
            DropColumn("dbo.TAG", "FORUM_ID");
        }
    }
}

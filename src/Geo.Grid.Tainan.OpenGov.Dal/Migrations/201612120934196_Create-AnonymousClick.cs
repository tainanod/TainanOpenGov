namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAnonymousClick : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnonymousClick",
                c => new
                    {
                        AnonymousId = c.String(nullable: false, maxLength: 128),
                        ClickId = c.String(nullable: false, maxLength: 128),
                        ClickType = c.String(nullable: false, maxLength: 128),
                        ForumId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.AnonymousId, t.ClickId, t.ClickType })
                .ForeignKey("dbo.FORUM", t => t.ForumId)
                .Index(t => t.ForumId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AnonymousClick", "ForumId", "dbo.FORUM");
            DropIndex("dbo.AnonymousClick", new[] { "ForumId" });
            DropTable("dbo.AnonymousClick");
        }
    }
}

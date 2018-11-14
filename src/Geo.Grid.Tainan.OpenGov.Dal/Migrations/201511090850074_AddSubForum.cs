namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSubForum : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TAG",
                c => new
                    {
                        TAG_ID = c.Guid(nullable: false),
                        TAG_NAME = c.String(nullable: false, maxLength: 50),
                        IS_ENABLED = c.Boolean(nullable: false),
                        CREATED_BY = c.String(nullable: false, maxLength: 100),
                        CREATED_DATE = c.DateTime(nullable: false),
                        UPDATE_BY = c.String(nullable: false, maxLength: 100),
                        UPDATE_DATE = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TAG_ID);
            
            CreateTable(
                "dbo.REL_USER_PUSH",
                c => new
                    {
                        DiscussId = c.Guid(nullable: false),
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.DiscussId, t.Id })
                .ForeignKey("dbo.DISCUSS", t => t.DiscussId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Id, cascadeDelete: true)
                .Index(t => t.DiscussId)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.REL_DISCUSS_TAG",
                c => new
                    {
                        DiscussId = c.Guid(nullable: false),
                        TagId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.DiscussId, t.TagId })
                .ForeignKey("dbo.DISCUSS", t => t.DiscussId, cascadeDelete: true)
                .ForeignKey("dbo.TAG", t => t.TagId, cascadeDelete: true)
                .Index(t => t.DiscussId)
                .Index(t => t.TagId);
            
            AddColumn("dbo.FORUM", "UPPER_ID", c => c.Guid());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.REL_DISCUSS_TAG", "TagId", "dbo.TAG");
            DropForeignKey("dbo.REL_DISCUSS_TAG", "DiscussId", "dbo.DISCUSS");
            DropForeignKey("dbo.REL_USER_PUSH", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.REL_USER_PUSH", "DiscussId", "dbo.DISCUSS");
            DropIndex("dbo.REL_DISCUSS_TAG", new[] { "TagId" });
            DropIndex("dbo.REL_DISCUSS_TAG", new[] { "DiscussId" });
            DropIndex("dbo.REL_USER_PUSH", new[] { "Id" });
            DropIndex("dbo.REL_USER_PUSH", new[] { "DiscussId" });
            DropColumn("dbo.FORUM", "UPPER_ID");
            DropTable("dbo.REL_DISCUSS_TAG");
            DropTable("dbo.REL_USER_PUSH");
            DropTable("dbo.TAG");
        }
    }
}

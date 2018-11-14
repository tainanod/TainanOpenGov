namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class RelActivityDocumentAndVideoUrlCanNullAndAspNetUserKey : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.REL_ACTIVITY_DOCUMENT",
                c => new
                    {
                        ACTIVITY_ID = c.Guid(nullable: false),
                        DOCUMENT_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ACTIVITY_ID, t.DOCUMENT_ID })
                .ForeignKey("dbo.ACTIVITY", t => t.ACTIVITY_ID, cascadeDelete: true)
                .ForeignKey("dbo.DOCUMENT", t => t.DOCUMENT_ID, cascadeDelete: true)
                .Index(t => t.ACTIVITY_ID)
                .Index(t => t.DOCUMENT_ID);

            AlterColumn("dbo.YOUTUBE", "URL", c => c.String(maxLength: 4000));
            AlterColumn("dbo.YOUTUBE", "VIDEO_TIME", c => c.String(maxLength: 50));
        }

        public override void Down()
        {
            DropForeignKey("dbo.REL_ACTIVITY_DOCUMENT", "DOCUMENT_ID", "dbo.DOCUMENT");
            DropForeignKey("dbo.REL_ACTIVITY_DOCUMENT", "ACTIVITY_ID", "dbo.ACTIVITY");
            DropIndex("dbo.REL_ACTIVITY_DOCUMENT", new[] { "DOCUMENT_ID" });
            DropIndex("dbo.REL_ACTIVITY_DOCUMENT", new[] { "ACTIVITY_ID" });
            AlterColumn("dbo.YOUTUBE", "VIDEO_TIME", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.YOUTUBE", "URL", c => c.String(nullable: false, maxLength: 4000));
            DropTable("dbo.REL_ACTIVITY_DOCUMENT");
        }
    }
}
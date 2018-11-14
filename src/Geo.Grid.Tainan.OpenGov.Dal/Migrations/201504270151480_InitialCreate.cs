namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ACTIVITY",
                c => new
                    {
                        ACTIVITY_ID = c.Guid(nullable: false, identity: true),
                        FORUM_ID = c.Guid(nullable: false),
                        SUBJECT = c.String(nullable: false, maxLength: 300),
                        DESCRIPTION = c.String(nullable: false, maxLength: 2000),
                        HOLD_DATE = c.DateTime(nullable: false),
                        LOCATION = c.String(nullable: false, maxLength: 200),
                        IS_ENABLED = c.Boolean(nullable: false),
                        CREATED_BY = c.String(nullable: false, maxLength: 100),
                        CREATED_DATE = c.DateTime(nullable: false),
                        UPDATE_BY = c.String(nullable: false, maxLength: 100),
                        UPDATE_DATE = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ACTIVITY_ID)
                .ForeignKey("dbo.FORUM", t => t.FORUM_ID)
                .Index(t => t.FORUM_ID);

            CreateTable(
                "dbo.FORUM",
                c => new
                    {
                        FORUM_ID = c.Guid(nullable: false, identity: true),
                        CATEGORY = c.String(nullable: false, maxLength: 100),
                        SUBJECT = c.String(nullable: false, maxLength: 300),
                        HOLDER = c.String(nullable: false, maxLength: 100),
                        DESCRIPTION = c.String(nullable: false, maxLength: 4000),
                        OPEN_DATE = c.DateTime(nullable: false),
                        CLOSE_DATE = c.DateTime(nullable: false),
                        IS_ENABLED = c.Boolean(nullable: false),
                        CREATED_BY = c.String(nullable: false, maxLength: 100),
                        CREATED_DATE = c.DateTime(nullable: false),
                        UPDATE_BY = c.String(nullable: false, maxLength: 100),
                        UPDATE_DATE = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.FORUM_ID);

            CreateTable(
                "dbo.DISCUSS",
                c => new
                    {
                        DISCUSS_ID = c.Guid(nullable: false, identity: true),
                        FORUM_ID = c.Guid(nullable: false),
                        USER_ID = c.String(nullable: false, maxLength: 128),
                        UPPER_ID = c.Guid(),
                        MESSAGE = c.String(nullable: false, maxLength: 4000),
                        IS_ENABLED = c.Boolean(nullable: false),
                        CREATED_BY = c.String(nullable: false, maxLength: 100),
                        CREATED_DATE = c.DateTime(nullable: false),
                        UPDATE_BY = c.String(nullable: false, maxLength: 100),
                        UPDATE_DATE = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DISCUSS_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.USER_ID)
                .ForeignKey("dbo.FORUM", t => t.FORUM_ID)
                .Index(t => t.FORUM_ID)
                .Index(t => t.USER_ID);

            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        NickName = c.String(),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.DOCUMENT",
                c => new
                    {
                        DOCUMENT_ID = c.Guid(nullable: false, identity: true),
                        SIZE = c.Long(nullable: false),
                        FILE_NAME = c.String(nullable: false, maxLength: 255),
                        FILE_TYPE = c.String(nullable: false, maxLength: 255),
                        ALT = c.String(maxLength: 255),
                        IS_ENABLED = c.Boolean(nullable: false),
                        CREATED_BY = c.String(nullable: false, maxLength: 100),
                        CREATED_DATE = c.DateTime(nullable: false),
                        UPDATE_BY = c.String(nullable: false, maxLength: 100),
                        UPDATE_DATE = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DOCUMENT_ID);

            CreateTable(
                "dbo.DOCUMENT_EXT",
                c => new
                    {
                        DOCUMENT_ID = c.Guid(nullable: false),
                        ORIGINAL = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.DOCUMENT_ID)
                .ForeignKey("dbo.DOCUMENT", t => t.DOCUMENT_ID, cascadeDelete: true)
                .Index(t => t.DOCUMENT_ID);

            CreateTable(
                "dbo.HYPERLINK",
                c => new
                    {
                        HYPERLINK_ID = c.Guid(nullable: false, identity: true),
                        TITLE = c.String(nullable: false, maxLength: 200),
                        DESCRIPTION = c.String(maxLength: 500),
                        URL = c.String(nullable: false, maxLength: 200),
                        IS_ENABLED = c.Boolean(nullable: false),
                        CREATED_BY = c.String(nullable: false, maxLength: 100),
                        CREATED_DATE = c.DateTime(nullable: false),
                        UPDATE_BY = c.String(nullable: false, maxLength: 100),
                        UPDATE_DATE = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.HYPERLINK_ID);

            CreateTable(
                "dbo.PHOTO",
                c => new
                    {
                        PHOTO_ID = c.Guid(nullable: false, identity: true),
                        SIZE = c.Long(nullable: false),
                        FILE_NAME = c.String(nullable: false, maxLength: 255),
                        FILE_TYPE = c.String(nullable: false, maxLength: 255),
                        ALT = c.String(maxLength: 255),
                        IS_ENABLED = c.Boolean(nullable: false),
                        CREATED_BY = c.String(nullable: false, maxLength: 100),
                        CREATED_DATE = c.DateTime(nullable: false),
                        UPDATE_BY = c.String(nullable: false, maxLength: 100),
                        UPDATE_DATE = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PHOTO_ID);

            CreateTable(
                "dbo.PHOTO_EXT",
                c => new
                    {
                        PHOTO_ID = c.Guid(nullable: false),
                        ORIGINAL = c.Binary(nullable: false, storeType: "image"),
                        WIDTH1024 = c.Binary(nullable: false, storeType: "image"),
                        WIDTH768 = c.Binary(nullable: false, storeType: "image"),
                        WIDTH320 = c.Binary(nullable: false, storeType: "image"),
                        WIDTH120 = c.Binary(nullable: false, storeType: "image"),
                    })
                .PrimaryKey(t => t.PHOTO_ID)
                .ForeignKey("dbo.PHOTO", t => t.PHOTO_ID, cascadeDelete: true)
                .Index(t => t.PHOTO_ID);

            CreateTable(
                "dbo.YOUTUBE",
                c => new
                    {
                        YOUTUBE_ID = c.Guid(nullable: false, identity: true),
                        NAME = c.String(nullable: false, maxLength: 50),
                        URL = c.String(nullable: false, maxLength: 4000),
                        START_DATE = c.DateTime(nullable: false),
                        VIDEO_TIME = c.String(nullable: false, maxLength: 50),
                        IS_OPENED = c.Boolean(nullable: false),
                        DESCRIPTION = c.String(maxLength: 4000),
                        END_DATE = c.DateTime(nullable: false),
                        IS_ENABLED = c.Boolean(nullable: false),
                        CREATED_BY = c.String(nullable: false, maxLength: 100),
                        CREATED_DATE = c.DateTime(nullable: false),
                        UPDATE_BY = c.String(nullable: false, maxLength: 100),
                        UPDATE_DATE = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.YOUTUBE_ID);

            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.REL_FORUM_DOCUMENT",
                c => new
                    {
                        DOCUMENT_ID = c.Guid(nullable: false),
                        FORUM_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.DOCUMENT_ID, t.FORUM_ID })
                .ForeignKey("dbo.DOCUMENT", t => t.DOCUMENT_ID, cascadeDelete: true)
                .ForeignKey("dbo.FORUM", t => t.FORUM_ID, cascadeDelete: true)
                .Index(t => t.DOCUMENT_ID)
                .Index(t => t.FORUM_ID);

            CreateTable(
                "dbo.REL_FORUM_HYPERLINK",
                c => new
                    {
                        FORUM_ID = c.Guid(nullable: false),
                        HYPERLINK_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.FORUM_ID, t.HYPERLINK_ID })
                .ForeignKey("dbo.FORUM", t => t.FORUM_ID, cascadeDelete: true)
                .ForeignKey("dbo.HYPERLINK", t => t.HYPERLINK_ID, cascadeDelete: true)
                .Index(t => t.FORUM_ID)
                .Index(t => t.HYPERLINK_ID);

            CreateTable(
                "dbo.REL_FORUM_PHOTO",
                c => new
                    {
                        FORUM_ID = c.Guid(nullable: false),
                        PHOTO_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.FORUM_ID, t.PHOTO_ID })
                .ForeignKey("dbo.FORUM", t => t.FORUM_ID, cascadeDelete: true)
                .ForeignKey("dbo.PHOTO", t => t.PHOTO_ID, cascadeDelete: true)
                .Index(t => t.FORUM_ID)
                .Index(t => t.PHOTO_ID);
        }

        public override void Down()
        {
            DropForeignKey("dbo.REL_FORUM_PHOTO", "PHOTO_ID", "dbo.PHOTO");
            DropForeignKey("dbo.REL_FORUM_PHOTO", "FORUM_ID", "dbo.FORUM");
            DropForeignKey("dbo.PHOTO_EXT", "PHOTO_ID", "dbo.PHOTO");
            DropForeignKey("dbo.REL_FORUM_HYPERLINK", "HYPERLINK_ID", "dbo.HYPERLINK");
            DropForeignKey("dbo.REL_FORUM_HYPERLINK", "FORUM_ID", "dbo.FORUM");
            DropForeignKey("dbo.REL_FORUM_DOCUMENT", "FORUM_ID", "dbo.FORUM");
            DropForeignKey("dbo.REL_FORUM_DOCUMENT", "DOCUMENT_ID", "dbo.DOCUMENT");
            DropForeignKey("dbo.DOCUMENT_EXT", "DOCUMENT_ID", "dbo.DOCUMENT");
            DropForeignKey("dbo.DISCUSS", "FORUM_ID", "dbo.FORUM");
            DropForeignKey("dbo.DISCUSS", "USER_ID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ACTIVITY", "FORUM_ID", "dbo.FORUM");
            DropIndex("dbo.REL_FORUM_PHOTO", new[] { "PHOTO_ID" });
            DropIndex("dbo.REL_FORUM_PHOTO", new[] { "FORUM_ID" });
            DropIndex("dbo.REL_FORUM_HYPERLINK", new[] { "HYPERLINK_ID" });
            DropIndex("dbo.REL_FORUM_HYPERLINK", new[] { "FORUM_ID" });
            DropIndex("dbo.REL_FORUM_DOCUMENT", new[] { "FORUM_ID" });
            DropIndex("dbo.REL_FORUM_DOCUMENT", new[] { "DOCUMENT_ID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.PHOTO_EXT", new[] { "PHOTO_ID" });
            DropIndex("dbo.DOCUMENT_EXT", new[] { "DOCUMENT_ID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.DISCUSS", new[] { "USER_ID" });
            DropIndex("dbo.DISCUSS", new[] { "FORUM_ID" });
            DropIndex("dbo.ACTIVITY", new[] { "FORUM_ID" });
            DropTable("dbo.REL_FORUM_PHOTO");
            DropTable("dbo.REL_FORUM_HYPERLINK");
            DropTable("dbo.REL_FORUM_DOCUMENT");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.YOUTUBE");
            DropTable("dbo.PHOTO_EXT");
            DropTable("dbo.PHOTO");
            DropTable("dbo.HYPERLINK");
            DropTable("dbo.DOCUMENT_EXT");
            DropTable("dbo.DOCUMENT");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.DISCUSS");
            DropTable("dbo.FORUM");
            DropTable("dbo.ACTIVITY");
        }
    }
}
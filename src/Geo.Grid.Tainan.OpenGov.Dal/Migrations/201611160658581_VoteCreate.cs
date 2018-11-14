namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class VoteCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vote",
                c => new
                {
                    VoteId = c.Guid(nullable: false),
                    ForumId = c.Guid(nullable: false),
                    Title = c.String(nullable: false, maxLength: 255),
                    Info = c.String(nullable: false),
                    StartDate = c.DateTime(nullable: false),
                    EndDate = c.DateTime(nullable: false),
                    CanVote = c.Boolean(nullable: false),
                    SelectNumber = c.Int(nullable: false),
                    IsPublish = c.Boolean(nullable: false),
                    IsEnabled = c.Boolean(nullable: false),
                    CreatedDate = c.DateTime(nullable: false),
                    CreatedBy = c.String(nullable: false, maxLength: 128),
                    UpdatedDate = c.DateTime(nullable: false),
                    UpdatedBy = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.VoteId)
                .ForeignKey("dbo.FORUM", t => t.ForumId, cascadeDelete: true)
                .Index(t => t.ForumId);

            CreateTable(
                "dbo.VoteBasicGroup",
                c => new
                {
                    GroupId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                    Sort = c.Int(nullable: false),
                    IsEnabled = c.Boolean(nullable: false),
                    CreatedDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.GroupId);

            CreateTable(
                "dbo.VoteBasic",
                c => new
                {
                    BasicId = c.Guid(nullable: false),
                    GroupId = c.Int(nullable: false),
                    Name = c.String(nullable: false, maxLength: 50),
                    Sort = c.Int(nullable: false),
                    IsEnabled = c.Boolean(nullable: false),
                    CreatedDate = c.DateTime(nullable: false),
                    IsExplain = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.BasicId)
                .ForeignKey("dbo.VoteBasicGroup", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId);

            CreateTable(
                "dbo.VoteOption",
                c => new
                {
                    OptionId = c.Guid(nullable: false),
                    VoteId = c.Guid(nullable: false),
                    Title = c.String(nullable: false, maxLength: 255),
                    Sort = c.Int(nullable: false),
                    IsEnabled = c.Boolean(nullable: false),
                    CreatedDate = c.DateTime(nullable: false),
                    CreatedBy = c.String(nullable: false, maxLength: 128),
                    UpdatedDate = c.DateTime(nullable: false),
                    UpdatedBy = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.OptionId)
                .ForeignKey("dbo.Vote", t => t.VoteId, cascadeDelete: true)
                .Index(t => t.VoteId);

            CreateTable(
                "dbo.VoteFillBasic",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    BasicId = c.Guid(nullable: false),
                    VoteId = c.Guid(nullable: false),
                    BasicDesc = c.String(nullable: false, maxLength: 500),
                    GroupId = c.Int(nullable: false),
                    CreatedDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => new { t.UserId, t.BasicId, t.VoteId });

            CreateTable(
                "dbo.VoteFillOption",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    OptionId = c.Guid(nullable: false),
                    VoteId = c.Guid(nullable: false),
                    CreatedDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => new { t.UserId, t.OptionId });

            CreateTable(
                "dbo.VoteRelBasicGroup",
                c => new
                {
                    VoteId = c.Guid(nullable: false),
                    GroupId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.VoteId, t.GroupId })
                .ForeignKey("dbo.Vote", t => t.VoteId, cascadeDelete: true)
                .ForeignKey("dbo.VoteBasicGroup", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.VoteId)
                .Index(t => t.GroupId);            
        }

        public override void Down()
        {
            DropForeignKey("dbo.VoteOption", "VoteId", "dbo.Vote");
            DropForeignKey("dbo.VoteRelBasicGroup", "GroupId", "dbo.VoteBasicGroup");
            DropForeignKey("dbo.VoteRelBasicGroup", "VoteId", "dbo.Vote");
            DropForeignKey("dbo.VoteBasic", "GroupId", "dbo.VoteBasicGroup");
            DropForeignKey("dbo.Vote", "ForumId", "dbo.FORUM");
            DropIndex("dbo.VoteRelBasicGroup", new[] { "GroupId" });
            DropIndex("dbo.VoteRelBasicGroup", new[] { "VoteId" });
            DropIndex("dbo.VoteOption", new[] { "VoteId" });
            DropIndex("dbo.VoteBasic", new[] { "GroupId" });
            DropIndex("dbo.Vote", new[] { "ForumId" });
            DropTable("dbo.VoteRelBasicGroup");
            DropTable("dbo.VoteFillOption");
            DropTable("dbo.VoteFillBasic");
            DropTable("dbo.VoteOption");
            DropTable("dbo.VoteBasic");
            DropTable("dbo.VoteBasicGroup");
            DropTable("dbo.Vote");
        }
    }
}

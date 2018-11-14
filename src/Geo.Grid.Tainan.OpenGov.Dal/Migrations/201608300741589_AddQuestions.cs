namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuestions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MdFillQuest",
                c => new
                    {
                        FillId = c.Guid(nullable: false),
                        InfoId = c.Guid(nullable: false),
                        MdFillUserId = c.Guid(nullable: false),
                        QstId = c.Guid(nullable: false),
                        SetId = c.Guid(),
                        FillDesc = c.String(maxLength: 3000),
                        FillScore = c.Int(nullable: false),
                        EditDate = c.DateTime(nullable: false),
                        Editer = c.Guid(),
                    })
                .PrimaryKey(t => t.FillId)
                .ForeignKey("dbo.MdQuestion", t => t.QstId, cascadeDelete: true)
                .Index(t => t.QstId);
            
            CreateTable(
                "dbo.MdQuestion",
                c => new
                    {
                        QstId = c.Guid(nullable: false),
                        GroupId = c.Guid(nullable: false),
                        QstSorting = c.Int(nullable: false),
                        QstAnsType = c.Int(nullable: false),
                        QstQuestion = c.String(maxLength: 300),
                        QstScore = c.Int(nullable: false),
                        EditDate = c.DateTime(nullable: false),
                        Editer = c.Guid(),
                        IsEnable = c.Boolean(nullable: false),
                        IsRequired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.QstId)
                .ForeignKey("dbo.MdQuestionGroup", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.MdQuestionGroup",
                c => new
                    {
                        GroupId = c.Guid(nullable: false),
                        GroupTitle = c.String(maxLength: 500),
                        InfoId = c.Guid(nullable: false),
                        GroupDesc = c.String(maxLength: 500),
                        GroupSort = c.Int(nullable: false),
                        IsEnable = c.Boolean(nullable: false),
                        EditDate = c.DateTime(nullable: false),
                        Editer = c.Guid(),
                    })
                .PrimaryKey(t => t.GroupId)
                .ForeignKey("dbo.MdQuestInfo", t => t.InfoId, cascadeDelete: true)
                .Index(t => t.InfoId);
            
            CreateTable(
                "dbo.MdQuestInfo",
                c => new
                    {
                        InfoId = c.Guid(nullable: false),
                        InfoTitle = c.String(maxLength: 500),
                        InfoDesc = c.String(maxLength: 500),
                        InfoDateSt = c.DateTime(),
                        InfoDateEnd = c.DateTime(),
                        InfoValid = c.Boolean(nullable: false),
                        InfoFooter = c.String(maxLength: 500),
                        EditDate = c.DateTime(nullable: false),
                        Editer = c.Guid(),
                        IsEnable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.InfoId);
            
            CreateTable(
                "dbo.MdQuestSetItem",
                c => new
                    {
                        SetId = c.Guid(nullable: false),
                        QstId = c.Guid(nullable: false),
                        SetDesc = c.String(maxLength: 100),
                        SetScore = c.Int(nullable: false),
                        SetSorting = c.Int(nullable: false),
                        SetNote = c.Boolean(nullable: false),
                        EditDate = c.DateTime(nullable: false),
                        Editer = c.Guid(),
                        IsEnable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SetId)
                .ForeignKey("dbo.MdQuestion", t => t.QstId, cascadeDelete: true)
                .Index(t => t.QstId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MdFillQuest", "QstId", "dbo.MdQuestion");
            DropForeignKey("dbo.MdQuestSetItem", "QstId", "dbo.MdQuestion");
            DropForeignKey("dbo.MdQuestion", "GroupId", "dbo.MdQuestionGroup");
            DropForeignKey("dbo.MdQuestionGroup", "InfoId", "dbo.MdQuestInfo");
            DropIndex("dbo.MdQuestSetItem", new[] { "QstId" });
            DropIndex("dbo.MdQuestionGroup", new[] { "InfoId" });
            DropIndex("dbo.MdQuestion", new[] { "GroupId" });
            DropIndex("dbo.MdFillQuest", new[] { "QstId" });
            DropTable("dbo.MdQuestSetItem");
            DropTable("dbo.MdQuestInfo");
            DropTable("dbo.MdQuestionGroup");
            DropTable("dbo.MdQuestion");
            DropTable("dbo.MdFillQuest");
        }
    }
}

namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateQuestionsDeleteRel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MdQuestionGroup", "InfoId", "dbo.MdQuestInfo");
            DropForeignKey("dbo.MdQuestion", "GroupId", "dbo.MdQuestionGroup");
            DropForeignKey("dbo.MdFillQuest", "QstId", "dbo.MdQuestion");
            DropForeignKey("dbo.MdQuestSetItem", "QstId", "dbo.MdQuestion");
            AddForeignKey("dbo.MdQuestionGroup", "InfoId", "dbo.MdQuestInfo", "InfoId");
            AddForeignKey("dbo.MdQuestion", "GroupId", "dbo.MdQuestionGroup", "GroupId");
            AddForeignKey("dbo.MdFillQuest", "QstId", "dbo.MdQuestion", "QstId");
            AddForeignKey("dbo.MdQuestSetItem", "QstId", "dbo.MdQuestion", "QstId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MdQuestSetItem", "QstId", "dbo.MdQuestion");
            DropForeignKey("dbo.MdFillQuest", "QstId", "dbo.MdQuestion");
            DropForeignKey("dbo.MdQuestion", "GroupId", "dbo.MdQuestionGroup");
            DropForeignKey("dbo.MdQuestionGroup", "InfoId", "dbo.MdQuestInfo");
            AddForeignKey("dbo.MdQuestSetItem", "QstId", "dbo.MdQuestion", "QstId", cascadeDelete: true);
            AddForeignKey("dbo.MdFillQuest", "QstId", "dbo.MdQuestion", "QstId", cascadeDelete: true);
            AddForeignKey("dbo.MdQuestion", "GroupId", "dbo.MdQuestionGroup", "GroupId", cascadeDelete: true);
            AddForeignKey("dbo.MdQuestionGroup", "InfoId", "dbo.MdQuestInfo", "InfoId", cascadeDelete: true);
        }
    }
}

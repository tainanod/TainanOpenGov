namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestInfoAddVerifyType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MdQuestInfo", "VerifyType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MdQuestInfo", "VerifyType");
        }
    }
}

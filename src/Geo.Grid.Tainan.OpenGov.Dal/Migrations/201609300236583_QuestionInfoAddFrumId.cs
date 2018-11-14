namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestionInfoAddFrumId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MdQuestInfo", "ForumId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MdQuestInfo", "ForumId");
        }
    }
}

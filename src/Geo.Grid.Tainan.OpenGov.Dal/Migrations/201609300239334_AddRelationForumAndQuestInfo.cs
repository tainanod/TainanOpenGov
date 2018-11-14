namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelationForumAndQuestInfo : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.MdQuestInfo", "ForumId");
            AddForeignKey("dbo.MdQuestInfo", "ForumId", "dbo.FORUM", "FORUM_ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MdQuestInfo", "ForumId", "dbo.FORUM");
            DropIndex("dbo.MdQuestInfo", new[] { "ForumId" });
        }
    }
}

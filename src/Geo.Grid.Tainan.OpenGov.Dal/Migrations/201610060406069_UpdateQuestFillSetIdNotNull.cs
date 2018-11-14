namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateQuestFillSetIdNotNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MdFillQuest", "SetId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MdFillQuest", "SetId", c => c.Guid());
        }
    }
}

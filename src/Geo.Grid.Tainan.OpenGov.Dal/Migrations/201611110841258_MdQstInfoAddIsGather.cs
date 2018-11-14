namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MdQstInfoAddIsGather : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MdQuestInfo", "IsGather", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MdQuestInfo", "IsGather");
        }
    }
}

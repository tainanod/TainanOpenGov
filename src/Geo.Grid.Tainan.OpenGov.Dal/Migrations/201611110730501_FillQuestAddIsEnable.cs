namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FillQuestAddIsEnable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MdFillQuest", "IsEnable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MdFillQuest", "IsEnable");
        }
    }
}

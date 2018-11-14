namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InfoDesc3000 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MdQuestInfo", "InfoDesc", c => c.String(maxLength: 3000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MdQuestInfo", "InfoDesc", c => c.String(maxLength: 500));
        }
    }
}

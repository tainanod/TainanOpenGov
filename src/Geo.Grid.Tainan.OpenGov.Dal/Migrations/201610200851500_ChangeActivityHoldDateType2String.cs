namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeActivityHoldDateType2String : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ACTIVITY", "HOLD_DATE", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ACTIVITY", "HOLD_DATE", c => c.DateTime(nullable: false));
        }
    }
}

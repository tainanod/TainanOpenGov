namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddActivityTypeColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ACTIVITY", "ACTIVITY_TYPE", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.ACTIVITY", "ACTIVITY_TYPE");
        }
    }
}
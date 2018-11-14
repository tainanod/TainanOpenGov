namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LeoAddForumDataSetUnitId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FORUM", "DataSetUnitId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FORUM", "DataSetUnitId");
        }
    }
}

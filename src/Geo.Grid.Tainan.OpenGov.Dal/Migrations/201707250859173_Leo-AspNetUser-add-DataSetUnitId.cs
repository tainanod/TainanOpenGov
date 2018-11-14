namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LeoAspNetUseraddDataSetUnitId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DataSetUnitId", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DataSetUnitId");
        }
    }
}

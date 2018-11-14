namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LeoFourmrelDataSetUnit : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.FORUM", "DataSetUnitId");
            AddForeignKey("dbo.FORUM", "DataSetUnitId", "dbo.DataSetUnit", "UnitId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FORUM", "DataSetUnitId", "dbo.DataSetUnit");
            DropIndex("dbo.FORUM", new[] { "DataSetUnitId" });
        }
    }
}

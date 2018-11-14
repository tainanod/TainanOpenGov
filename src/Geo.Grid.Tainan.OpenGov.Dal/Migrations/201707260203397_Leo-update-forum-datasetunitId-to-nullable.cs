namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LeoupdateforumdatasetunitIdtonullable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.FORUM", new[] { "DataSetUnitId" });
            AlterColumn("dbo.FORUM", "DataSetUnitId", c => c.Guid());
            CreateIndex("dbo.FORUM", "DataSetUnitId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.FORUM", new[] { "DataSetUnitId" });
            AlterColumn("dbo.FORUM", "DataSetUnitId", c => c.Guid(nullable: false));
            CreateIndex("dbo.FORUM", "DataSetUnitId");
        }
    }
}

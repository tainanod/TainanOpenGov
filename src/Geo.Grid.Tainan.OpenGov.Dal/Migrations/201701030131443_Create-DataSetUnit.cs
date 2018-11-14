namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDataSetUnit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DataSetUnit",
                c => new
                {
                    UnitId = c.Guid(nullable: false),
                    Title = c.String(nullable: false, maxLength: 255),
                    Sort = c.Int(nullable: false),
                    IsEnabled = c.Boolean(nullable: false),
                    CreatedBy = c.String(nullable: false, maxLength: 128),
                    CreatedDate = c.DateTime(nullable: false),
                    UpdatedBy = c.String(nullable: false, maxLength: 128),
                    UpdatedDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.UnitId);
            
            AddColumn("dbo.DataSet", "UnitId", c => c.Guid(nullable: false));
            CreateIndex("dbo.DataSet", "UnitId");
            AddForeignKey("dbo.DataSet", "UnitId", "dbo.DataSetUnit", "UnitId", cascadeDelete: true);
            DropColumn("dbo.DataSet", "UnitName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DataSet", "UnitName", c => c.String(maxLength: 255));
            DropForeignKey("dbo.DataSet", "UnitId", "dbo.DataSetUnit");
            DropIndex("dbo.DataSet", new[] { "UnitId" });
            DropColumn("dbo.DataSet", "UnitId");
            DropTable("dbo.DataSetUnit");
        }
    }
}

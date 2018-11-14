namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WeiAddTableAllowanceSource : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AllowanceSource",
                c => new
                    {
                        SourceId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 500),
                        Organization = c.String(nullable: false, maxLength: 500),
                        WebSite = c.String(nullable: false, maxLength: 500),
                        ApiUrl = c.String(nullable: false, maxLength: 500),
                        ResourceId = c.Guid(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SourceId);
            
            AddColumn("dbo.Allowance", "SourceId", c => c.Guid());
            AddColumn("dbo.Allowance", "DataId", c => c.Int(nullable: false));
            CreateIndex("dbo.Allowance", "SourceId");
            AddForeignKey("dbo.Allowance", "SourceId", "dbo.AllowanceSource", "SourceId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Allowance", "SourceId", "dbo.AllowanceSource");
            DropIndex("dbo.Allowance", new[] { "SourceId" });
            DropColumn("dbo.Allowance", "DataId");
            DropColumn("dbo.Allowance", "SourceId");
            DropTable("dbo.AllowanceSource");
        }
    }
}

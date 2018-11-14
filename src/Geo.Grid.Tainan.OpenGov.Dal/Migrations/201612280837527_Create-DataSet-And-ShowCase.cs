namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDataSetAndShowCase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShowCase",
                c => new
                    {
                        CaseId = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 400),
                        UserName = c.String(nullable: false, maxLength: 255),
                        Contents = c.String(nullable: false),
                        PhotoId = c.Guid(nullable: false),
                        Sort = c.Int(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 128),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.CaseId);
            
            CreateTable(
                "dbo.DataSet",
                c => new
                    {
                        SetId = c.Guid(nullable: false),
                        TypeId = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 255),
                        UnitName = c.String(nullable: false, maxLength: 255),
                        ContactName = c.String(nullable: false, maxLength: 255),
                        ContactTel = c.String(nullable: false, maxLength: 255),
                        WebUrl = c.String(nullable: false, maxLength: 400),
                        VersonNo = c.String(nullable: false, maxLength: 50),
                        Contents = c.String(nullable: false),
                        Info = c.String(nullable: false),
                        Sort = c.Int(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 128),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.SetId)
                .ForeignKey("dbo.DataSetType", t => t.TypeId, cascadeDelete: true)
                .Index(t => t.TypeId);
            
            CreateTable(
                "dbo.DataSetExtension",
                c => new
                    {
                        ExtensionId = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 255),
                        Sort = c.Int(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 128),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ExtensionId);
            
            CreateTable(
                "dbo.DataSetHistory",
                c => new
                    {
                        HistoryId = c.Guid(nullable: false),
                        SetId = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 255),
                        Contents = c.String(nullable: false),
                        Info = c.String(nullable: false),
                        Sort = c.Int(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 128),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.HistoryId)
                .ForeignKey("dbo.DataSet", t => t.SetId, cascadeDelete: true)
                .Index(t => t.SetId);
            
            CreateTable(
                "dbo.DataSetType",
                c => new
                    {
                        TypeId = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 255),
                        Sort = c.Int(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 128),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.TypeId);
            
            CreateTable(
                "dbo.ShowCaseUrl",
                c => new
                    {
                        UrlId = c.Guid(nullable: false),
                        CaseId = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 400),
                        WebUrl = c.String(nullable: false, maxLength: 800),
                        Sort = c.Int(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 128),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.UrlId)
                .ForeignKey("dbo.ShowCase", t => t.CaseId, cascadeDelete: true)
                .Index(t => t.CaseId);
            
            CreateTable(
                "dbo.DataSetRelExtension",
                c => new
                    {
                        SetId = c.Guid(nullable: false),
                        ExtensionId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.SetId, t.ExtensionId })
                .ForeignKey("dbo.DataSet", t => t.SetId, cascadeDelete: true)
                .ForeignKey("dbo.DataSetExtension", t => t.ExtensionId, cascadeDelete: true)
                .Index(t => t.SetId)
                .Index(t => t.ExtensionId);
            
            CreateTable(
                "dbo.ShowCaseRelDataSet",
                c => new
                    {
                        CaseId = c.Guid(nullable: false),
                        SetId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.CaseId, t.SetId })
                .ForeignKey("dbo.ShowCase", t => t.CaseId, cascadeDelete: true)
                .ForeignKey("dbo.DataSet", t => t.SetId, cascadeDelete: true)
                .Index(t => t.CaseId)
                .Index(t => t.SetId);
            
            CreateTable(
                "dbo.ShowCaseRelPhoto",
                c => new
                    {
                        CaseId = c.Guid(nullable: false),
                        PhotoId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.CaseId, t.PhotoId })
                .ForeignKey("dbo.ShowCase", t => t.CaseId, cascadeDelete: true)
                .ForeignKey("dbo.PHOTO", t => t.PhotoId, cascadeDelete: true)
                .Index(t => t.CaseId)
                .Index(t => t.PhotoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShowCaseUrl", "CaseId", "dbo.ShowCase");
            DropForeignKey("dbo.ShowCaseRelPhoto", "PhotoId", "dbo.PHOTO");
            DropForeignKey("dbo.ShowCaseRelPhoto", "CaseId", "dbo.ShowCase");
            DropForeignKey("dbo.ShowCaseRelDataSet", "SetId", "dbo.DataSet");
            DropForeignKey("dbo.ShowCaseRelDataSet", "CaseId", "dbo.ShowCase");
            DropForeignKey("dbo.DataSet", "TypeId", "dbo.DataSetType");
            DropForeignKey("dbo.DataSetHistory", "SetId", "dbo.DataSet");
            DropForeignKey("dbo.DataSetRelExtension", "ExtensionId", "dbo.DataSetExtension");
            DropForeignKey("dbo.DataSetRelExtension", "SetId", "dbo.DataSet");
            DropIndex("dbo.ShowCaseRelPhoto", new[] { "PhotoId" });
            DropIndex("dbo.ShowCaseRelPhoto", new[] { "CaseId" });
            DropIndex("dbo.ShowCaseRelDataSet", new[] { "SetId" });
            DropIndex("dbo.ShowCaseRelDataSet", new[] { "CaseId" });
            DropIndex("dbo.DataSetRelExtension", new[] { "ExtensionId" });
            DropIndex("dbo.DataSetRelExtension", new[] { "SetId" });
            DropIndex("dbo.ShowCaseUrl", new[] { "CaseId" });
            DropIndex("dbo.DataSetHistory", new[] { "SetId" });
            DropIndex("dbo.DataSet", new[] { "TypeId" });
            DropTable("dbo.ShowCaseRelPhoto");
            DropTable("dbo.ShowCaseRelDataSet");
            DropTable("dbo.DataSetRelExtension");
            DropTable("dbo.ShowCaseUrl");
            DropTable("dbo.DataSetType");
            DropTable("dbo.DataSetHistory");
            DropTable("dbo.DataSetExtension");
            DropTable("dbo.DataSet");
            DropTable("dbo.ShowCase");
        }
    }
}

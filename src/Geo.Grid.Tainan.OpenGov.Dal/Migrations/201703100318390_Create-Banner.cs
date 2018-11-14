namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class CreateBanner : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Banner",
                c => new
                {
                    BannerId = c.Guid(nullable: false),
                    Title = c.String(nullable: false, maxLength: 255),
                    WebUrl = c.String(nullable: false, maxLength: 400),
                    Target = c.Boolean(nullable: false),
                    IsTopEnabled = c.Boolean(nullable: false),
                    IsDeleted = c.Boolean(nullable: false),
                    PhotoId = c.Guid(nullable: false),
                    IsEnabled = c.Boolean(nullable: false),
                    CreatedBy = c.String(nullable: false, maxLength: 128),
                    CreatedDate = c.DateTime(nullable: false),
                    UpdatedBy = c.String(nullable: false, maxLength: 128),
                    UpdatedDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.BannerId);

        }

        public override void Down()
        {
            DropTable("dbo.Banner");
        }
    }
}

namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class adduserImageUrlcolumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ImageUrl", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ImageUrl");
        }
    }
}
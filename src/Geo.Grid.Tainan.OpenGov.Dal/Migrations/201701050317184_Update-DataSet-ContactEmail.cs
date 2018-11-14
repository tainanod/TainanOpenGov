namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDataSetContactEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DataSet", "ContactEmail", c => c.String(nullable: false, maxLength: 800));
            DropColumn("dbo.DataSet", "VersonNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DataSet", "VersonNo", c => c.String(maxLength: 50));
            DropColumn("dbo.DataSet", "ContactEmail");
        }
    }
}

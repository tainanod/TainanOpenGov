namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Leoupdateforumholdernotrequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FORUM", "HOLDER", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FORUM", "HOLDER", c => c.String(nullable: false, maxLength: 100));
        }
    }
}

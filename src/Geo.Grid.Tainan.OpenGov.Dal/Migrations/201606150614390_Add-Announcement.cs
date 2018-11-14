namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAnnouncement : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FORUM", "ANNOUNCEMENT", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FORUM", "ANNOUNCEMENT");
        }
    }
}

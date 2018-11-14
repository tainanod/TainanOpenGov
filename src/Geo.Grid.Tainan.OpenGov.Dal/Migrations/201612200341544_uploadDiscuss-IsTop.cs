namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uploadDiscussIsTop : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DISCUSS", "IS_TOP", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DISCUSS", "IS_TOP");
        }
    }
}

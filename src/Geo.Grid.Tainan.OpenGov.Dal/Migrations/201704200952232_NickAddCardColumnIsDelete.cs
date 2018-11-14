namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NickAddCardColumnIsDelete : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Card", "IsDelete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Card", "IsDelete");
        }
    }
}

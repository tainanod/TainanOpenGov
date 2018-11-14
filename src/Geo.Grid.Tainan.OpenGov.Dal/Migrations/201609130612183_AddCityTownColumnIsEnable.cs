namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCityTownColumnIsEnable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CityTown", "IsEnable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CityTown", "IsEnable");
        }
    }
}

namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCityTown : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CityTown",
                c => new
                    {
                        CityTownId = c.Guid(nullable: false),
                        TownSeq = c.Int(nullable: false),
                        TownName = c.String(maxLength: 50),
                        CitySeq = c.Int(nullable: false),
                        CityName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.CityTownId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CityTown");
        }
    }
}

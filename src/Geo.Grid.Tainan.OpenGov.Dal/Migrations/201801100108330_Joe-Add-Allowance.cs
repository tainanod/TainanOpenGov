namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JoeAddAllowance : DbMigration
    {
        public override void Up()
        {
            //name 由 dbo.Allowance 改為 Allowance，避免索引鍵名稱變為 "PK_dbo.Allowance"
            var allowance = CreateTable(
                "Allowance",
                c => new
                {
                    AllowanceId = c.Guid(nullable: false),
                    AllowanceCode = c.String(nullable: false, maxLength: 10),
                    Name = c.String(nullable: false),
                    Age = c.String(),
                    AgeMin = c.Int(),
                    AgeMax = c.Int(),
                    LiveIn = c.String(),
                    IsLiveIn = c.String(),
                    LiveDays = c.String(),
                    IsLiveDays = c.String(),
                    Identity1 = c.String(),
                    Identity2 = c.String(),
                    Income = c.String(),
                    IncomeValue = c.Int(),
                    Movable = c.String(),
                    MovableValue = c.Int(),
                    Immovable = c.String(),
                    ImmovableValue = c.Int(),
                    Others = c.String(),
                    Docs = c.String(),
                    Receiver = c.String(),
                    Contact = c.String(),
                    MoreInfo = c.String(),
                })
                .PrimaryKey(t => t.AllowanceId);
            //.Index(t => t.AllowanceId, unique: true, clustered: true, name: "PK_Allowance");


            CreateTable(
                "DistrictOffice",
                c => new
                {
                    OfficeId = c.Guid(nullable: false),
                    Name = c.String(nullable: false, maxLength: 500),
                    Lng = c.Double(nullable: false),
                    Lat = c.Double(nullable: false),
                })
                .PrimaryKey(t => t.OfficeId);
                //.Index(t => t.OfficeId, unique: true, clustered: true, name: "PK_DistrictOffice");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.DistrictOffice", "PK_DistrictOffice");
            DropIndex("dbo.Allowance", "PK_Allowance");
            DropTable("dbo.DistrictOffice");
            DropTable("dbo.Allowance");
        }
    }
}

namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateMdCheck : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MdCheck",
                c => new
                    {
                        InfoId = c.Guid(nullable: false),
                        UserEmail = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IsEnabled = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.InfoId, t.UserEmail });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MdCheck");
        }
    }
}

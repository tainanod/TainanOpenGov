namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVoteCheck : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VoteCheck",
                c => new
                    {
                        VoteId = c.Guid(nullable: false),
                        UserEmail = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IsEnabled = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.VoteId, t.UserEmail });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VoteCheck");
        }
    }
}

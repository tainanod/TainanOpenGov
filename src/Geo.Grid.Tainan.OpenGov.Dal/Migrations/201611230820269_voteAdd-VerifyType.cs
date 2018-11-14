namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class voteAddVerifyType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vote", "VerifyType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vote", "VerifyType");
        }
    }
}

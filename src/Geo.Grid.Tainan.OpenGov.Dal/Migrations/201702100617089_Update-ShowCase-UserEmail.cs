namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateShowCaseUserEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShowCase", "UserEmail", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShowCase", "UserEmail");
        }
    }
}

namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MdInfoMdFillUserEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MdFillQuest", "MdFillUserEmail", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MdFillQuest", "MdFillUserEmail");
        }
    }
}

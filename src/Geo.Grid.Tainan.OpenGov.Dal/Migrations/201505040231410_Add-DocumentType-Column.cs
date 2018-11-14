namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDocumentTypeColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DOCUMENT", "DOCUMENT_TYPE", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DOCUMENT", "DOCUMENT_TYPE");
        }
    }
}

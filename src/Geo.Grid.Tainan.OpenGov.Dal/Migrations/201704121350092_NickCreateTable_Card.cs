namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NickCreateTable_Card : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Card",
                c => new
                    {
                        CardId = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 150),
                        Contents = c.String(),
                        IconId = c.Guid(nullable: false),
                        Color = c.Int(nullable: false),
                        WebUrl = c.String(maxLength: 500),
                        Sort = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CardId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Card");
        }
    }
}

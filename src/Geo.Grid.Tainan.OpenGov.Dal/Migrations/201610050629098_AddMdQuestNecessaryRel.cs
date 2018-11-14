namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMdQuestNecessaryRel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MdQuestNecessaryRel",
                c => new
                    {
                        SetId = c.Guid(nullable: false),
                        TargetType = c.Int(nullable: false),
                        TargetId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.SetId)
                .ForeignKey("dbo.MdQuestSetItem", t => t.SetId)
                .Index(t => t.SetId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MdQuestNecessaryRel", "SetId", "dbo.MdQuestSetItem");
            DropIndex("dbo.MdQuestNecessaryRel", new[] { "SetId" });
            DropTable("dbo.MdQuestNecessaryRel");
        }
    }
}

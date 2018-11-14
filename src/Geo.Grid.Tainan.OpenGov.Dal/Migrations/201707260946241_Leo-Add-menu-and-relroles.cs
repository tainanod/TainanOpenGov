namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LeoAddmenuandrelroles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        MenuId = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Controller = c.String(nullable: false),
                        Action = c.String(nullable: false),
                        Area = c.String(),
                        Sort = c.Int(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MenuId);
            
            CreateTable(
                "dbo.REL_AspNetRoles_Menu",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        MenuId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.MenuId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Menu", t => t.MenuId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.MenuId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.REL_AspNetRoles_Menu", "MenuId", "dbo.Menu");
            DropForeignKey("dbo.REL_AspNetRoles_Menu", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.REL_AspNetRoles_Menu", new[] { "MenuId" });
            DropIndex("dbo.REL_AspNetRoles_Menu", new[] { "RoleId" });
            DropTable("dbo.REL_AspNetRoles_Menu");
            DropTable("dbo.Menu");
        }
    }
}

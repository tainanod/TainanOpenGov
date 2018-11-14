namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddForumTypeColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FORUM", "FORUM_TYPE", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.FORUM", "FORUM_TYPE");
        }
    }
}
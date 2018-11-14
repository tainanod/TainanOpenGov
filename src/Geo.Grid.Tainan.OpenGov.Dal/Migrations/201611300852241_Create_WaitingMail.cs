namespace Geo.Grid.Tainan.OpenGov.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_WaitingMail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WaitingMail",
                c => new
                    {
                        MailSeq = c.Int(nullable: false, identity: true),
                        TypeName = c.String(nullable: false, maxLength: 400),
                        ToName = c.String(nullable: false, maxLength: 50),
                        ToEmail = c.String(nullable: false, maxLength: 400),
                        FromName = c.String(nullable: false, maxLength: 50),
                        FromEmail = c.String(nullable: false, maxLength: 400),
                        BccEmail = c.String(nullable: false, maxLength: 800),
                        Subject = c.String(nullable: false, maxLength: 256),
                        MailContent = c.String(nullable: false),
                        IsSend = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        SendTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.MailSeq);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WaitingMail");
        }
    }
}

namespace Messanger.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dialog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        LastMessage_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Message", t => t.LastMessage_Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.LastMessage_Id);
            
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 50),
                        SenderId = c.Int(nullable: false),
                        ReceiverId = c.Int(nullable: false),
                        DialogId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dialog", t => t.DialogId, cascadeDelete: true)
                .Index(t => t.DialogId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        AvatarUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Dialog", "UserId", "dbo.User");
            DropForeignKey("dbo.Dialog", "LastMessage_Id", "dbo.Message");
            DropForeignKey("dbo.Message", "DialogId", "dbo.Dialog");
            DropIndex("dbo.Message", new[] { "DialogId" });
            DropIndex("dbo.Dialog", new[] { "LastMessage_Id" });
            DropIndex("dbo.Dialog", new[] { "UserId" });
            DropTable("dbo.User");
            DropTable("dbo.Message");
            DropTable("dbo.Dialog");
        }
    }
}

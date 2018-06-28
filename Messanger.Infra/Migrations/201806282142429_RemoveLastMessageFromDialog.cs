namespace Messanger.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveLastMessageFromDialog : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Dialog", "LastMessage_Id", "dbo.Message");
            DropIndex("dbo.Dialog", new[] { "LastMessage_Id" });
            DropColumn("dbo.Dialog", "LastMessage_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Dialog", "LastMessage_Id", c => c.Int());
            CreateIndex("dbo.Dialog", "LastMessage_Id");
            AddForeignKey("dbo.Dialog", "LastMessage_Id", "dbo.Message", "Id");
        }
    }
}

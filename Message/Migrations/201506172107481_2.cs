namespace Message.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ResponseMessages", new[] { "FirstMessage_id" });
            CreateIndex("dbo.ResponseMessages", "firstMessage_id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ResponseMessages", new[] { "firstMessage_id" });
            CreateIndex("dbo.ResponseMessages", "FirstMessage_id");
        }
    }
}

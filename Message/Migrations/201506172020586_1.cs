namespace Message.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        publisherID = c.Int(nullable: false),
                        publisherName = c.String(),
                        title = c.String(),
                        body = c.String(),
                        publishDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Threads",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.FirstMessages",
                c => new
                    {
                        id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Messages", t => t.id)
                .Index(t => t.id);
            
            CreateTable(
                "dbo.ResponseMessages",
                c => new
                    {
                        id = c.Int(nullable: false),
                        FirstMessage_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Messages", t => t.id)
                .ForeignKey("dbo.FirstMessages", t => t.FirstMessage_id)
                .Index(t => t.id)
                .Index(t => t.FirstMessage_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ResponseMessages", "FirstMessage_id", "dbo.FirstMessages");
            DropForeignKey("dbo.ResponseMessages", "id", "dbo.Messages");
            DropForeignKey("dbo.FirstMessages", "id", "dbo.Messages");
            DropIndex("dbo.ResponseMessages", new[] { "FirstMessage_id" });
            DropIndex("dbo.ResponseMessages", new[] { "id" });
            DropIndex("dbo.FirstMessages", new[] { "id" });
            DropTable("dbo.ResponseMessages");
            DropTable("dbo.FirstMessages");
            DropTable("dbo.Threads");
            DropTable("dbo.Messages");
        }
    }
}

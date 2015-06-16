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
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        FirstMessage_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Messages", t => t.FirstMessage_id)
                .Index(t => t.FirstMessage_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "FirstMessage_id", "dbo.Messages");
            DropIndex("dbo.Messages", new[] { "FirstMessage_id" });
            DropTable("dbo.Messages");
        }
    }
}

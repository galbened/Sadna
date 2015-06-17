namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fora",
                c => new
                    {
                        forumID = c.Int(nullable: false, identity: true),
                        forumName = c.String(),
                    })
                .PrimaryKey(t => t.forumID);
            
            CreateTable(
                "dbo.SubForums",
                c => new
                    {
                        subForumId = c.Int(nullable: false, identity: true),
                        Topic = c.String(),
                        Forum_forumID = c.Int(),
                    })
                .PrimaryKey(t => t.subForumId)
                .ForeignKey("dbo.Fora", t => t.Forum_forumID)
                .Index(t => t.Forum_forumID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubForums", "Forum_forumID", "dbo.Fora");
            DropIndex("dbo.SubForums", new[] { "Forum_forumID" });
            DropTable("dbo.SubForums");
            DropTable("dbo.Fora");
        }
    }
}

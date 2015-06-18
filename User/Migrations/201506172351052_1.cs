namespace User.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Passwords",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        pass = c.String(),
                        Member_memberID = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Members", t => t.Member_memberID)
                .Index(t => t.Member_memberID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Passwords", "Member_memberID", "dbo.Members");
            DropIndex("dbo.Passwords", new[] { "Member_memberID" });
            DropTable("dbo.Passwords");
        }
    }
}

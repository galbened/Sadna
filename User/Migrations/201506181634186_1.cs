namespace User.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        memberID = c.Int(nullable: false, identity: true),
                        memberUsername = c.String(),
                        memberPassword = c.String(),
                        identifyQuestion = c.String(),
                        identifyAnswer = c.String(),
                        passwordLastChanged = c.DateTime(nullable: false),
                        memberEmail = c.String(),
                        loginStatus = c.Boolean(nullable: false),
                        accountStatus = c.Boolean(nullable: false),
                        confirmationCode = c.Int(nullable: false),
                        Member_memberID = c.Int(),
                    })
                .PrimaryKey(t => t.memberID)
                .ForeignKey("dbo.Members", t => t.Member_memberID)
                .Index(t => t.Member_memberID);
            
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
            DropForeignKey("dbo.Members", "Member_memberID", "dbo.Members");
            DropIndex("dbo.Passwords", new[] { "Member_memberID" });
            DropIndex("dbo.Members", new[] { "Member_memberID" });
            DropTable("dbo.Passwords");
            DropTable("dbo.Members");
        }
    }
}

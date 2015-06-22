namespace User.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Passwords", "passString", c => c.String());
            DropColumn("dbo.Passwords", "pass");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Passwords", "pass", c => c.String());
            DropColumn("dbo.Passwords", "passString");
        }
    }
}

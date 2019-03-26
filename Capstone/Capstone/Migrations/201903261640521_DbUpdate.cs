namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberModels", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MemberModels", "Email");
        }
    }
}

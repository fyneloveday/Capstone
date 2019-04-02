namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbUpdate8 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MemberModels", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MemberModels", "Email", c => c.String(nullable: false));
        }
    }
}

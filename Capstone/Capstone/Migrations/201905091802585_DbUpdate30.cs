namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbUpdate30 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberModels", "Rating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MemberModels", "Rating");
        }
    }
}

namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbUpdate24 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberModels", "ProgressInBook", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MemberModels", "ProgressInBook");
        }
    }
}

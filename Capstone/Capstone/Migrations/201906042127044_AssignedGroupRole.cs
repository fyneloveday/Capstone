namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssignedGroupRole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GroupModels", "GroupRole", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GroupModels", "GroupRole");
        }
    }
}

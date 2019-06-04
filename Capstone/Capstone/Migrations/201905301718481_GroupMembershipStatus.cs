namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroupMembershipStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GroupMembersModels", "GroupMembershipStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GroupMembersModels", "GroupMembershipStatus");
        }
    }
}

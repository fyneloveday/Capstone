namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbUpdate28 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GroupModels", "Members_ID", c => c.Int());
            AddColumn("dbo.MemberModels", "YourGroups_Id", c => c.Int());
            CreateIndex("dbo.GroupModels", "Members_ID");
            CreateIndex("dbo.MemberModels", "YourGroups_Id");
            AddForeignKey("dbo.MemberModels", "YourGroups_Id", "dbo.GroupModels", "Id");
            AddForeignKey("dbo.GroupModels", "Members_ID", "dbo.MemberModels", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GroupModels", "Members_ID", "dbo.MemberModels");
            DropForeignKey("dbo.MemberModels", "YourGroups_Id", "dbo.GroupModels");
            DropIndex("dbo.MemberModels", new[] { "YourGroups_Id" });
            DropIndex("dbo.GroupModels", new[] { "Members_ID" });
            DropColumn("dbo.MemberModels", "YourGroups_Id");
            DropColumn("dbo.GroupModels", "Members_ID");
        }
    }
}

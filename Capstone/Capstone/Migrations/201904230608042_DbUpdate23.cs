namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbUpdate23 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MemberModels", "GroupMembersModel_Id", "dbo.GroupMembersModels");
            DropForeignKey("dbo.GroupMembersModels", "GroupAdmin", "dbo.MemberModels");
            DropIndex("dbo.GroupMembersModels", new[] { "GroupAdmin" });
            DropIndex("dbo.MemberModels", new[] { "GroupMembersModel_Id" });
            DropColumn("dbo.MemberModels", "GroupMembersModel_Id");
            DropTable("dbo.GroupMembersModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GroupMembersModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupAdmin = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.MemberModels", "GroupMembersModel_Id", c => c.Int());
            CreateIndex("dbo.MemberModels", "GroupMembersModel_Id");
            CreateIndex("dbo.GroupMembersModels", "GroupAdmin");
            AddForeignKey("dbo.GroupMembersModels", "GroupAdmin", "dbo.MemberModels", "ID", cascadeDelete: true);
            AddForeignKey("dbo.MemberModels", "GroupMembersModel_Id", "dbo.GroupMembersModels", "Id");
        }
    }
}

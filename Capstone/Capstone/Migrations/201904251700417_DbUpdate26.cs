namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbUpdate26 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GroupMembersModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupId = c.Int(nullable: false),
                        MemberId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GroupModels", t => t.GroupId, cascadeDelete: false)
                .ForeignKey("dbo.MemberModels", t => t.MemberId, cascadeDelete: false)
                .Index(t => t.GroupId)
                .Index(t => t.MemberId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GroupMembersModels", "MemberId", "dbo.MemberModels");
            DropForeignKey("dbo.GroupMembersModels", "GroupId", "dbo.GroupModels");
            DropIndex("dbo.GroupMembersModels", new[] { "MemberId" });
            DropIndex("dbo.GroupMembersModels", new[] { "GroupId" });
            DropTable("dbo.GroupMembersModels");
        }
    }
}

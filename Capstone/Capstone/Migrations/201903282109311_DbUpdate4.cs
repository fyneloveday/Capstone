namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbUpdate4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GroupMembersModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupAdmin = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MemberModels", t => t.GroupAdmin, cascadeDelete: true)
                .Index(t => t.GroupAdmin);
            
            CreateTable(
                "dbo.GroupModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupName = c.String(nullable: false),
                        Description = c.String(),
                        Rules = c.String(),
                        MemberModelId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MemberModels", t => t.MemberModelId)
                .Index(t => t.MemberModelId);
            
            CreateTable(
                "dbo.SendEmailModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ToEmail = c.String(nullable: false),
                        EmailSubject = c.String(),
                        EmailCC = c.String(),
                        EmailBCC = c.String(),
                        EmailBody = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.MemberModels", "GroupMembersModel_Id", c => c.Int());
            CreateIndex("dbo.MemberModels", "GroupMembersModel_Id");
            AddForeignKey("dbo.MemberModels", "GroupMembersModel_Id", "dbo.GroupMembersModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GroupModels", "MemberModelId", "dbo.MemberModels");
            DropForeignKey("dbo.GroupMembersModels", "GroupAdmin", "dbo.MemberModels");
            DropForeignKey("dbo.MemberModels", "GroupMembersModel_Id", "dbo.GroupMembersModels");
            DropIndex("dbo.GroupModels", new[] { "MemberModelId" });
            DropIndex("dbo.MemberModels", new[] { "GroupMembersModel_Id" });
            DropIndex("dbo.GroupMembersModels", new[] { "GroupAdmin" });
            DropColumn("dbo.MemberModels", "GroupMembersModel_Id");
            DropTable("dbo.SendEmailModels");
            DropTable("dbo.GroupModels");
            DropTable("dbo.GroupMembersModels");
        }
    }
}

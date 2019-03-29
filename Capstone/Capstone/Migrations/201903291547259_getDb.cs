namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class getDb : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GroupModels", "MemberModelId", "dbo.MemberModels");
            DropIndex("dbo.GroupModels", new[] { "MemberModelId" });
            AddColumn("dbo.MemberModels", "ApplicationUserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.GroupModels", "MemberModelId", c => c.Int(nullable: false));
            CreateIndex("dbo.MemberModels", "ApplicationUserId");
            CreateIndex("dbo.GroupModels", "MemberModelId");
            AddForeignKey("dbo.MemberModels", "ApplicationUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.GroupModels", "MemberModelId", "dbo.MemberModels", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GroupModels", "MemberModelId", "dbo.MemberModels");
            DropForeignKey("dbo.MemberModels", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.GroupModels", new[] { "MemberModelId" });
            DropIndex("dbo.MemberModels", new[] { "ApplicationUserId" });
            AlterColumn("dbo.GroupModels", "MemberModelId", c => c.Int());
            DropColumn("dbo.MemberModels", "ApplicationUserId");
            CreateIndex("dbo.GroupModels", "MemberModelId");
            AddForeignKey("dbo.GroupModels", "MemberModelId", "dbo.MemberModels", "ID");
        }
    }
}

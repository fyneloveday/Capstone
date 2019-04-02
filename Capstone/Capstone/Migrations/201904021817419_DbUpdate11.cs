namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbUpdate11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookAPIModels", "ApplicationUserId", c => c.String(maxLength: 128));
            AddColumn("dbo.BookAPIModels", "MemberModel_ID", c => c.Int());
            AlterColumn("dbo.BookAPIModels", "Rating", c => c.Int(nullable: false));
            CreateIndex("dbo.BookAPIModels", "ApplicationUserId");
            CreateIndex("dbo.BookAPIModels", "MemberModel_ID");
            AddForeignKey("dbo.BookAPIModels", "ApplicationUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.BookAPIModels", "MemberModel_ID", "dbo.MemberModels", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookAPIModels", "MemberModel_ID", "dbo.MemberModels");
            DropForeignKey("dbo.BookAPIModels", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.BookAPIModels", new[] { "MemberModel_ID" });
            DropIndex("dbo.BookAPIModels", new[] { "ApplicationUserId" });
            AlterColumn("dbo.BookAPIModels", "Rating", c => c.String());
            DropColumn("dbo.BookAPIModels", "MemberModel_ID");
            DropColumn("dbo.BookAPIModels", "ApplicationUserId");
        }
    }
}

namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbUpdate12 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookAPIModels", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.BookAPIModels", new[] { "ApplicationUserId" });
            AddColumn("dbo.BookEntryModels", "Rating", c => c.Int(nullable: false));
            AddColumn("dbo.BookEntryModels", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.BookEntryModels", "ApplicationUserId");
            AddForeignKey("dbo.BookEntryModels", "ApplicationUserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.BookAPIModels", "Rating");
            DropColumn("dbo.BookAPIModels", "ApplicationUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BookAPIModels", "ApplicationUserId", c => c.String(maxLength: 128));
            AddColumn("dbo.BookAPIModels", "Rating", c => c.Int(nullable: false));
            DropForeignKey("dbo.BookEntryModels", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.BookEntryModels", new[] { "ApplicationUserId" });
            DropColumn("dbo.BookEntryModels", "ApplicationUserId");
            DropColumn("dbo.BookEntryModels", "Rating");
            CreateIndex("dbo.BookAPIModels", "ApplicationUserId");
            AddForeignKey("dbo.BookAPIModels", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
    }
}

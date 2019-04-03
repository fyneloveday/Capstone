namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbUpdate18 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReadingListModels", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ReadingListModels", "ApplicationUserId");
            AddForeignKey("dbo.ReadingListModels", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReadingListModels", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.ReadingListModels", new[] { "ApplicationUserId" });
            DropColumn("dbo.ReadingListModels", "ApplicationUserId");
        }
    }
}

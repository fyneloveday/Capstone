namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbUpdate17 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReadingListModels", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.ReadingListModels", new[] { "ApplicationUserId" });
            DropColumn("dbo.ReadingListModels", "ApplicationUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReadingListModels", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ReadingListModels", "ApplicationUserId");
            AddForeignKey("dbo.ReadingListModels", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
    }
}

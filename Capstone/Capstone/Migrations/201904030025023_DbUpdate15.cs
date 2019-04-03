namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbUpdate15 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReadingListModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        AuthorFirstName = c.String(nullable: false),
                        AuthorLastName = c.String(nullable: false),
                        Rating = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReadingListModels", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.ReadingListModels", new[] { "ApplicationUserId" });
            DropTable("dbo.ReadingListModels");
        }
    }
}

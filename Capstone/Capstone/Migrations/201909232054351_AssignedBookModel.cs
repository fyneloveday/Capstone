namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssignedBookModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssignedBooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReadingAssignment = c.String(),
                        Author = c.String(),
                        Body = c.String(storeType: "ntext"),
                        BlogPostID = c.Int(nullable: false),
                        Members_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MemberModels", t => t.Members_ID)
                .Index(t => t.Members_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssignedBooks", "Members_ID", "dbo.MemberModels");
            DropIndex("dbo.AssignedBooks", new[] { "Members_ID" });
            DropTable("dbo.AssignedBooks");
        }
    }
}

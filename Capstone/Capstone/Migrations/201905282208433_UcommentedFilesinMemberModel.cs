namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UcommentedFilesinMemberModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FileId = c.Int(nullable: false),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        FileType = c.Int(nullable: false),
                        Member_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MemberModels", t => t.Member_ID)
                .Index(t => t.Member_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "Member_ID", "dbo.MemberModels");
            DropIndex("dbo.Files", new[] { "Member_ID" });
            DropTable("dbo.Files");
        }
    }
}

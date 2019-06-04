namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFileModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Files", "Member_ID", "dbo.MemberModels");
            DropIndex("dbo.Files", new[] { "Member_ID" });
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        ImageFile = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                        Member_ID = c.Int(),
                    })
                .PrimaryKey(t => t.FileId);
            
            CreateIndex("dbo.Files", "Member_ID");
            AddForeignKey("dbo.Files", "Member_ID", "dbo.MemberModels", "ID");
        }
    }
}

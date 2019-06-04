namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedKeyToFileCs : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Files", "Member_ID", "dbo.MemberModels");
            DropIndex("dbo.Files", new[] { "Member_ID" });
            RenameColumn(table: "dbo.Files", name: "Member_ID", newName: "MemberId");
            DropPrimaryKey("dbo.Files");
            AlterColumn("dbo.Files", "FileId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Files", "MemberId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Files", "FileId");
            CreateIndex("dbo.Files", "MemberId");
            AddForeignKey("dbo.Files", "MemberId", "dbo.MemberModels", "ID", cascadeDelete: true);
            DropColumn("dbo.Files", "ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "ID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Files", "MemberId", "dbo.MemberModels");
            DropIndex("dbo.Files", new[] { "MemberId" });
            DropPrimaryKey("dbo.Files");
            AlterColumn("dbo.Files", "MemberId", c => c.Int());
            AlterColumn("dbo.Files", "FileId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Files", "ID");
            RenameColumn(table: "dbo.Files", name: "MemberId", newName: "Member_ID");
            CreateIndex("dbo.Files", "Member_ID");
            AddForeignKey("dbo.Files", "Member_ID", "dbo.MemberModels", "ID");
        }
    }
}

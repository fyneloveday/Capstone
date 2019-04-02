namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbUpdate13 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.GroupModels", name: "MemberModelId", newName: "GroupAdminId");
            RenameIndex(table: "dbo.GroupModels", name: "IX_MemberModelId", newName: "IX_GroupAdminId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.GroupModels", name: "IX_GroupAdminId", newName: "IX_MemberModelId");
            RenameColumn(table: "dbo.GroupModels", name: "GroupAdminId", newName: "MemberModelId");
        }
    }
}

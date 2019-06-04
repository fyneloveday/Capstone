namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbUpdate31 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberModels", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MemberModels", "ImagePath");
        }
    }
}

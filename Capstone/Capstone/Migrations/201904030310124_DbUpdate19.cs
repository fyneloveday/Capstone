namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbUpdate19 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ReadingListModels", "Rating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReadingListModels", "Rating", c => c.Int(nullable: false));
        }
    }
}

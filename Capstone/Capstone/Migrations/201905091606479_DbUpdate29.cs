namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbUpdate29 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReadingListModels", "Rating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReadingListModels", "Rating");
        }
    }
}

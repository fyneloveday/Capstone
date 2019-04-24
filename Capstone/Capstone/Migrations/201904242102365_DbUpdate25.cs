namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbUpdate25 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReadingListModels", "MyReview", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReadingListModels", "MyReview");
        }
    }
}

namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbUpdate10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookAPIModels", "Rating", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookAPIModels", "Rating");
        }
    }
}

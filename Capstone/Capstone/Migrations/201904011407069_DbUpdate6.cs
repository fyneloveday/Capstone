namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbUpdate6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CurrentlyReadingModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CurrentlyReadingModels");
        }
    }
}

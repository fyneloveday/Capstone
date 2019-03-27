namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddToDb1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookAPIModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Author = c.String(),
                        Publisher = c.String(),
                        ISBN = c.Int(nullable: false),
                        Synopsis = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BookAPIModels");
        }
    }
}

namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbUpdate3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookEntryModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        AuthorFirstName = c.String(nullable: false),
                        AuthorMiddleName = c.String(),
                        AuthorLastName = c.String(nullable: false),
                        YearPublished = c.Int(nullable: false),
                        ISBN = c.Int(nullable: false),
                        Publisher = c.String(nullable: false),
                        Synopsis = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AlterColumn("dbo.BookAPIModels", "ISBN", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BookAPIModels", "ISBN", c => c.Int(nullable: false));
            DropTable("dbo.BookEntryModels");
        }
    }
}

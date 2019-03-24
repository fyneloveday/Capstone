namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MemberUpdate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MemberModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        MiddleName = c.String(),
                        LastName = c.String(nullable: false),
                        Password = c.String(),
                        Age = c.Int(nullable: false),
                        Gender = c.String(),
                        MemberSince = c.DateTime(nullable: false),
                        FavoriteBook = c.String(),
                        AboutYourself = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MemberModels");
        }
    }
}

namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbUpdate21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberModels", "CurrentlyReading", c => c.String());
            AddColumn("dbo.GroupModels", "ReadingAssignment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GroupModels", "ReadingAssignment");
            DropColumn("dbo.MemberModels", "CurrentlyReading");
        }
    }
}

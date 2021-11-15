namespace TravelBadgers.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MemberIndexing : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUser", "DateRegistered", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationUser", "DateRegistered");
        }
    }
}

namespace TravelBadgers.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Request : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Request", "MemberId", "dbo.Member");
            DropIndex("dbo.Request", new[] { "MemberId" });
            DropColumn("dbo.Request", "MemberId");
            DropColumn("dbo.Request", "FromHomeTown");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Request", "FromHomeTown", c => c.Boolean(nullable: false));
            AddColumn("dbo.Request", "MemberId", c => c.Int(nullable: false));
            CreateIndex("dbo.Request", "MemberId");
            AddForeignKey("dbo.Request", "MemberId", "dbo.Member", "MemberId", cascadeDelete: true);
        }
    }
}

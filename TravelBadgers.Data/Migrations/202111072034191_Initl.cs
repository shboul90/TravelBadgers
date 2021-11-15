namespace TravelBadgers.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Airport",
                c => new
                    {
                        AirportId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        AirportName = c.String(nullable: false),
                        AirpotZipCode = c.String(nullable: false),
                        LocationNorth = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LocationWest = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.AirportId);
            
            CreateTable(
                "dbo.City",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        CityName = c.String(nullable: false),
                        CityZipCode = c.String(nullable: false),
                        LocationNorth = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LocationWest = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AvgHotelDailyCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AvgEntertainmentDaily = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AvgFoodDaily = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cuisine = c.Int(nullable: false),
                        Climate = c.Int(nullable: false),
                        Terrain = c.Int(nullable: false),
                        CityRating = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.CityId);
            
            CreateTable(
                "dbo.Member",
                c => new
                    {
                        MemberId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        StreetAddress = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                        PhoneNumber = c.String(),
                        DateJoined = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MemberId);
            
            CreateTable(
                "dbo.Request",
                c => new
                    {
                        RequestId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        MemberId = c.Int(nullable: false),
                        FromHomeTown = c.Boolean(nullable: false),
                        CityId = c.Int(nullable: false),
                        OverallBudget = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DepartDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                        CreatedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUtc = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.RequestId)
                .ForeignKey("dbo.City", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.Member", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.MemberId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.TripOverview",
                c => new
                    {
                        TripOverviewId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        RequestId = c.Int(nullable: false),
                        CreatedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        ArrivalCity_CityId = c.Int(),
                    })
                .PrimaryKey(t => t.TripOverviewId)
                .ForeignKey("dbo.City", t => t.ArrivalCity_CityId)
                .ForeignKey("dbo.Request", t => t.RequestId, cascadeDelete: true)
                .Index(t => t.RequestId)
                .Index(t => t.ArrivalCity_CityId);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.TripOverview", "RequestId", "dbo.Request");
            DropForeignKey("dbo.TripOverview", "ArrivalCity_CityId", "dbo.City");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.Request", "MemberId", "dbo.Member");
            DropForeignKey("dbo.Request", "CityId", "dbo.City");
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.TripOverview", new[] { "ArrivalCity_CityId" });
            DropIndex("dbo.TripOverview", new[] { "RequestId" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.Request", new[] { "CityId" });
            DropIndex("dbo.Request", new[] { "MemberId" });
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.TripOverview");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Request");
            DropTable("dbo.Member");
            DropTable("dbo.City");
            DropTable("dbo.Airport");
        }
    }
}

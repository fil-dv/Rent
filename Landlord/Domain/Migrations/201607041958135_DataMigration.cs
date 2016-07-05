namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        AreaID = c.Int(nullable: false, identity: true),
                        OwnerName = c.String(),
                        ContactaName = c.String(),
                        ContactaPhone1 = c.String(),
                        ContactaPhone2 = c.String(),
                        ContactaPhone3 = c.String(),
                        LegalAddressRegion = c.String(),
                        LegalAddressCity = c.String(),
                        LegalAddressStreet = c.String(),
                        RentAreaAddressRegion = c.String(),
                        RentAreaAddressCity = c.String(),
                        RentAreaAddressStreet = c.String(),
                        SquareArea = c.Decimal(nullable: false, precision: 10, scale: 2),
                        MonthPrice = c.Decimal(nullable: false, precision: 10, scale: 2),
                        IsAvailable = c.Boolean(nullable: false),
                        Rating = c.Int(nullable: false),
                        Latitude = c.Decimal(precision: 16, scale: 4),
                        Longitude = c.Decimal(precision: 16, scale: 4),
                    })
                .PrimaryKey(t => t.AreaID);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        PhotoID = c.Int(nullable: false, identity: true),
                        AreaID = c.Int(nullable: false),
                        PathToPhoto = c.String(),
                        PhotoName = c.String(),
                        Latitude = c.Decimal(precision: 16, scale: 4),
                        Longitude = c.Decimal(precision: 16, scale: 4),
                    })
                .PrimaryKey(t => t.PhotoID);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    UserID = c.Int(nullable: false, identity: true),
                    UserName = c.String(),
                    UserPassword = c.String(),
                    UserRole = c.String(),
                    UserEmail = c.String(),
                })
                .PrimaryKey(t => t.UserID);
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Photos");
            DropTable("dbo.Areas");            
        }
    }
}

namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewPrecision : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Areas", "Latitude", c => c.Decimal(precision: 16, scale: 10));
            AlterColumn("dbo.Areas", "Longitude", c => c.Decimal(precision: 16, scale: 10));
            AlterColumn("dbo.Photos", "Latitude", c => c.Decimal(precision: 16, scale: 10));
            AlterColumn("dbo.Photos", "Longitude", c => c.Decimal(precision: 16, scale: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Photos", "Longitude", c => c.Decimal(precision: 16, scale: 4));
            AlterColumn("dbo.Photos", "Latitude", c => c.Decimal(precision: 16, scale: 4));
            AlterColumn("dbo.Areas", "Longitude", c => c.Decimal(precision: 16, scale: 4));
            AlterColumn("dbo.Areas", "Latitude", c => c.Decimal(precision: 16, scale: 4));
        }
    }
}

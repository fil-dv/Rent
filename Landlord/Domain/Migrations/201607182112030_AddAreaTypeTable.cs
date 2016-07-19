namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAreaTypeTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
               "dbo.AreaTypes",
               c => new
               {
                   AreaTypeID = c.Int(nullable: false, identity: true),
                   AreaTypeName = c.String()
               })
               .PrimaryKey(t => t.AreaTypeID);
        }
        
        public override void Down()
        {
            DropTable("dbo.AreaTypes");
        }
    }
}

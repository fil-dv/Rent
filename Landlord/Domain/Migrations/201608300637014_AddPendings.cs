namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPendings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pendings",
                c => new
                    {
                        PendingID = c.Int(nullable: false, identity: true),
                        AreaID = c.Int(nullable: false),
                        Start = c.DateTime(nullable: false),
                        Stop = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PendingID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Pendings");
        }
    }
}

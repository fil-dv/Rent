namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTimeNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pendings", "Start", c => c.DateTime());
            AlterColumn("dbo.Pendings", "Stop", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pendings", "Stop", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Pendings", "Start", c => c.DateTime(nullable: false));
        }
    }
}

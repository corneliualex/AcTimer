namespace AcTimer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_ActivityModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Activities", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Activities", "Description", c => c.String());
        }
    }
}

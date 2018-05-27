namespace AcTimer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_PropertyConstraintsForModels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Activities", "Description", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Activities", "Description", c => c.String(nullable: false));
        }
    }
}

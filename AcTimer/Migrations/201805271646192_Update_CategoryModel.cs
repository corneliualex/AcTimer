namespace AcTimer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_CategoryModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String());
        }
    }
}

namespace AcTimer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateActivityModel : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Activities", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Activities", "ApplicationUserId");
            RenameColumn(table: "dbo.Activities", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            AlterColumn("dbo.Activities", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Activities", "ApplicationUserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Activities", new[] { "ApplicationUserId" });
            AlterColumn("dbo.Activities", "ApplicationUserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Activities", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            AddColumn("dbo.Activities", "ApplicationUserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Activities", "ApplicationUser_Id");
        }
    }
}

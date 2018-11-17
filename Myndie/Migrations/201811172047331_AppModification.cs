namespace Myndie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppModification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Application", "ImageUrl", c => c.String(unicode: false));
            AlterColumn("dbo.Application", "Archive", c => c.String(maxLength: 255, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            AlterColumn("Application", "Archive", c => c.String(nullable: false, maxLength: 255, storeType: "nvarchar"));
            DropColumn("Application", "ImageUrl");
        }
    }
}

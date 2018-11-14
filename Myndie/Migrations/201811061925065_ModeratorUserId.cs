namespace Myndie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModeratorUserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Moderator", "UserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Moderator", "UserId");
        }
    }
}

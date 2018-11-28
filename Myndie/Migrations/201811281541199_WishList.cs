namespace Myndie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WishList : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Wishlist",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ApplicationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Application", t => t.ApplicationId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ApplicationId);
            
            AddColumn("dbo.Application", "Value", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("Wishlist", "UserId", "User");
            DropForeignKey("Wishlist", "ApplicationId", "Application");
            DropIndex("Wishlist", new[] { "ApplicationId" });
            DropIndex("Wishlist", new[] { "UserId" });
            DropColumn("Application", "Value");
            DropTable("Wishlist");
        }
    }
}

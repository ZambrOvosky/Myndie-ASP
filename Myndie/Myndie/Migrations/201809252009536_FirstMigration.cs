namespace Myndie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationGenre",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationId = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Application", t => t.ApplicationId, cascadeDelete: true)
                .ForeignKey("dbo.Genre", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.ApplicationId)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.Application",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        Desc = c.String(nullable: false, maxLength: 500, storeType: "nvarchar"),
                        Version = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        Price = c.Double(nullable: false),
                        PublisherName = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        ReleaseDate = c.DateTime(nullable: false, precision: 0),
                        Archive = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                        Approved = c.Int(nullable: false),
                        DeveloperId = c.Int(nullable: false),
                        TypeAppId = c.Int(nullable: false),
                        PegiId = c.Int(nullable: false),
                        ModeratorId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Developer", t => t.DeveloperId, cascadeDelete: true)
                .ForeignKey("dbo.Moderator", t => t.ModeratorId)
                .ForeignKey("dbo.Pegi", t => t.PegiId, cascadeDelete: true)
                .ForeignKey("dbo.TypeApp", t => t.TypeAppId, cascadeDelete: true)
                .Index(t => t.Name, unique: true)
                .Index(t => t.DeveloperId)
                .Index(t => t.TypeAppId)
                .Index(t => t.PegiId)
                .Index(t => t.ModeratorId);
            
            CreateTable(
                "dbo.Developer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Info = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                        NumSoft = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Moderator",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pegi",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhotoPath = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                        Age = c.String(nullable: false, maxLength: 2, storeType: "nvarchar"),
                        Desc = c.String(nullable: false, maxLength: 45, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.PhotoPath, unique: true);
            
            CreateTable(
                "dbo.TypeApp",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Genre",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(nullable: false, unicode: false),
                        Date = c.DateTime(nullable: false, precision: 0),
                        UserId = c.Int(nullable: false),
                        ApplicationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Application", t => t.ApplicationId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ApplicationId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 25, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        Password = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        Email = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        BirthDate = c.DateTime(nullable: false, precision: 0),
                        Picture = c.String(unicode: false),
                        CrtDate = c.DateTime(nullable: false, precision: 0),
                        CountryId = c.Int(nullable: false),
                        LanguageId = c.Int(nullable: false),
                        DeveloperId = c.Int(),
                        ModeratorId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Country", t => t.CountryId, cascadeDelete: true)
                .ForeignKey("dbo.Developer", t => t.DeveloperId)
                .ForeignKey("dbo.Language", t => t.LanguageId, cascadeDelete: true)
                .ForeignKey("dbo.Moderator", t => t.ModeratorId)
                .Index(t => t.CountryId)
                .Index(t => t.LanguageId)
                .Index(t => t.DeveloperId)
                .Index(t => t.ModeratorId);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Language",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 45, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Image",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                        UserId = c.Int(nullable: false),
                        ApplicationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Application", t => t.ApplicationId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.Url, unique: true)
                .Index(t => t.UserId)
                .Index(t => t.ApplicationId);
            
            CreateTable(
                "dbo.Review",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                        Desc = c.String(nullable: false, maxLength: 512, storeType: "nvarchar"),
                        Date = c.DateTime(nullable: false, precision: 0),
                        Version = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        ApplicationId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Application", t => t.ApplicationId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ApplicationId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.SellItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PriceItem = c.Double(nullable: false),
                        ApplicationId = c.Int(nullable: false),
                        SellId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Application", t => t.ApplicationId, cascadeDelete: true)
                .ForeignKey("dbo.Sell", t => t.SellId, cascadeDelete: true)
                .Index(t => t.ApplicationId)
                .Index(t => t.SellId);
            
            CreateTable(
                "dbo.Sell",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TotalPrice = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false, precision: 0),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UpdateNotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(nullable: false, maxLength: 512, storeType: "nvarchar"),
                        ApplicationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Application", t => t.ApplicationId, cascadeDelete: true)
                .Index(t => t.ApplicationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("UpdateNotes", "ApplicationId", "Application");
            DropForeignKey("SellItem", "SellId", "Sell");
            DropForeignKey("Sell", "UserId", "User");
            DropForeignKey("SellItem", "ApplicationId", "Application");
            DropForeignKey("Review", "UserId", "User");
            DropForeignKey("Review", "ApplicationId", "Application");
            DropForeignKey("Image", "UserId", "User");
            DropForeignKey("Image", "ApplicationId", "Application");
            DropForeignKey("Comment", "UserId", "User");
            DropForeignKey("User", "ModeratorId", "Moderator");
            DropForeignKey("User", "LanguageId", "Language");
            DropForeignKey("User", "DeveloperId", "Developer");
            DropForeignKey("User", "CountryId", "Country");
            DropForeignKey("Comment", "ApplicationId", "Application");
            DropForeignKey("ApplicationGenre", "GenreId", "Genre");
            DropForeignKey("ApplicationGenre", "ApplicationId", "Application");
            DropForeignKey("Application", "TypeAppId", "TypeApp");
            DropForeignKey("Application", "PegiId", "Pegi");
            DropForeignKey("Application", "ModeratorId", "Moderator");
            DropForeignKey("Application", "DeveloperId", "Developer");
            DropIndex("UpdateNotes", new[] { "ApplicationId" });
            DropIndex("Sell", new[] { "UserId" });
            DropIndex("SellItem", new[] { "SellId" });
            DropIndex("SellItem", new[] { "ApplicationId" });
            DropIndex("Review", new[] { "UserId" });
            DropIndex("Review", new[] { "ApplicationId" });
            DropIndex("Image", new[] { "ApplicationId" });
            DropIndex("Image", new[] { "UserId" });
            DropIndex("Image", new[] { "Url" });
            DropIndex("Language", new[] { "Name" });
            DropIndex("Country", new[] { "Name" });
            DropIndex("User", new[] { "ModeratorId" });
            DropIndex("User", new[] { "DeveloperId" });
            DropIndex("User", new[] { "LanguageId" });
            DropIndex("User", new[] { "CountryId" });
            DropIndex("Comment", new[] { "ApplicationId" });
            DropIndex("Comment", new[] { "UserId" });
            DropIndex("Genre", new[] { "Name" });
            DropIndex("TypeApp", new[] { "Name" });
            DropIndex("Pegi", new[] { "PhotoPath" });
            DropIndex("Application", new[] { "ModeratorId" });
            DropIndex("Application", new[] { "PegiId" });
            DropIndex("Application", new[] { "TypeAppId" });
            DropIndex("Application", new[] { "DeveloperId" });
            DropIndex("Application", new[] { "Name" });
            DropIndex("ApplicationGenre", new[] { "GenreId" });
            DropIndex("ApplicationGenre", new[] { "ApplicationId" });
            DropTable("UpdateNotes");
            DropTable("Sell");
            DropTable("SellItem");
            DropTable("Review");
            DropTable("Image");
            DropTable("Language");
            DropTable("Country");
            DropTable("User");
            DropTable("Comment");
            DropTable("Genre");
            DropTable("TypeApp");
            DropTable("Pegi");
            DropTable("Moderator");
            DropTable("Developer");
            DropTable("Application");
            DropTable("ApplicationGenre");
        }
    }
}

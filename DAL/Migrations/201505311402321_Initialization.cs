namespace ZigmaWeb.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialization : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "membership.BinaryProfile",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Key = c.Int(nullable: false),
                        Value = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("membership.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "membership.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 256),
                        LoweredEmail = c.String(nullable: false, maxLength: 256),
                        Sexuality = c.Byte(nullable: false),
                        BirthDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "zwcore.Content",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Type = c.Byte(nullable: false),
                        Title = c.String(nullable: false, maxLength: 32),
                        Text = c.String(nullable: false),
                        CultureLcid = c.Int(nullable: false),
                        Published = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastModifyDate = c.DateTime(),
                        PublishDate = c.DateTime(),
                        MetaDescription = c.String(maxLength: 512),
                        MetaKeyWords = c.String(maxLength: 512),
                        VisitCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("membership.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "zwcore.Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContentId = c.Int(nullable: false),
                        Text = c.String(nullable: false, maxLength: 1000),
                        CreateDate = c.DateTime(nullable: false),
                        IsPrivate = c.Boolean(nullable: false),
                        Status = c.Byte(nullable: false),
                        SenderName = c.String(maxLength: 50, fixedLength: true),
                        SenderEmail = c.String(maxLength: 50, fixedLength: true),
                        SenderWebSite = c.String(maxLength: 50, fixedLength: true),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("zwcore.Content", t => t.ContentId, cascadeDelete: true)
                .Index(t => t.ContentId);
            
            CreateTable(
                "zwcore.SourceContent",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        CultureLcid = c.Int(nullable: false),
                        DomainId = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 256),
                        Url = c.String(nullable: false, maxLength: 2048),
                        Summary = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("zwcore.Content", t => t.Id)
                .ForeignKey("zwcore.Domain", t => t.DomainId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.DomainId);
            
            CreateTable(
                "zwcore.Domain",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "membership.Membership",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Password = c.Binary(nullable: false, maxLength: 256, fixedLength: true),
                        IsApproved = c.Boolean(nullable: false),
                        IsLockedOut = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastLoginDate = c.DateTime(),
                        LastPasswordChangedDate = c.DateTime(),
                        LastLockoutDate = c.DateTime(),
                        FailedPasswordAttemptCount = c.Int(),
                        VerificationCode = c.Guid(nullable: false),
                        VerificationCodeSendDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("membership.User", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "membership.Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Title = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "membership.TextProfile",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Key = c.Int(nullable: false),
                        Value = c.String(nullable: false, maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("membership.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "zwcore.ForbiddenIdentifier",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Identifier = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "membership.UserRole",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("membership.Role", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("membership.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("membership.BinaryProfile", "UserId", "membership.User");
            DropForeignKey("membership.TextProfile", "UserId", "membership.User");
            DropForeignKey("membership.UserRole", "UserId", "membership.User");
            DropForeignKey("membership.UserRole", "RoleId", "membership.Role");
            DropForeignKey("membership.Membership", "Id", "membership.User");
            DropForeignKey("zwcore.Content", "UserId", "membership.User");
            DropForeignKey("zwcore.SourceContent", "DomainId", "zwcore.Domain");
            DropForeignKey("zwcore.SourceContent", "Id", "zwcore.Content");
            DropForeignKey("zwcore.Comment", "ContentId", "zwcore.Content");
            DropIndex("membership.UserRole", new[] { "UserId" });
            DropIndex("membership.UserRole", new[] { "RoleId" });
            DropIndex("membership.TextProfile", new[] { "UserId" });
            DropIndex("membership.Membership", new[] { "Id" });
            DropIndex("zwcore.SourceContent", new[] { "DomainId" });
            DropIndex("zwcore.SourceContent", new[] { "Id" });
            DropIndex("zwcore.Comment", new[] { "ContentId" });
            DropIndex("zwcore.Content", new[] { "UserId" });
            DropIndex("membership.BinaryProfile", new[] { "UserId" });
            DropTable("membership.UserRole");
            DropTable("zwcore.ForbiddenIdentifier");
            DropTable("membership.TextProfile");
            DropTable("membership.Role");
            DropTable("membership.Membership");
            DropTable("zwcore.Domain");
            DropTable("zwcore.SourceContent");
            DropTable("zwcore.Comment");
            DropTable("zwcore.Content");
            DropTable("membership.User");
            DropTable("membership.BinaryProfile");
        }
    }
}

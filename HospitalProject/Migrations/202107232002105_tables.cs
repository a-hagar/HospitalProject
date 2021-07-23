namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        JobId = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(),
                        JobDepartment = c.String(),
                        JobDescription = c.String(),
                        JobPublishDate = c.DateTime(nullable: false),
                        JobDeadline = c.DateTime(nullable: false),
                        JobType = c.String(),
                        JobLocation = c.String(),
                    })
                .PrimaryKey(t => t.JobId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        userid = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        AddressId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.userid)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        Street = c.String(),
                        Number = c.String(),
                        PostalCode = c.String(),
                        Province = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.AddressId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Users", new[] { "AddressId" });
            DropTable("dbo.Addresses");
            DropTable("dbo.Users");
            DropTable("dbo.Jobs");
        }
    }
}

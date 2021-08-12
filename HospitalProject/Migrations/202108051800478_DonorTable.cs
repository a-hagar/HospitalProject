namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DonorTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Donations",
                c => new
                    {
                        DonationId = c.Int(nullable: false, identity: true),
                        DonorId = c.Int(nullable: false),
                        DonationDate = c.DateTime(nullable: false),
                        Amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DonationId)
                .ForeignKey("dbo.Donors", t => t.DonorId, cascadeDelete: true)
                .Index(t => t.DonorId);
            
            CreateTable(
                "dbo.Donors",
                c => new
                    {
                        DonorId = c.Int(nullable: false, identity: true),
                        userid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DonorId)
                .ForeignKey("dbo.Users", t => t.userid, cascadeDelete: true)
                .Index(t => t.userid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Donations", "DonorId", "dbo.Donors");
            DropForeignKey("dbo.Donors", "userid", "dbo.Users");
            DropIndex("dbo.Donors", new[] { "userid" });
            DropIndex("dbo.Donations", new[] { "DonorId" });
            DropTable("dbo.Donors");
            DropTable("dbo.Donations");
        }
    }
}

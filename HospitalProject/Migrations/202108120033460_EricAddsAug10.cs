namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EricAddsAug10 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Donors", new[] { "userid" });
            CreateIndex("dbo.Donors", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Donors", new[] { "UserId" });
            CreateIndex("dbo.Donors", "userid");
        }
    }
}

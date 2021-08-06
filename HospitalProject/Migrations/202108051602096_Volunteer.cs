namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Volunteer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Volunteers",
                c => new
                    {
                        VolunteerID = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.VolunteerID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Volunteers", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Volunteers", new[] { "UserID" });
            DropTable("dbo.Volunteers");
        }
    }
}

namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patientlogcorrectionsmital : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PatientLogs",
                c => new
                    {
                        PatientID = c.Int(nullable: false, identity: true),
                        PatientDateIn = c.DateTime(nullable: false),
                        PatientDateOut = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PatientID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PatientLogs");
        }
    }
}

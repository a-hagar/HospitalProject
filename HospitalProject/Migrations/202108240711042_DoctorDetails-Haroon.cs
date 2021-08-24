namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DoctorDetailsHaroon : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DoctorDetails",
                c => new
                    {
                        DoctorId = c.Int(nullable: false, identity: true),
                        DoctorFname = c.String(),
                        DoctorLname = c.String(),
                        DoctorDesignation = c.String(),
                        DoctorEmail = c.String(),
                        DepartmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DoctorId)
                .ForeignKey("dbo.Departments", t => t.DepartmentID, cascadeDelete: true)
                .Index(t => t.DepartmentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DoctorDetails", "DepartmentID", "dbo.Departments");
            DropIndex("dbo.DoctorDetails", new[] { "DepartmentID" });
            DropTable("dbo.DoctorDetails");
        }
    }
}

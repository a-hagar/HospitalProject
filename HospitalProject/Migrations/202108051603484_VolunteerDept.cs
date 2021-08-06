namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VolunteerDept : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VolunteerDepts",
                c => new
                    {
                        VolDepID = c.Int(nullable: false, identity: true),
                        VolID = c.Int(nullable: false),
                        DeptID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VolDepID)
                .ForeignKey("dbo.Departments", t => t.DeptID, cascadeDelete: true)
                .ForeignKey("dbo.Volunteers", t => t.VolID, cascadeDelete: true)
                .Index(t => t.VolID)
                .Index(t => t.DeptID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VolunteerDepts", "VolID", "dbo.Volunteers");
            DropForeignKey("dbo.VolunteerDepts", "DeptID", "dbo.Departments");
            DropIndex("dbo.VolunteerDepts", new[] { "DeptID" });
            DropIndex("dbo.VolunteerDepts", new[] { "VolID" });
            DropTable("dbo.VolunteerDepts");
        }
    }
}

namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NavaneethVolunteerDept : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DepartmentID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
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
            
            CreateTable(
                "dbo.Volunteers",
                c => new
                    {
                        VolunteerID = c.Int(nullable: false, identity: true),
                        VolunteerBadge = c.String(),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.VolunteerID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VolunteerDepts", "VolID", "dbo.Volunteers");
            DropForeignKey("dbo.Volunteers", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.VolunteerDepts", "DeptID", "dbo.Departments");
            DropForeignKey("dbo.Departments", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Volunteers", new[] { "UserID" });
            DropIndex("dbo.VolunteerDepts", new[] { "DeptID" });
            DropIndex("dbo.VolunteerDepts", new[] { "VolID" });
            DropIndex("dbo.Departments", new[] { "UserID" });
            DropTable("dbo.Volunteers");
            DropTable("dbo.VolunteerDepts");
            DropTable("dbo.Departments");
        }
    }
}

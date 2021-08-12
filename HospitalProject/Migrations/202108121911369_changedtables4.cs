namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedtables4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "DepartmentID", c => c.Int(nullable: false));
            CreateIndex("dbo.Jobs", "DepartmentID");
            AddForeignKey("dbo.Jobs", "DepartmentID", "dbo.Departments", "DepartmentID", cascadeDelete: true);
            DropColumn("dbo.Jobs", "JobDepartment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "JobDepartment", c => c.String());
            DropForeignKey("dbo.Jobs", "DepartmentID", "dbo.Departments");
            DropIndex("dbo.Jobs", new[] { "DepartmentID" });
            DropColumn("dbo.Jobs", "DepartmentID");
        }
    }
}

namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConflictSolutionNavaneeth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "DepartmentStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Departments", "DepartmentStatus");
        }
    }
}

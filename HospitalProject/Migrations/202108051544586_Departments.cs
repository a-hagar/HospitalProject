namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Departments : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departments", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Departments", new[] { "UserID" });
            DropTable("dbo.Departments");
        }
    }
}

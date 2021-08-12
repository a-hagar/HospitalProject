namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserChange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Donors", "UserId", "dbo.Users");
            DropIndex("dbo.Donors", new[] { "UserId" });
            AlterColumn("dbo.Donors", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Donors", "UserId");
            AddForeignKey("dbo.Donors", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Donors", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Donors", new[] { "UserId" });
            AlterColumn("dbo.Donors", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Donors", "UserId");
            AddForeignKey("dbo.Donors", "UserId", "dbo.Users", "userid", cascadeDelete: true);
        }
    }
}

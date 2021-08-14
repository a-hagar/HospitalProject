namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedsubmissions2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Submissions", "UserId", "dbo.Users");
            DropIndex("dbo.Submissions", new[] { "UserId" });
            DropColumn("dbo.Submissions", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Submissions", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Submissions", "UserId");
            AddForeignKey("dbo.Submissions", "UserId", "dbo.Users", "userid", cascadeDelete: true);
        }
    }
}

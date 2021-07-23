namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moretables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        FileName = c.String(),
                        FileExtension = c.String(),
                    })
                .PrimaryKey(t => t.FileId);
            
            CreateTable(
                "dbo.Submissions",
                c => new
                    {
                        SubmissionId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        JobId = c.Int(nullable: false),
                        SubmissionDate = c.DateTime(nullable: false),
                        FileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubmissionId)
                .ForeignKey("dbo.Files", t => t.FileId, cascadeDelete: true)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.JobId)
                .Index(t => t.FileId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Submissions", "UserId", "dbo.Users");
            DropForeignKey("dbo.Submissions", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.Submissions", "FileId", "dbo.Files");
            DropIndex("dbo.Submissions", new[] { "FileId" });
            DropIndex("dbo.Submissions", new[] { "JobId" });
            DropIndex("dbo.Submissions", new[] { "UserId" });
            DropTable("dbo.Submissions");
            DropTable("dbo.Files");
        }
    }
}

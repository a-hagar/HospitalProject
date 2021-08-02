namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Submissions", "FileId", "dbo.Files");
            DropIndex("dbo.Submissions", new[] { "FileId" });
            AddColumn("dbo.Submissions", "hasFile", c => c.Boolean(nullable: false));
            AddColumn("dbo.Submissions", "FileExtension", c => c.String());
            DropColumn("dbo.Submissions", "FileId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Submissions", "FileId", c => c.Int(nullable: false));
            DropColumn("dbo.Submissions", "FileExtension");
            DropColumn("dbo.Submissions", "hasFile");
            CreateIndex("dbo.Submissions", "FileId");
            AddForeignKey("dbo.Submissions", "FileId", "dbo.Files", "FileId", cascadeDelete: true);
        }
    }
}

namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class undo : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Submissions", "FileName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Submissions", "FileName", c => c.String());
        }
    }
}

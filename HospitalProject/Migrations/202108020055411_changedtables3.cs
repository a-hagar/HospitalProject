namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedtables3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Submissions", "FileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Submissions", "FileName");
        }
    }
}

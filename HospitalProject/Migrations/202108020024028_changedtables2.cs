namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedtables2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Submissions", "City", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Submissions", "City");
        }
    }
}

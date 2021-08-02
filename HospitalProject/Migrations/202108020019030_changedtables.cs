namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedtables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Submissions", "FirstName", c => c.String());
            AddColumn("dbo.Submissions", "LastName", c => c.String());
            AddColumn("dbo.Submissions", "Address", c => c.String());
            AddColumn("dbo.Submissions", "Email", c => c.String());
            AddColumn("dbo.Submissions", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {   
            DropColumn("dbo.Submissions", "PhoneNumber");
            DropColumn("dbo.Submissions", "Email");
            DropColumn("dbo.Submissions", "Address");
            DropColumn("dbo.Submissions", "LastName");
            DropColumn("dbo.Submissions", "FirstName");
        }
    }
}

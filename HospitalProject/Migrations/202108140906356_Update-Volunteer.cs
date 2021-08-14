namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateVolunteer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VolunteerDepts", "VolunteerStatus", c => c.String());
            DropColumn("dbo.Volunteers", "VolunteerStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Volunteers", "VolunteerStatus", c => c.String());
            DropColumn("dbo.VolunteerDepts", "VolunteerStatus");
        }
    }
}

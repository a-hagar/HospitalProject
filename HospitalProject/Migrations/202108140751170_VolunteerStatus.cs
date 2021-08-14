namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VolunteerStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Volunteers", "VolunteerStatus", c => c.String());
            DropColumn("dbo.Volunteers", "VolunteerBadge");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Volunteers", "VolunteerBadge", c => c.String());
            DropColumn("dbo.Volunteers", "VolunteerStatus");
        }
    }
}

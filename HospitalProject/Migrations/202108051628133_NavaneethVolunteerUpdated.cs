namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NavaneethVolunteerUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Volunteers", "VolunteerBadge", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Volunteers", "VolunteerBadge");
        }
    }
}

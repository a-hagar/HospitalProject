using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using HospitalProject.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HospitalProject.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateofBirth { get; set; }

        public BloodGroupList BloodGroup { get; set; }
        public string Address { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        // adding Department to DB
        public DbSet<Department> Departments { get; set; }

        //adding DoctorDetails to Db
        public DbSet<DoctorDetail> DoctorDetails { get; set; }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        
        // Donor and Donations
        public DbSet<Donor> Donors { get; set; }
        public DbSet<Donation> Donations { get; set; }

        
        // adding Volunteers to DB
        public DbSet<Volunteer> Volunteers { get; set; }
        // adding junction table to take care of Volunteer and Dept Mapping
        public DbSet<VolunteerDept> VolunteerDepts { get; set; }



        //Create a table for patient details
        public DbSet<Patient> Patients { get; set; }

        //Create a table for patientlog details
        public DbSet<PatientLog> PatientLogsDto { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public enum BloodGroupList
    {
        [Display(Name = "A+")]
        APositive,
        [Display(Name = "A-")]
        ANegative,
        [Display(Name = "B+")]
        BPositive,
        [Display(Name = "B-")]
        BNegative,
        [Display(Name = "O+")]
        OPositive,
        [Display(Name = "O-")]
        ONegative,
        [Display(Name = "AB+")]
        ABPositive,
        [Display(Name = "AB-")]
        ABNegative
    }
}
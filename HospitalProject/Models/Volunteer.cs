using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalProject.Models
{
    public class Volunteer
    {
        [Key]
        [Column(Order = 0)]
        [Required(ErrorMessage = "VolunteerID is Required")]
        public int VolunteerID { get; set; }

        public string VolunteerBadge { get; set; }

        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public ApplicationUser ApplicationUser { get; set; }

    }
}
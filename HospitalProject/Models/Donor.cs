using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class Donor
    {
        [Key]
        public int DonorId { get; set; }

        [ForeignKey("ApplicationUser")] //navaneeth put actual variable/column in here rather than the table
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }


    }

    public class DonorDto
    {
        public int DonorId { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }

    }
}



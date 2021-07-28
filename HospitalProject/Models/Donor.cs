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

        [ForeignKey("User")]
        public int userid { get; set; }
        public virtual User User { get; set; }


    }
}



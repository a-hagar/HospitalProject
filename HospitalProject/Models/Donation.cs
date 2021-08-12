using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class Donation
    {
        [Key]
        public int DonationId { get; set; }

        [ForeignKey("Donor")]
        public int DonorId { get; set; }
        public virtual Donor Donor { get; set; }

        public DateTime DonationDate { get; set; }

        public int Amount { get; set; }
         
    }

    public class DonationDto
    {
        [Key]
        public int DonationId { get; set; }

        [ForeignKey("Donor")]
        public int DonorId { get; set; }
        public virtual Donor Donor { get; set; }

        public DateTime DonationDate { get; set; }

        public int Amount { get; set; }


    }
}
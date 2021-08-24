using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalProject.Models
{
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }

        //Different period of time associated with same users
        //[ForeignKey("Patientlog")]
        //public int PatientLogID { get; set; }
        //
        //Diiferent users can stay in same period of time


        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
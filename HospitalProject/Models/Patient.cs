using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalProject.Models
{
    public class Patient
    {
        [ForeignKey("PatientLog")]

        public int PatientID { get; set; }

        [ForeignKey("User")]

        public int userid { get; set; }
    }
}
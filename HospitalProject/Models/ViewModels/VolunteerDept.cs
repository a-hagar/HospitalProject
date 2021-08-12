using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class VolunteerDept
    {
        [Key]
        public int VolDepID { get; set; }

        [ForeignKey("Volunteer")]
        public int VolID { get; set; }
        public virtual Volunteer Volunteer { get; set; }

        [ForeignKey("Department")]
        public int DeptID { get; set; }
        public virtual Department Department { get; set; }
    }
}
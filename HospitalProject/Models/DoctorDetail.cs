using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalProject.Models
{
    public class DoctorDetail
    {
        [Key]
        public int DoctorId { get; set; }
        public string DoctorFname { get; set; }
        public string DoctorLname { get; set; }
        public string DoctorDesignation { get; set; }
        public string DoctorEmail { get; set; }

        // A Doctor belongs to one department
        // a Department can have many doctors
        [ForeignKey("Department")]
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }
    }
    public class DoctorDetailDto
    {
        public int DoctorId { get; set; }
        public string DoctorFname { get; set; }
        public string DoctorLname { get; set; }
        public string DoctorDesignation { get; set; }
        public string DoctorEmail { get; set; }
        public int DepartmentID { get; set; }
    }

}
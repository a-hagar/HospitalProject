using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class Job
    {
        [Key]
        public int JobId { get; set; }
        public string JobTitle { get; set; }

        [ForeignKey("Department")]
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }
        
        [AllowHtml]
        public string JobDescription { get; set; }

        public DateTime JobPublishDate { get; set; }
        public DateTime JobDeadline { get; set; }
        public string JobType { get; set; }
        public string JobLocation { get; set; }

    }

    public class JobDto
    {
        public int JobId { get; set; }
        public string JobTitle { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }

        [AllowHtml]
        public string JobDescription { get; set; }

        public DateTime JobPublishDate { get; set; }
        public DateTime JobDeadline { get; set; }
        public string JobType { get; set; }
        public string JobLocation { get; set; }
    }

}
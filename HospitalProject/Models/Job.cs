using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class Job
    {
        [Key]
        public int JobId { get; set; }
        public string JobTitle { get; set; }
        public string JobDepartment { get; set; }
        public string JobDescription{ get; set; }
        public DateTime JobPublishDate { get; set; }
        public DateTime JobDeadline { get; set; }
        public string JobType { get; set; }
        public string JobLocation { get; set; }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class Submissions
    {
        [Key]
        public int SubmissionId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Job")]
        public int JobId { get; set; }
        public virtual Job Job { get; set; }

        public DateTime SubmissionDate { get; set; }

        [ForeignKey("Files")]
        public int FileId { get; set; }
        public virtual Files Files { get; set; }

    }
}
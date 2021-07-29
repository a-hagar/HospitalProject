using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalProject.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackID { get; set; }
        public string FeedbackDetails { get; set; }



        [ForeignKey ("Department")]
        public string Deptname { get; set; }

        [ForeignKey("User")]
        public int userID { get; set; }



    }
}
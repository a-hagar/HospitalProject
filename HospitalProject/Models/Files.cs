using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class Files
    {
        [Key]
        public int FileId { get; set; }
        public int UserId { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }

    }
}
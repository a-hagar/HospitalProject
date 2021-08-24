using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalProject.Models
{
	public class PatientLog
	{
		[Key]

		public int PatientID { get; set; }
		public DateTime PatientDateIn { get; set; }
		public DateTime PatientDateOut { get; set; }


		public class PatientLogsDto
		{
			public int PatientID { get; set; }
			public DateTime PatientDateIn { get; set; }
			public DateTime PatientDateOut { get; set; }

		}
	}
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class DoctorDetailsDepartment
    {
        public DepartmentDto SelectedDepartment { get; set; }

        public IEnumerable<DoctorDetailDto> AssociatedDoctors { get; set; }

    }
}
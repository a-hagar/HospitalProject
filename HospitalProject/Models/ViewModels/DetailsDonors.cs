using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class DetailsDonors
    {
        public DonorDto SelectedDonor { get; set; }
        public IEnumerable<DonationDto> DonorDonations{ get; set; }


    }
}
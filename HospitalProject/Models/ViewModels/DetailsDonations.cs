using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class DetailsDonations
    {
        public DonationDto SelectedDonation { get; set; }

        public DonationDto DonationDonor { get; set; }

        public static DateTime Now { get; }     //I think this is the right place to put this? 

    }
}
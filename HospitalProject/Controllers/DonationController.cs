using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using HospitalProject.Models;
using HospitalProject.Models.ViewModels;
using System.Web.Script.Serialization;

// Currently in controller: List, Details, new, create, edit, update, delete
// need to work on associations between donor and donation


namespace HospitalProject.Controllers
{
    public class DonationController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static DonationController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44361/api/"); //Make sure is the right portnumber
        }
        // GET: Donation/List
        public ActionResult List()
        {
            //get a list of all Donations in the system

            string url = "Donationdata/listDonations";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<DonationDto> donations = response.Content.ReadAsAsync<IEnumerable<DonationDto>>().Result;

            return View(donations);

        }

        // GET: Donation/Details
        public ActionResult Details(int id)
        {
            DetailsDonations ViewModel = new DetailsDonations();
            string url = "donationdata/listdonations" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            DonationDto SelectedDonation = response.Content.ReadAsAsync<DonationDto>().Result;

            ViewModel.SelectedDonation = SelectedDonation;


            // This is where you put the donation details for the selected donor



            return View(ViewModel);

        }

        // GET: Donation/New
        public ActionResult New()
        {

            //GET api/donationdata/listdonations

            string url = "donationdata/listdonations";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<DonationDto> DonationOptions = response.Content.ReadAsAsync<IEnumerable<DonationDto>>().Result;

            return View(DonationOptions);
        }

        // POST: Donation/Create

        [HttpPost]
        public ActionResult Create(Donation donation)
        {
            Debug.WriteLine("the json payload is :");
            //Debug.WriteLine(donation.DonationName);
            //objective: add a new Donation into our system using the API
            //curl -H "Content-Type:application/json" -d @donor.json https://localhost:44324/api/donordata/adddonor
            string url = "donationdata/adddonation";


            string jsonpayload = jss.Serialize(donation);
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }


        }

        // GET: Donation/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateDonation ViewModel = new UpdateDonation();

            //the existing Donation information
            string url = "donationdata/finddonation/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DonationDto SelectedDonation = response.Content.ReadAsAsync<DonationDto>().Result;
            ViewModel.SelectedDonation = SelectedDonation;

            // all donation information to choose from when updating this Donation
            //the existing Donation information 
            url = "Donationdata/listdonations/";
            response = client.GetAsync(url).Result;
            // IEnumerable<DonorDto> DonorOptions = response.Content.ReadAsAsync<IEnumerable<DonorDto>>().Result;

            //ViewModel.DonorOptions = DonorOptions; //would allow to reassign Donors to specific donations

            return View(ViewModel);
        }
        // POST: Donation/Update/5
        [HttpPost]
        public ActionResult Update(int id, Donation donation)
        {

            string url = "Donationdata/updatedonations/" + id;
            string jsonpayload = jss.Serialize(donation);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Donation/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "donationdata/finddonation/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DonationDto selecteddonation = response.Content.ReadAsAsync<DonationDto>().Result;
            return View(selecteddonation);
        }
        // POST: Donation/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "donationdata/deletedonation/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

    }
}
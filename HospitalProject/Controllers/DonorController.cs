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
    public class DonorController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static DonorController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44361/api/"); //Make sure is the right portnumber
        }
        // GET: Donor/List
        public ActionResult List()
        {
            //get a list of all donors in the system

            string url = "donordata/listdonors";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<DonorDto> donors = response.Content.ReadAsAsync<IEnumerable<DonorDto>>().Result;

            return View(donors);

        }

        // GET: Donor/Details
        public ActionResult Details(int id)
        {
            DetailsDonors ViewModel = new DetailsDonors();
            string url = "donordata/listdonors" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            DonorDto SelectedDonor = response.Content.ReadAsAsync<DonorDto>().Result;

            ViewModel.SelectedDonor = SelectedDonor;


            // This is where you put the donation details for the selected donor



            return View(ViewModel);

        }

        // GET: Donor/New
        public ActionResult New()
        {

            //GET api/donordata/listdonors

            string url = "donordata/listdonors";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<DonorDto> DonorOptions = response.Content.ReadAsAsync<IEnumerable<DonorDto>>().Result;

            return View(DonorOptions);
        }

        // POST: Donor/Create

        [HttpPost]
        public ActionResult Create(Donor donor)
        {
            Debug.WriteLine("the json payload is :");
            //Debug.WriteLine(donor.DonorName);
            //objective: add a new donor into our system using the API
            //curl -H "Content-Type:application/json" -d @donor.json https://localhost:44324/api/donordata/adddonor
            string url = "donordata/adddonor";


            string jsonpayload = jss.Serialize(donor);
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

        // GET: Donor/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateDonor ViewModel = new UpdateDonor();

            //the existing donor information
            string url = "donordata/finddonor/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DonorDto SelectedDonor = response.Content.ReadAsAsync<DonorDto>().Result;
            ViewModel.SelectedDonor = SelectedDonor;

            // all donation information to choose from when updating this donor
            //the existing donor information 
            url = "donationdata/listdonations/";
            response = client.GetAsync(url).Result;
           // IEnumerable<DonationDto> DonationOptions = response.Content.ReadAsAsync<IEnumerable<DonationDto>>().Result;

            //ViewModel.DonationOptions = DonationOptions; //would allow to reassign donations to specific users

            return View(ViewModel);
        }
        // POST: Donor/Update/5
        [HttpPost]
        public ActionResult Update(int id, Donor donor)
        {

            string url = "donordata/updatedonors/" + id;
            string jsonpayload = jss.Serialize(donor);
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

        // GET: Donor/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "donordata/finddonor/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DonorDto selecteddonor = response.Content.ReadAsAsync<DonorDto>().Result;
            return View(selecteddonor);
        }
        // POST: Donor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "donordata/deletedonor/" + id;
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
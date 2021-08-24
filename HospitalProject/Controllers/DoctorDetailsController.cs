using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Web.Script.Serialization;
using HospitalProject.Models;
using HospitalProject.Models.ViewModels;
using System.Diagnostics;

namespace HospitalProject.Controllers
{

    public class DoctorDetailsController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static DoctorDetailsController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
                UseCookies = false
            };
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44361/api/");
        }


        private void GetApplicationCookie()
        {
            string token = "";

            client.DefaultRequestHeaders.Remove("Cookie");
            if (!User.Identity.IsAuthenticated) return;

            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies.Get(".AspNet.ApplicationCookie");
            if (cookie != null) token = cookie.Value;

            Debug.WriteLine("Token Submitted is : " + token);
            if (token != "") client.DefaultRequestHeaders.Add("Cookie", ".AspNet.ApplicationCookie=" + token);

            return;
        }


        // GET: List
        public ActionResult List()
        {
            string url = "doctordetailsdata/ListDoctorDetails";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<DoctorDetailDto> doctorDetails = response.Content.ReadAsAsync<IEnumerable<DoctorDetailDto>>().Result;
            return View(doctorDetails);
        }

        // GET: DoctorDetails/Details/5
        public ActionResult Details(int id)
        {
            string url = "doctordetailsdata/finddoctordetail/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DoctorDetailDto doctorDetail = response.Content.ReadAsAsync<DoctorDetailDto>().Result;
            return View(doctorDetail);
        }

        //GET: DoctorDetails/Error

        public ActionResult Error()
        {
            return View();
        }
        // GET: DoctorDetails/New
        [Authorize(Roles = "Admin")]
        public ActionResult New()
        {

            string url = "departmentdata/listdepartments";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<DepartmentDto> departments = response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;
            return View(departments);
        }

        // POST: DoctorDetails/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(DoctorDetail doctorDetails)
        {
            GetApplicationCookie();
            string url = "doctorDetailsData/addDoctor";
            
            string jsonpayload = jss.Serialize(doctorDetails);

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

        // GET: DoctorDetails/Update/5
        [Authorize(Roles = "Admin")]
        public ActionResult Update(int id)
        {
            UpdateDoctorDetails viewModel = new UpdateDoctorDetails();

            string url = "doctordetailsdata/finddoctordetail/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            DoctorDetailDto selectedDoctor = response.Content.ReadAsAsync<DoctorDetailDto>().Result;
            viewModel.selectedDoctor = selectedDoctor;

            url = "departmentdata/listdepartments";
            response = client.GetAsync(url).Result;
            IEnumerable<DepartmentDto> departments = response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;
            viewModel.departmentOptions = departments;


            return View(viewModel);
        }

        // POST: DoctorDetails/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, DoctorDetail doctorDetails)
        {
            GetApplicationCookie();
            string url = "doctorDetailsData/UpdateDoctorDetail/" + id;
            
            doctorDetails.DoctorId = id;
            string jsonpayload = jss.Serialize(doctorDetails);

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

        // GET: DoctorDetails/DeleteConfirmation/5
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmation(int id)
        {
            string url = "DoctorDetailsData/FindDoctorDetail/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            DoctorDetailDto selectedDoctor = response.Content.ReadAsAsync<DoctorDetailDto>().Result;
            return View(selectedDoctor);
        }

        // POST: DoctorDetails/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            GetApplicationCookie();
            string url = "doctorDetailsData/DeleteDoctorDetail/" + id;
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
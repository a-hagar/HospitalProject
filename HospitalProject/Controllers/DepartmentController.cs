using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Web.Script.Serialization;
using HospitalProject.Models;
using System.Diagnostics;
using HospitalProject.Models.ViewModels;


namespace Hospital_Project.Controllers
{
    public class DepartmentController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static DepartmentController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
                UseCookies = false
            };
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44342/api/");
        }

        private void GetApplicationCookie()
        {
            string token = "";
            client.DefaultRequestHeaders.Remove("Cookie");
            if (!User.Identity.IsAuthenticated) return;

            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies.Get(".AspNet.ApplicationCookie");
            if (cookie != null) token = cookie.Value;

            //Debug.WriteLine("Token Submitted is : " + token);
            if (token != "") client.DefaultRequestHeaders.Add("Cookie", ".AspNet.ApplicationCookie=" + token);

            return;
        }


        // GET: Department
        public ActionResult List()
        {
            string url = "departmentdata/listdepartments";
            HttpResponseMessage response = client.GetAsync(url).Result;
            Debug.WriteLine(response);
            IEnumerable<DepartmentDto> departments = response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;
            return View(departments);
        }

        // GET: Department/Details/5
        public ActionResult Details(int id)
        {
            Debug.WriteLine(id);
            DoctorDetailsDepartment ViewModel = new DoctorDetailsDepartment();

            //objective: communicate with our department data api to retrieve one department
            // curl https://localhost:44361/api/applicationdata/findapplication/{id}

            string url = "DepartmentData/FindDepartment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            DepartmentDto SelectedDepartment = response.Content.ReadAsAsync<DepartmentDto>().Result;
            // Debug.WriteLine(SelectedDepartment.DepartmentName);
            ViewModel.SelectedDepartment = SelectedDepartment;

            //Debug.WriteLine(ViewModel);
            url = "DoctorDetailsData/ListDoctorDetailForDepartment/" + id;
            response = client.GetAsync(url).Result;

            IEnumerable<DoctorDetailDto> associatedDoctors = response.Content.ReadAsAsync<IEnumerable<DoctorDetailDto>>().Result;
            ViewModel.AssociatedDoctors = associatedDoctors;
            return View(ViewModel);
        }
        //GET: Department/Error
        public ActionResult Error()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        // GET: Department/New
        public ActionResult New()
        {

            return View();
        }

        // POST: Department/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Department department)
        {
            GetApplicationCookie();
            string url = "departmentdata/AddDepartment";
            // curl -d @department.json -H "Content-type:application/json" http://localhost:44361/api/DepartmentData/AddDepartment
            string jsonpayload = jss.Serialize(department);

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

        // GET: Department/Update/5
        [Authorize(Roles = "Admin")]
        public ActionResult Update(int id)
        {

            // curl https://localhost:44361/api/applicationdata/findapplication/{id}

            string url = "DepartmentData/FindDepartment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            DepartmentDto SelectedDepartment = response.Content.ReadAsAsync<DepartmentDto>().Result;

            return View(SelectedDepartment);


        }

        // POST: Department/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, Department department)
        {
            GetApplicationCookie(); 
            string url = "departmentData/UpdateDepartment/" + id;
            department.DepartmentID = id;
            string jsonpayload = jss.Serialize(department);
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

        // GET: Department/DeleteConfirmation/5
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmation(int id)
        {
            string url = "DepartmentData/FindDepartment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            DepartmentDto SelectedDepartment = response.Content.ReadAsAsync<DepartmentDto>().Result;

            return View(SelectedDepartment);
        }

        // POST: Department/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            GetApplicationCookie();//get token credentials   
            string url = "departmentdata/deleteDepartment/" + id;
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
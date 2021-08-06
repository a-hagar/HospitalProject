using HospitalProject.Models;
using HospitalProject.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HospitalProject.Controllers
{
    public class VolunteersController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        private static readonly HttpClient client;
        JavaScriptSerializer jss = new JavaScriptSerializer();
        
        static VolunteersController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
                UseCookies = false
            };

            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44361/api/");
        }

        // GET: Volunteers
        public ActionResult List()
        {
            string url = "VolunteersData";
            HttpResponseMessage response = client.GetAsync(url).Result;
            Debug.WriteLine("The response code is " + response.StatusCode);
            IEnumerable<Volunteer> volunteers = response.Content.ReadAsAsync<IEnumerable<Volunteer>>().Result;
            return View(volunteers);
        }

        // GET: Volunteers/Details/5
        public ActionResult Details(int id)
        {
            string url = "VolunteersData/FindVolunteer/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is " + response.StatusCode);

            Volunteer volunteer = response.Content.ReadAsAsync<Volunteer>().Result;
            return View(volunteer);
        }

        [Authorize]
        // GET: Volunteers/Create
        public ActionResult Create()
        {

            var userID = User.Identity.GetUserId();
            System.Diagnostics.Debug.WriteLine(userID);
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var applicationManager = new ApplicationUserManager(userStore);
            var user = applicationManager.FindById(userID);
            Volunteer volunteer = new Volunteer
            {
                UserID = userID,
                ApplicationUser = user
            };
            ViewBag.departments = db.Departments.ToList();
            return View(volunteer);
        }

        // POST: Volunteers/Create
        [HttpPost]
        public ActionResult Create(string[] dept)
        {
            try
            {
                // TODO: Add insert logic here
                var userID = User.Identity.GetUserId();
                Volunteer volunteer = new Volunteer
                {
                    UserID = userID
                };
                db.Volunteers.Add(volunteer);
                db.SaveChanges();

                foreach (var dep in dept)
                {
                    VolunteerDept volunteerDept = new VolunteerDept
                    {
                        VolID = volunteer.VolunteerID,
                        DeptID = Convert.ToInt32(dep)
                    };
                    db.VolunteerDepts.Add(volunteerDept);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Volunteers/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Volunteers/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Volunteers/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Volunteers/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

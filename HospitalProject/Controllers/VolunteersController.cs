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
        /// <summary>
        /// List all the volunteers
        /// </summary>
        /// <returns>View with all volunteers</returns>
        public ActionResult List()
        {
            string url = "VolunteersData/ListVolunteers";
            HttpResponseMessage response = client.GetAsync(url).Result;
            Debug.WriteLine("The response code is " + response.StatusCode);
            IEnumerable<Volunteer> volunteers = response.Content.ReadAsAsync<IEnumerable<Volunteer>>().Result;
            return View(volunteers);
        }

        // GET: Volunteers/Details/5
        /// <summary>
        /// Display the details of a volunteer
        /// </summary>
        /// <param name="id">Volunteer ID</param>
        /// <returns>View with volunteer detail</returns>
        public ActionResult Details(int id)
        {
            string url = "VolunteersData/FindVolunteer/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is " + response.StatusCode);

            List<VolunteerDept> volunteerDepts = response.Content.ReadAsAsync<List<VolunteerDept>>().Result;
            return View(volunteerDepts);
        }

        /// <summary>
        /// Add a new volunteer
        /// Shows a page with the information of the logged in user in readonly format, and a checkbox containing all possible departments
        /// the person can volunteer for
        /// </summary>
        /// <returns></returns>
        [Authorize]
        // GET: Volunteers/Create
        public ActionResult Join()
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
        /// <summary>
        /// Create a volunteer - user - department mapping
        /// </summary>
        /// <param name="dept">list of departmentIDs selected by the user</param>
        /// <returns></returns>
        /// Note : Code needs to be moved to datacontroller
        [HttpPost]
        public ActionResult Join(string[] dept)
        {
            try
            {
                // TODO: Add insert logic here
                // get the user id of the logged in user
                // and create a volunteer entry in db
                var userID = User.Identity.GetUserId();
                Volunteer volunteer = new Volunteer
                {
                    UserID = userID,
                };
                db.Volunteers.Add(volunteer);
                db.SaveChanges();

                // for all the depts selected create a volunteer - dept mapping and set default status as Pending approval
                foreach (var dep in dept)
                {
                    VolunteerDept volunteerDept = new VolunteerDept
                    {
                        VolID = volunteer.VolunteerID,
                        DeptID = Convert.ToInt32(dep),
                        VolunteerStatus = "PendingApproval",
                    };
                    db.VolunteerDepts.Add(volunteerDept);
                    db.SaveChanges();
                }
                return RedirectToAction("Details", "Volunteers", new { id = volunteer.VolunteerID });
            }
            catch
            {
                return View();
            }
        }

        // GET: Volunteers/Edit/5
        /// <summary>
        /// For the admin to change the status of the Volunteer request
        /// </summary>
        /// <param name="id">ID of the volunteer</param>
        /// <returns>A view with the volunteer details and the departments to which the volunteer requested with current status</returns>
        public ActionResult Edit(int id)
        {
            string url = "VolunteersData/FindVolunteer/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is " + response.StatusCode);

            List<VolunteerDept> volunteerDepts = response.Content.ReadAsAsync<List<VolunteerDept>>().Result;
            var statusList = (from action in (VolunteerStatus[])Enum.GetValues(typeof(VolunteerStatus)) select action.ToString()).ToList();
            var list = statusList.Select(x => new SelectListItem() { Value = x, Text = x }).ToList();
            ViewBag.statusList = list;
            return View(volunteerDepts);
        }

        // POST: Volunteers/Edit/5
        /// <summary>
        /// Volunteer Satus Update code, admin can select the Status of the Volunteer from the page and click on save to update it
        /// </summary>
        /// <param name="volDepID">VolDepID</param>
        /// <param name="volStatus">Updated Volunteer Status</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(ICollection<string> volDepID, ICollection<string> volStatus)
        {
            //foreach (var fd in volDepID.Zip(volStatus, Tuple.Create))
            //{
            //    VolunteerDept volunteerDept = db.VolunteerDepts.Find(Convert.ToInt32(fd.Item1));
            //    volunteerDept.VolunteerStatus = fd.Item2;
            //    db.SaveChanges();
            //}
            //return RedirectToAction("List");
            try
            {
                // TODO: Add update logic here
                foreach (var fd in volDepID.Zip(volStatus, Tuple.Create))
                {
                    VolunteerDept volunteerDept = db.VolunteerDepts.Find(Convert.ToInt32(fd.Item1));
                    volunteerDept.VolunteerStatus = fd.Item2;
                    db.SaveChanges();
                }
                return RedirectToAction("List");
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
        /// <summary>
        /// Change this logic to datacontroller
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Volunteer vol = db.Volunteers.Find(id);
                if (vol == null)
                {
                    return HttpNotFound();
                }

                db.Volunteers.Remove(vol);
                db.SaveChanges();
                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }
    }
}
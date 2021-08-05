using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using System.Web.Script.Serialization;
using HospitalProject.Models;

namespace HospitalProject.Controllers
{
    public class SubmissionsController : Controller
    {
        private static readonly HttpClient client;
        JavaScriptSerializer jss = new JavaScriptSerializer();


        private ApplicationDbContext db = new ApplicationDbContext();

        static SubmissionsController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
                UseCookies = false
            };

            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44361/api/");
        }

        // GET: Submissions
        public ActionResult List()
        {
            //retrieve list of submissions from submissions api
            //curl https://localhost:44329/api/memberdata/listmembers

            string url = "submissionsdata/listsubmissions/";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is " + response.StatusCode);

            IEnumerable<SubmissionsDto> submissions = response.Content.ReadAsAsync<IEnumerable<SubmissionsDto>>().Result;
            Debug.WriteLine("The number of members is: " + submissions.Count());


            return View(submissions);
        }

        // GET: Submissions/Details/5
        public ActionResult Details(int id)
        {
            string url = "submissionsdata/findsubmission/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is " + response.StatusCode);

            SubmissionsDto selectedsubmission = response.Content.ReadAsAsync<SubmissionsDto>().Result;
            return View(selectedsubmission);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Submissions/Create
        public ActionResult New(int? JobId)
        {
            if (JobId == null)
            {
                return RedirectToAction("List", "Job");
            }

            //var job = db.Jobs.Find(JobId);
            SubmissionsDto newSubmission = new SubmissionsDto { JobId = Convert.ToInt32(JobId) };

            return View(newSubmission);
        }

        // POST: Submissions/Create
        [HttpPost]
        public ActionResult Create(Submissions submissions)
        {
            Debug.WriteLine("The user" + submissions.FirstName + " " + submissions.FirstName + "is applying...");
            string url = "submissionsdata/addsubmissions";

            string jsonpayload = jss.Serialize(submissions);

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

        // GET: Submissions/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Submissions/Edit/5
        [HttpPost]
        public ActionResult Update(int id, Submissions submissions)
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

        // GET: Submissions/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "submissionsdata/findsubmissions/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is " + response.StatusCode);

            SubmissionsDto selectedsubmission = response.Content.ReadAsAsync<SubmissionsDto>().Result;
            return View(selectedsubmission);

        }

        // POST: Submissions/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "submissionsdata/deletesubmissions/" + id;

            HttpContent content = new StringContent("");
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
    }
}


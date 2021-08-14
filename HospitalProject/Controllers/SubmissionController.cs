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
    public class SubmissionController : Controller
    {
        private static readonly HttpClient client;
        JavaScriptSerializer jss = new JavaScriptSerializer();


        private ApplicationDbContext db = new ApplicationDbContext();

        static SubmissionController()
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
            //curl https://localhost:44329/api/submissiondata/listsubmissions

            string url = "submissiondata/listsubmissions/";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is " + response.StatusCode);

            IEnumerable<SubmissionDto> submissions = response.Content.ReadAsAsync<IEnumerable<SubmissionDto>>().Result;
            Debug.WriteLine("The total number of submissions is: " + submissions.Count());


            return View(submissions);
        }

        // GET: Submission/Details/5
        public ActionResult Details(int id)
        {
            string url = "submissiondata/findsubmission/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is " + response.StatusCode);

            SubmissionDto selectedsubmission = response.Content.ReadAsAsync<SubmissionDto>().Result;
            return View(selectedsubmission);
        }

        // GET: Submission/ListSubmissionByJob/1
        public ActionResult ListSubmissionByJob(int id)
        {

            string url = "submissiondata/listsubmissionsbyjob/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is " + response.StatusCode);

            IEnumerable<SubmissionDto> submissions = response.Content.ReadAsAsync<IEnumerable<SubmissionDto>>().Result;
            Debug.WriteLine("The number of applications for this job is " + submissions.Count());


            return View(submissions);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Submissions/Create
        [HttpGet]
        public ActionResult New(int? JobId, string JobTitle)
        {
            if (JobId == null && JobTitle == null)
            {
                return RedirectToAction("List", "Job");
            }

            // var job = db.Jobs.Find(JobId);
            SubmissionDto newSubmission = new SubmissionDto { JobId = Convert.ToInt32(JobId), JobTitle = Convert.ToString(JobTitle) };

            return View(newSubmission);
        }

        // POST: Submissions/Create
        [HttpPost]
        public ActionResult Create(Submission submission)
        {
            Debug.WriteLine("The user" + submission.FirstName + " " + submission.FirstName + "is applying...");
            string url = "submissiondata/addsubmissions";

            string jsonpayload = jss.Serialize(submission);

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
        public ActionResult Update(int id, Submission submissions)
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
            string url = "submissiondata/findsubmission/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is " + response.StatusCode);

            SubmissionDto selectedsubmission = response.Content.ReadAsAsync<SubmissionDto>().Result;
            return View(selectedsubmission);

        }

        // POST: Submissions/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "submissiondata/deletesubmission/" + id;

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


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
    public class JobController : Controller
    {
        private static readonly HttpClient client;
        JavaScriptSerializer jss = new JavaScriptSerializer();

        static JobController()
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


            if (token != "") client.DefaultRequestHeaders.Add("Cookie", ".AspNet.ApplicationCookie=" + token);
            Debug.WriteLine("Token Submitted is : " + token);

            return;
        }


        // GET: Job/List
        public ActionResult List()
        {
            string url = "jobdata/listjobs/";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is " + response.StatusCode);
            IEnumerable<JobDto> jobs = response.Content.ReadAsAsync<IEnumerable<JobDto>>().Result;

            Debug.WriteLine("Number of jobs posted: " + jobs.Count());

            return View(jobs);
        }

        // GET: Job/Details/5
        public ActionResult Details(int id)
        {
            string url = "jobdata/findjob/" +id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is " + response.StatusCode);

            JobDto selectedjob = response.Content.ReadAsAsync<JobDto>().Result;
            return View(selectedjob);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Job/Create
        public ActionResult New()
        {

            return View();
        }

        // POST: Job/Create
        [HttpPost]
        public ActionResult Create(Job job)
        {
            Debug.WriteLine("the new job posting is: " + job.JobTitle + " " + job.JobId);
            string url = "jobdata/addjob";

            string jsonpayload = jss.Serialize(job);

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

        // GET: Job/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Job/Edit/5
        [HttpPost]
        public ActionResult Update(int id, Job job)
        {
            string url = "jobdata/updatejob/" + id;

            string jsonpayload = jss.Serialize(job);
            Debug.WriteLine(jsonpayload);

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

        // GET: Job/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "jobdata/findjob/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is " + response.StatusCode);

            JobDto selectedjob = response.Content.ReadAsAsync<JobDto>().Result;
            return View(selectedjob);
        }

        // POST: Job/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "jobdata/deletejob/" + id;

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

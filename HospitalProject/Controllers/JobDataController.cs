using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HospitalProject.Models;
using System.Diagnostics;

namespace HospitalProject.Controllers
{
    public class JobDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/JobData/ListJobs
        [HttpGet]
        [ResponseType(typeof(JobDto))]
        public IHttpActionResult ListJobs()
        {
            List<Job> Jobs = db.Jobs.ToList();
            List<JobDto> JobDtos = new List<JobDto>();

            Jobs.ForEach(j => JobDtos.Add(new JobDto()
            {
                JobId = j.JobId,
                JobTitle = j.JobTitle,
                JobDescription = j.JobDescription,
                DepartmentID = j.DepartmentID,
                JobPublishDate = j.JobPublishDate,
                JobDeadline = j.JobDeadline,
                JobType = j.JobType,
                JobLocation = j.JobLocation
            }));

            return Ok(JobDtos);

        }

        // GET: api/JobData/FindJob/5
        [ResponseType(typeof(JobDto))]
        [HttpGet]
        public IHttpActionResult FindJob(int id)
        {
            Job job = db.Jobs.Find(id);
            JobDto JobDto = new JobDto()
            {
                JobId = job.JobId,
                JobTitle = job.JobTitle,
                JobDescription = job.JobDescription,
                DepartmentID = job.DepartmentID,
                JobPublishDate = job.JobPublishDate,
                JobDeadline = job.JobDeadline,
                JobType = job.JobType,
                JobLocation = job.JobLocation
            };

            if (job == null)
            {
                return NotFound();
            }

            return Ok(JobDto);
        }

        // PUT: api/JobData/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateJob(int id, Job job)
        {
            Debug.WriteLine("Updating Job...");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != job.JobId)
            {
                return BadRequest();
            }

            db.Entry(job).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Debug.WriteLine("There are no errors");
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/JobData/AddJob
        [ResponseType(typeof(Job))]
        [HttpPost]
        public IHttpActionResult AddJob(Job job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Jobs.Add(job);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = job.JobId }, job);
        }

        // DELETE: api/JobData/DeleteJob/2
        [ResponseType(typeof(Job))]
        [HttpPost]
        public IHttpActionResult DeleteJob(int id)
        {
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return NotFound();
            }

            db.Jobs.Remove(job);
            db.SaveChanges();

            return Ok(job);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobExists(int id)
        {
            return db.Jobs.Count(e => e.JobId == id) > 0;
        }
    }
}
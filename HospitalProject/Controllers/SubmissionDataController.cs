using System;
using System.IO;
using System.Web;
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
    public class SubmissionDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all submissions from database
        /// </summary>
        /// <returns>
        /// List of all submissions
        /// </returns>
        /// <example>
        /// GET: api/SubmissionData/ListSubmissions
        /// </example>
        
        [HttpGet]
        [ResponseType(typeof(SubmissionDto))]
        public IHttpActionResult ListSubmissions()
        {
            List<Submission> Submissions = db.Submissions.ToList();
            List<SubmissionDto> SubmissionDtos = new List<SubmissionDto>();

            Submissions.ForEach(s => SubmissionDtos.Add(new SubmissionDto()
            {

                SubmissionId = s.SubmissionId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Address = s.Address,
                City = s.City,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber,
                JobId = s.JobId,
                JobTitle = s.Job.JobTitle
            }));

            return Ok(SubmissionDtos);

        }

        /// <summary>
        /// Returns all submissions that applied for a specific job posting by its id
        /// </summary>
        /// <param name="id">Job Id to match all the submissions that applied to the specified job posting</param>
        /// <returns>
        /// List of submissions for a selected job posting
        /// </returns>
        /// <example>
        /// GET: api/JobData/ListSubmissionsForJob/1
        /// </example>
        
        [HttpGet]
        [ResponseType(typeof(SubmissionDto))]
        public IHttpActionResult ListSubmissionsByJob(int id)
        {
            List<Submission> Submissions = db.Submissions.Where(s => s.JobId == id).ToList();
            List<SubmissionDto> SubmissionDtos = new List<SubmissionDto>();

            Submissions.ForEach(s => SubmissionDtos.Add(new SubmissionDto()
            {
                SubmissionId = s.SubmissionId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Address = s.Address,
                City = s.City,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber,
                JobId = s.JobId,
                JobTitle = s.Job.JobTitle,
                SubmissionDate = s.SubmissionDate
            }));


            return Ok(SubmissionDtos);
        }

        /// <summary>
        /// Returns selected job submission by id
        /// </summary>
        /// <param name="id">The id of the submission selected to view</param>
        /// <returns>
        /// The info of the selected submission with the matching id
        /// </returns>
        /// <example>
        /// GET api/SubmissionData/FindSubmission/1
        /// </example>

        [HttpGet]
        [ResponseType(typeof(SubmissionDto))]
        public IHttpActionResult FindSubmission(int id)
        {
            Submission submissions = db.Submissions.Find(id);
            SubmissionDto SubmissionDto = new SubmissionDto()
            {
                SubmissionId = submissions.SubmissionId,
                FirstName = submissions.FirstName,
                LastName = submissions.LastName,
                Address = submissions.Address,
                City = submissions.City,
                Email = submissions.Email,
                PhoneNumber = submissions.PhoneNumber,
                JobId = submissions.JobId,
                JobTitle = submissions.Job.JobTitle,
                SubmissionDate = submissions.SubmissionDate,
                hasFile = submissions.hasFile,
                FileExtension = submissions.FileExtension
            };

            if (submissions == null)
            {
                return NotFound();
            }

            return Ok(SubmissionDto);
        }

        /// <summary>
        /// Receives user file and uploads it to the server
        /// </summary>
        /// <param name="id">The id for the job associated to the resume upload</param>
        /// <returns>
        /// Resume is uploaded to the system and associated to the submissions
        /// </returns>
        /// <example>
        /// POST: api/SubmissionData/UploadResume/1
        /// </example>
        [HttpPost]
        public IHttpActionResult UploadResume(int id)
        {
            bool hasFile = false;
            string FileExtension;
            if (Request.Content.IsMimeMultipartContent())
            {
                Debug.WriteLine("Received multipart form data.");

                int numfiles = HttpContext.Current.Request.Files.Count;
                Debug.WriteLine("Files Received: " + numfiles);


                if (numfiles == 1 && HttpContext.Current.Request.Files[0] != null)
                {
                    var Resume = HttpContext.Current.Request.Files[0];

                    if (Resume.ContentLength > 0)
                    {

                        var valtypes = new[] { "pdf", "docx", "doc" };
                        var extension = Path.GetExtension(Resume.FileName).Substring(1);

                        if (valtypes.Contains(extension))
                        {
                            try
                            {

                                string fn = id + "." + extension;
                                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Resumes/"), fn);

                                Resume.SaveAs(path);


                                hasFile = true;
                                FileExtension = extension;


                                Submission selectedsubmission = db.Submissions.Find(id);
                                selectedsubmission.hasFile = hasFile;
                                selectedsubmission.FileExtension = FileExtension;
                                db.Entry(selectedsubmission).State = EntityState.Modified;

                                db.SaveChanges();

                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine("Image was not saved successfully.");
                                Debug.WriteLine("Exception:" + ex);
                                return BadRequest();
                            }
                        }
                    }

                }

                return Ok();
            }
            else
            {
                return BadRequest();

            }

        }

        /// <summary>
        /// Updates the selected job submission
        /// </summary>
        /// <param name="id">The id of the submission selected</param>
        /// <param name="submissions">The data of the </param>
        /// <example>
        /// PUT: api/SubmissionsData/5
        /// </example>
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateSubmissions(int id, Submission submissions)
        {
            Debug.WriteLine("Updating Submission...");
            if (!ModelState.IsValid)
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

            if (id != submissions.SubmissionId)
            {
                return BadRequest();
            }

            db.Entry(submissions).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubmissionsExists(id))
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

        /// <summary>
        /// Adds new submission to the database
        /// </summary>
        /// <param name="submissions"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(Submission))]
        public IHttpActionResult AddSubmissions(Submission submissions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Submissions.Add(submissions);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = submissions.SubmissionId }, submissions);
        }

        /// <summary>
        /// Deletes the selected submission from the database
        /// </summary>
        /// <param name="id">The id of the selected submission</param>
        /// <returns>
        /// The selected database is removed from the database
        /// </returns>
        [HttpPost]
        [ResponseType(typeof(Submission))]
        public IHttpActionResult DeleteSubmissions(int id)
        {
            Submission submissions = db.Submissions.Find(id);
            if (submissions == null)
            {
                return NotFound();
            }

            db.Submissions.Remove(submissions);
            db.SaveChanges();

            return Ok(submissions);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubmissionsExists(int id)
        {
            return db.Submissions.Count(e => e.SubmissionId == id) > 0;
        }
    }
}
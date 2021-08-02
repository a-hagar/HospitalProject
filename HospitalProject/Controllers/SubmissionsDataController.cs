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
    public class SubmissionsDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/SubmissionsData

        [HttpGet]
        [ResponseType(typeof(SubmissionsDto))]
        public IHttpActionResult ListSubmissions()
        {
            List<Submissions> Submissions = db.Submissions.ToList();
            List<SubmissionsDto> SubmissionsDtos = new List<SubmissionsDto>();

            Submissions.ForEach(s => SubmissionsDtos.Add(new SubmissionsDto()
            {

                SubmissionId = s.SubmissionId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Address = s.Address,
                City = s.City,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber,
                JobId = s.JobId,
            }));

            return Ok(SubmissionsDtos);

        }

        // GET: api/SubmissionsData/5

        [HttpGet]
        [ResponseType(typeof(SubmissionsDto))]
        public IHttpActionResult FindSubmission(int id)
        {
            Submissions submissions = db.Submissions.Find(id);
            SubmissionsDto SubmissionsDto = new SubmissionsDto()
            {
                SubmissionId = submissions.SubmissionId,
                FirstName = submissions.FirstName,
                LastName = submissions.LastName,
                Address = submissions.Address,
                City = submissions.City,
                Email = submissions.Email,
                PhoneNumber = submissions.PhoneNumber,
                JobId = submissions.JobId,
            };

            if (submissions == null)
            {
                return NotFound();
            }

            return Ok(SubmissionsDto);
        }


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


                                Submissions selectedsubmission = db.Submissions.Find(id);
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

        // PUT: api/SubmissionsData/5
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateSubmissions(int id, Submissions submissions)
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

        // POST: api/SubmissionsData
        [HttpPost]
        [ResponseType(typeof(Submissions))]
        public IHttpActionResult AddSubmissions(Submissions submissions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Submissions.Add(submissions);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = submissions.SubmissionId }, submissions);
        }

        // DELETE: api/SubmissionsData/5
        [HttpPost]
        [ResponseType(typeof(Submissions))]
        public IHttpActionResult DeleteSubmissions(int id)
        {
            Submissions submissions = db.Submissions.Find(id);
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
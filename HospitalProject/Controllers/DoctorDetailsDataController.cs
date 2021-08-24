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

namespace HospitalProject.Controllers
{
    public class DoctorDetailsDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// List 
        /// </summary>
        /// <returns>The list of doctors</returns>
        // GET: api/DoctorDetailsData/ListDoctorDetails

        [HttpGet]
        [ResponseType(typeof(DoctorDetailDto))]
        public IHttpActionResult ListDoctorDetails()
        {
            List<DoctorDetail> doctorDetails = db.DoctorDetails.ToList();
            List<DoctorDetailDto> doctorDetailDtos = new List<DoctorDetailDto>();
            doctorDetails.ForEach(element => doctorDetailDtos.Add(new DoctorDetailDto()
            {
                DoctorId = element.DoctorId,
                DoctorFname = element.DoctorFname,
                DoctorLname = element.DoctorLname,
                DoctorDesignation = element.DoctorDesignation,
                DoctorEmail = element.DoctorEmail,
                DepartmentID = element.DepartmentID
            }));
            return Ok(doctorDetailDtos);
        }
        /// <summary>
        /// Finds a particular doctor by referencing its ID.
        /// </summary>
        /// <param name="id">DoctorId</param>
        /// <returns>Details for the doctor with the referenced ID</returns>
        // GET: api/doctordetailsdata/finddoctordetail/5
        [ResponseType(typeof(DoctorDetail))]
        [HttpGet]
        public IHttpActionResult FindDoctorDetail(int id)
        {
            DoctorDetail doctorDetails = db.DoctorDetails.Find(id);
            DoctorDetailDto doctorDetailDto = new DoctorDetailDto()
            {
                DoctorId = doctorDetails.DoctorId,
                DoctorFname = doctorDetails.DoctorFname,
                DoctorLname = doctorDetails.DoctorLname,
                DoctorDesignation = doctorDetails.DoctorDesignation,
                DoctorEmail = doctorDetails.DoctorEmail,
                DepartmentID = doctorDetails.DepartmentID
            };
            if (doctorDetails == null)
            {
                return NotFound();
            }

            return Ok(doctorDetailDto);
        }
        /// <summary>
        /// Doctors in a particular department
        /// </summary>
        /// <param name="id">DepartmentID</param>
        /// <returns>List of Doctor's Details</returns>
        /// GET: api/doctordetailsdata/listdoctordetailfordepartment/5

        [HttpGet]
        [ResponseType(typeof(DoctorDetailDto))]
        public IHttpActionResult ListDoctorDetailForDepartment(int id)
        {
            List<DoctorDetail> doctorDetails = db.DoctorDetails.Where(a => a.DepartmentID == id).ToList();
            List<DoctorDetailDto> doctorDetailDtos = new List<DoctorDetailDto>();
            doctorDetails.ForEach(element => doctorDetailDtos.Add(new DoctorDetailDto()
            {
                DoctorId = element.DoctorId,
                DoctorFname = element.DoctorFname,
                DoctorLname = element.DoctorLname,
                DoctorDesignation = element.DoctorDesignation,
                DoctorEmail = element.DoctorEmail,
                DepartmentID = element.DepartmentID,

            }));
            return Ok(doctorDetailDtos);

        }
        /// <summary>
        /// Updates the doctor's details by referencing the Id
        /// </summary>
        /// <param name="id">DoctorId</param>
        /// <param name="doctorDetails"></param>
        // POST: api/doctordetailsdata/updatedoctordetail/1
        // curl -H "Content-Type:application/json" -d @DoctorDetail.json https://localhost:44361/api/doctordetailsdata/updatedoctordetail/1

        [HttpPost]
        [ResponseType(typeof(void))]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult UpdateDoctorDetail(int id, DoctorDetail doctorDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != doctorDetails.DoctorId)
            {
                return BadRequest();
            }

            db.Entry(doctorDetails).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Add a new doctor to the database.
        /// </summary>
        /// <param name="doctorDetails">doctorDetails</param>
        /// <returns>
        // curl -d @DoctorDetail.json -H "Content-type:application/json" https://localhost:44361/api/doctordetailsdata/adddoctor
        // POST: api/doctordetailsdata/adddoctor
        [ResponseType(typeof(DoctorDetail))]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult AddDoctor(DoctorDetail doctorDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DoctorDetails.Add(doctorDetails);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = doctorDetails.DoctorId }, doctorDetails);
        }
        /// <summary>
        /// Delete a spefic doctor referenced by Id
        /// </summary>
        /// <param name="id">DoctorId</param>
        /// <returns>
        //POST: api/doctordetailsdata/deletdoctordetail/3
        // curl -d "" https://localhost:44361/api/doctordetailsdata/deletedoctordetail/1
        [HttpPost]
        [ResponseType(typeof(DoctorDetail))]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult DeleteDoctorDetail(int id)
        {
            DoctorDetail doctorDetails = db.DoctorDetails.Find(id);
            if (doctorDetails == null)
            {
                return NotFound();
            }

            db.DoctorDetails.Remove(doctorDetails);
            db.SaveChanges();

            return Ok(doctorDetails);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DoctorDetailsExists(int id)
        {
            return db.DoctorDetails.Count(e => e.DoctorId == id) > 0;
        }
    }
}
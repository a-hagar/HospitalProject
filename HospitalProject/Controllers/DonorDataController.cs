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
    public class DonorDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all donors in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all donors in the database, including their individual donations.
        /// </returns>
        /// <example>
        /// GET: api/DonorData/ListDonors
        /// </example>
        /// 
        [HttpGet]
        [ResponseType(typeof(DonorDto))]
        public IHttpActionResult ListDonors()
        {
            List<Donor> Donor = db.Donors.ToList();
            List<DonorDto> DonorDtos = new List<DonorDto>();

            Donor.ForEach(d => DonorDtos.Add(new DonorDto()
            {
                DonorId = d.DonorId,
                UserId = d.UserId,
                //add columns from user table -> probably joined at the Model
            }));

            return Ok(DonorDtos);
        }

        // GET: api/DonorData/5
        [ResponseType(typeof(Donor))]
        public IHttpActionResult GetDonor(int id)
        {
            Donor donor = db.Donors.Find(id);
            if (donor == null)
            {
                return NotFound();
            }

            return Ok(donor);
        }

        /// <summary>
        /// Adds an donor to the system
        /// </summary>
        /// <param name="donor">JSON FORM DATA of an donor</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Donor ID, Donor Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/DonorData/AddDonor
        /// FORM DATA: Donor JSON Object
        /// </example>
        [ResponseType(typeof(Donor))]
        [HttpPost]
        public IHttpActionResult AddDonor(Donor donor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Donors.Add(donor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = donor.DonorId }, donor);
        }


        /// <summary>
        /// Deletes a donor from the system by its ID.
        /// </summary>
        /// <param name="id">The primary key of the donor</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/DonorData/DeleteDonor/5
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(Donor))]
        [HttpPost]
        public IHttpActionResult DeleteDonor(int id)
        {
            Donor donor = db.Donors.Find(id);
            if (donor == null)
            {
                return NotFound();
            }

            db.Donors.Remove(donor);
            db.SaveChanges();

            return Ok();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DonorExists(int id)
        {
            return db.Donors.Count(e => e.DonorId == id) > 0;
        }
    }
}
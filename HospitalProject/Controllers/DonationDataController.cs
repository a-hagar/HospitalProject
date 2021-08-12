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
    public class DonationDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all Donations in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Donation in the database, including their individual donations.
        /// </returns>
        /// <example>
        /// GET: api/DonationData/ListDonation
        /// </example>
        /// 
        [HttpGet]
        [ResponseType(typeof(DonationDto))]
        public IHttpActionResult ListDonations()
        {
            List<Donation> Donation = db.Donations.ToList();
            List<DonationDto> DonationDtos = new List<DonationDto>();

            Donation.ForEach(d => DonationDtos.Add(new DonationDto()
            {
                DonationId = d.DonationId,
                DonorId = d.DonorId,
                DonationDate = d.DonationDate,
                Amount = d.Amount

        //add columns from user table -> probably joined at the Model
    }));

            return Ok(DonationDtos);
        }

        // GET: api/DonationData/5
        [ResponseType(typeof(Donation))]
        public IHttpActionResult GetDonation(int id)
        {
            Donation donation = db.Donations.Find(id);
            if (donation == null)
            {
                return NotFound();
            }

            return Ok(donation);
        }

        /// <summary>
        /// Adds an Donation to the system
        /// </summary>
        /// <param name="Donation">JSON FORM DATA of an Donation</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Donation ID, Donation Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/DonationData/AddDonation
        /// FORM DATA: Donation JSON Object
        /// </example>
        [ResponseType(typeof(Donation))]
        [HttpPost]
        public IHttpActionResult AddDonation(Donation donation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Donations.Add(donation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = donation.DonationId }, donation);
        }


        /// <summary>
        /// Deletes a Donation from the system by its ID.
        /// </summary>
        /// <param name="id">The primary key of the Donation</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/DonationData/DeleteDonation/5
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(Donation))]
        [HttpPost]
        public IHttpActionResult DeleteDonation(int id)
        {
            Donation donation = db.Donations.Find(id);
            if (donation == null)
            {
                return NotFound();
            }

            db.Donations.Remove(donation);
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

        private bool DonationExists(int id)
        {
            return db.Donations.Count(e => e.DonationId == id) > 0;
        }


    }
}
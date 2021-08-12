using HospitalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace HospitalProject.Controllers
{
    public class VolunteersDataController : ApiController
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/VolunteersData
        [HttpGet]
        public IHttpActionResult ListVolunteers()
        {
            List<Volunteer> volunteers = db.Volunteers.ToList();
            return Ok(volunteers);
        }

        // GET: api/VolunteersData/FindVolunteer/5
        [HttpGet]
        public IHttpActionResult FindVolunteer(int id)
        {
            Volunteer volunteer = db.Volunteers.Find(id);

            if (volunteer == null)
            {
                return NotFound();
            }

            return Ok(volunteer);
        }



        // POST: api/VolunteersData/AddVolunteer
        [HttpPost]
        public IHttpActionResult AddVolunteer(Volunteer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Volunteers.Add(model);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = model.VolunteerID }, model);
        }

        // DELETE: api/VolunteersData/Delete/5
        [HttpPost]
        public IHttpActionResult DeleteVolunteer(int id)
        {
            Volunteer volunteer = db.Volunteers.Find(id);
            if (volunteer == null)
            {
                return NotFound();
            }

            db.Volunteers.Remove(volunteer);
            db.SaveChanges();

            return Ok(volunteer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
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
using static HospitalProject.Models.PatientLog;

namespace HospitalProject.Controllers
{
    public class PatientLogsDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PatientLogsData/ListPatientLogs
        [HttpGet]
        public IEnumerable<PatientLogsDto> ListPatientLogs()
        {
            List<PatientLog> PatientLogs = db.PatientLogsDto.ToList();
            List<PatientLogsDto> PatientLogsDtos = new List<PatientLogsDto>();

            PatientLogs.ForEach(p => PatientLogsDtos.Add(new PatientLogsDto()
            {

                PatientID = p.PatientID,
                PatientDateIn = p.PatientDateIn,
                PatientDateOut = p.PatientDateOut
            }));

            return PatientLogsDtos;


        }

        // GET: api/PatientLogsData/FindPatientLog/5
        [ResponseType(typeof(PatientLog))]
        [HttpGet]
        public IHttpActionResult FindPatientLog(int id)
        {
            PatientLog PatientLog = db.PatientLogsDto.Find(id);
            PatientLogsDto PatientLogsDto = new PatientLogsDto()
            {
                PatientID = PatientLog.PatientID,
                PatientDateIn = PatientLog.PatientDateIn,
                PatientDateOut = PatientLog.PatientDateOut
            };
            if (PatientLog == null)
            {
                return NotFound();
            }

            return Ok(PatientLogsDto);
        }

        // PUT: api/PatientLogsData/UpdatePatientLog/5
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdatePatientLog(int id, PatientLog patientLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != patientLog.PatientID)
            {
                return BadRequest();
            }

            db.Entry(patientLog).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientLogExists(id))
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

        // POST: api/PatientLogsData/AddPatientLog
        [ResponseType(typeof(PatientLog))]
        [HttpPost]
        public IHttpActionResult AddPatientLog(PatientLog patientLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PatientLogsDto.Add(patientLog);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = patientLog.PatientID }, patientLog);
        }

        // DELETE: api/PatientLogsData/DeletePatientLog/5
        [ResponseType(typeof(PatientLog))]
        [HttpPost]
        public IHttpActionResult DeletePatientLog(int id)
        {
            PatientLog patientLog = db.PatientLogsDto.Find(id);
            if (patientLog == null)
            {
                return NotFound();
            }

            db.PatientLogsDto.Remove(patientLog);
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

        private bool PatientLogExists(int id)
        {
            return db.PatientLogsDto.Count(e => e.PatientID == id) > 0;
        }
    }
}
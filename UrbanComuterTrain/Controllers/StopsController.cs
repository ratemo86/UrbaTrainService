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
using UrbanComuterTrain;
using UrbanComuterTrain.Models;
using UrbanComuterTrain.Repositories;

namespace UrbanComuterTrain.Controllers
{
    public class StopsController : ApiController
    {
        private UTEntities db = new UTEntities();
        private readonly UrbanTrainDb repo;
         public StopsController()
         {
             repo = new UrbanTrainDb(db);
         }

        // GET: api/Stops
        [AllowAnonymous]
        public List<StopModel> GetStops()
        {
            var allStopModels = repo.GetAllStops();
            return allStopModels;
        }

        // GET: api/Stops/5
        [ResponseType(typeof(StopModel))]
        [AllowAnonymous]
        public IHttpActionResult GetStop(int id)
        {
            Stop stop = db.Stops.Find(id);
            if (stop == null)
            {
                return NotFound();
            }
            //StopModel stopModel = repo.
            var stopModel = repo.GetAllStops().Where(x=>x.StopId ==id);
            return Ok(stopModel);
        }

        [Route("api/Stops/{id}/NextTrain")]
        [ResponseType(typeof(Int32))]
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult NextTrain(int id)
        {
            Stop stop = db.Stops.Find(id);
            DateTime now = DateTime.Now;


            var allSchedules = db.StopSchedules;
            if (stop == null)
            {
                return NotFound();
            }
            var netxTimePoint = DateTime.Now.Hour + 1;
            var nextTimePontScheduled = netxTimePoint.ToString() + ":00:00";
            var nextTrainId = db.StopSchedules.Where(x => x.StopId == stop.StopId && x.ScheduledTime == nextTimePontScheduled).First().TrainNO;
            //return Ok(stop);
            return Ok(nextTrainId);
        }

        // PUT: api/Stops/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStop(int id, Stop stop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stop.StopId)
            {
                return BadRequest();
            }

            db.Entry(stop).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StopExists(id))
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

        // POST: api/Stops
        [ResponseType(typeof(Stop))]
        public IHttpActionResult PostStop(Stop stop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Stops.Add(stop);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (StopExists(stop.StopId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = stop.StopId }, stop);
        }

        // DELETE: api/Stops/5
        [ResponseType(typeof(Stop))]
        public IHttpActionResult DeleteStop(int id)
        {
            Stop stop = db.Stops.Find(id);
            if (stop == null)
            {
                return NotFound();
            }

            db.Stops.Remove(stop);
            db.SaveChanges();

            return Ok(stop);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StopExists(int id)
        {
            return db.Stops.Count(e => e.StopId == id) > 0;
        }
    }
}
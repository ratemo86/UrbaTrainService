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
    public class TrainsController : ApiController
    {
        private UTEntities db = new UTEntities();
        private readonly UrbanTrainDb repo;

        public TrainsController()
        {
            repo = new UrbanTrainDb(db);
        
        }

        // GET: api/Trains
        [AllowAnonymous]
       
        public List<TrainModel> GetTrains()
        {
            //foreach (var item in db.Trains)
            //{
            //    item.Links.Add(new Link {

            //        Href = "http://localhost:60013/api/Trains/" + item.TrainNO,
            //        Rel = "Train NO " + item.TrainNO,
            //        Method = "GET"
            //    });
            //}
            //db.SaveChanges();
            return repo.GetAllTrains();
        }

        // GET: api/Trains/5
        [ResponseType(typeof(TrainModel))]
        [AllowAnonymous]
        public IHttpActionResult GetTrain(int id)
        {
            Train train = db.Trains.Find(id);
            if (train == null)
            {
                return NotFound();
            }
            //train.Links.Add(new Link {

            //    Href = "http://localhost:60013/api/Trains/" + id + "/GetCurrLoc",
            //    Rel = "Train NO " + id,
            //    Method = "GET"
            
            //});
            //db.SaveChanges();
            return Ok(repo.GetAllTrains().Where(x=>x.TrainNO==id).First());
        }

        [ResponseType(typeof(string))]
        [AllowAnonymous]
        [Route("api/Trains/{id}/GetCurrLoc")]
        public IHttpActionResult GetCurrLoc(int id)
        {
            Train train = db.Trains.Find(id);
            if (train == null)
            {
                return NotFound();
            }
            else
            {
                //string currentLocation = train.TrainStatusId==1? "Headed from " + train.NextStop  +" from " + train.LastStop: "";

                //return Ok(currentLocation);
                return Ok(repo.GetAllTrains().Where(x => x.TrainNO == id).First().CurrentLocation);
            }

          
        }

        // PUT: api/Trains/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTrain(int id, Train train)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != train.TrainNO)
            {
                return BadRequest();
            }

            db.Entry(train).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainExists(id))
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

        // POST: api/Trains
        [ResponseType(typeof(Train))]
        public IHttpActionResult PostTrain(Train train)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Trains.Add(train);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TrainExists(train.TrainNO))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = train.TrainNO }, train);
        }

        // DELETE: api/Trains/5
        [ResponseType(typeof(Train))]
        public IHttpActionResult DeleteTrain(int id)
        {
            Train train = db.Trains.Find(id);
            if (train == null)
            {
                return NotFound();
            }

            db.Trains.Remove(train);
            db.SaveChanges();

            return Ok(train);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TrainExists(int id)
        {
            return db.Trains.Count(e => e.TrainNO == id) > 0;
        }
    }
}
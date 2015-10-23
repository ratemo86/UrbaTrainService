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
using Newtonsoft.Json;


namespace UrbanComuterTrain.Controllers
{
    using UrbanComuterTrain.Models;
using UrbanComuterTrain.Repositories;
    public class LinesController : ApiController
    {
        private UTEntities db = new UTEntities();
        private readonly UrbanTrainDb repo;

        public LinesController()
        {
            repo = new UrbanTrainDb(db);
        }
        // GET: api/Lines
        [Route("")]
        [AllowAnonymous]
        [HttpGet]
        public List<LineModel> GetLines()
        {
          
            List<LineModel> lineModels = repo.GetAllLines();

            foreach (var line in lineModels)
            {
                line.LineLink= repo.GetLinkforLine(line);
                line.Stops = repo.GetStopsForLine(line.LineId);
                line.Trains = repo.GetTainsForLine(line.LineId);
            }

           return lineModels;
        }

        // GET: api/Lines/5
        [ResponseType(typeof(LineModel))]
        [AllowAnonymous]
       // [Route("/{id}")]
        public IHttpActionResult GetLine(int id)
        {
            Line line = db.Lines.Find(id);
            if (line == null)
            {
                return NotFound();
            }
           
            //return Ok(line);
            LineModel lineModel = repo.GetLine(id);
            lineModel.LineLink = repo.GetLinkforLine(lineModel);
            lineModel.Stops = repo.GetStopsForLine(id);
            lineModel.Trains = repo.GetTainsForLine(id);
            return Ok(lineModel);
        }

        [ResponseType(typeof(string))]
        [AllowAnonymous]
        [Route("api/Lines/{id}/GetLineStatus")]
        public IHttpActionResult GetLineStatus(int id)
        {
            Line line = db.Lines.Find(id);
            if (line == null)
            {
                return NotFound();
            }
            else 
            {
                return Ok(db.LineStatus.Where(x=>x.LineStatusId ==line.LineStatusId).First().CurrentStatus);
            }

           
        }




        //// PUT: api/Lines/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutLine(int id, Line line)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != line.LineId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(line).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!LineExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // POST: api/Lines
       // [ResponseType(typeof(Line))]
        //public IHttpActionResult PostLine(Line line)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Lines.Add(line);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (LineExists(line.LineId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = line.LineId }, line);
        //}

        //// DELETE: api/Lines/5
        //[ResponseType(typeof(Line))]
        //public IHttpActionResult DeleteLine(int id)
        //{
        //    Line line = db.Lines.Find(id);
        //    if (line == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Lines.Remove(line);
        //    db.SaveChanges();

        //    return Ok(line);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LineExists(int id)
        {
            return db.Lines.Count(e => e.LineId == id) > 0;
        }
    }
}
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
using WarehouseDataAccess;

namespace WarehouseServices.Controllers
{
    public class BoMsController : ApiController
    {
        private WarehouseDetailsEntities db = new WarehouseDetailsEntities();

        // GET: api/BoMs
        public IQueryable<BoM> GetBoMs()
        {
            return db.BoMs;
        }

        // GET: api/BoMs/5
        [ResponseType(typeof(BoM))]
        public IHttpActionResult GetBoM(string id)
        {
            BoM boM = db.BoMs.Find(id);
            if (boM == null)
            {
                return NotFound();
            }

            return Ok(boM);
        }

        // PUT: api/BoMs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBoM(string id, BoM boM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != boM.BOMID)
            {
                return BadRequest();
            }

            db.Entry(boM).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoMExists(id))
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

        // POST: api/BoMs
        [ResponseType(typeof(BoM))]
        public IHttpActionResult PostBoM(BoM boM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BoMs.Add(boM);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BoMExists(boM.BOMID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = boM.BOMID }, boM);
        }

        // DELETE: api/BoMs/5
        [ResponseType(typeof(BoM))]
        public IHttpActionResult DeleteBoM(string id)
        {
            BoM boM = db.BoMs.Find(id);
            if (boM == null)
            {
                return NotFound();
            }

            db.BoMs.Remove(boM);
            db.SaveChanges();

            return Ok(boM);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BoMExists(string id)
        {
            return db.BoMs.Count(e => e.BOMID == id) > 0;
        }
    }
}
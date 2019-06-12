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
    public class DelTypesController : ApiController
    {
        private WarehouseDetailsEntities db = new WarehouseDetailsEntities();

        // GET: api/DelTypes
        public IQueryable<DelType> GetDelTypes()
        {
            return db.DelTypes;
        }

        // GET: api/DelTypes/5
        [ResponseType(typeof(DelType))]
        public IHttpActionResult GetDelType(string id)
        {
            DelType delType = db.DelTypes.Find(id);
            if (delType == null)
            {
                return NotFound();
            }

            return Ok(delType);
        }

        // PUT: api/DelTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDelType(string id, DelType delType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != delType.DelTypeID)
            {
                return BadRequest();
            }

            db.Entry(delType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DelTypeExists(id))
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

        // POST: api/DelTypes
        [ResponseType(typeof(DelType))]
        public IHttpActionResult PostDelType(DelType delType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DelTypes.Add(delType);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DelTypeExists(delType.DelTypeID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = delType.DelTypeID }, delType);
        }

        // DELETE: api/DelTypes/5
        [ResponseType(typeof(DelType))]
        public IHttpActionResult DeleteDelType(string id)
        {
            DelType delType = db.DelTypes.Find(id);
            if (delType == null)
            {
                return NotFound();
            }

            db.DelTypes.Remove(delType);
            db.SaveChanges();

            return Ok(delType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DelTypeExists(string id)
        {
            return db.DelTypes.Count(e => e.DelTypeID == id) > 0;
        }
    }
}
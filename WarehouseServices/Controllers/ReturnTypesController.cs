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
    public class ReturnTypesController : ApiController
    {
        private WarehouseDetailsEntities db = new WarehouseDetailsEntities();

        // GET: api/ReturnTypes
        public IQueryable<ReturnType> GetReturnTypes()
        {
            return db.ReturnTypes;
        }

        // GET: api/ReturnTypes/5
        [ResponseType(typeof(ReturnType))]
        public IHttpActionResult GetReturnType(string id)
        {
            ReturnType returnType = db.ReturnTypes.Find(id);
            if (returnType == null)
            {
                return NotFound();
            }

            return Ok(returnType);
        }

        // PUT: api/ReturnTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReturnType(string id, ReturnType returnType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != returnType.ReturnTypeID)
            {
                return BadRequest();
            }

            db.Entry(returnType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReturnTypeExists(id))
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

        // POST: api/ReturnTypes
        [ResponseType(typeof(ReturnType))]
        public IHttpActionResult PostReturnType(ReturnType returnType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ReturnTypes.Add(returnType);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ReturnTypeExists(returnType.ReturnTypeID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = returnType.ReturnTypeID }, returnType);
        }

        // DELETE: api/ReturnTypes/5
        [ResponseType(typeof(ReturnType))]
        public IHttpActionResult DeleteReturnType(string id)
        {
            ReturnType returnType = db.ReturnTypes.Find(id);
            if (returnType == null)
            {
                return NotFound();
            }

            db.ReturnTypes.Remove(returnType);
            db.SaveChanges();

            return Ok(returnType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReturnTypeExists(string id)
        {
            return db.ReturnTypes.Count(e => e.ReturnTypeID == id) > 0;
        }
    }
}
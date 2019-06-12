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
    public class InwardDelItemsController : ApiController
    {
        private WarehouseDetailsEntities db = new WarehouseDetailsEntities();

        // GET: api/InwardDelItems
        public IQueryable<InwardDelItem> GetInwardDelItems()
        {
            return db.InwardDelItems;
        }

        // GET: api/InwardDelItems/5
        [ResponseType(typeof(InwardDelItem))]
        public IHttpActionResult GetInwardDelItem(string id)
        {
            InwardDelItem inwardDelItem = db.InwardDelItems.Find(id);
            if (inwardDelItem == null)
            {
                return NotFound();
            }

            return Ok(inwardDelItem);
        }

        // PUT: api/InwardDelItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInwardDelItem(string id, InwardDelItem inwardDelItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != inwardDelItem.InwardDelItemID)
            {
                return BadRequest();
            }

            db.Entry(inwardDelItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InwardDelItemExists(id))
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

        // POST: api/InwardDelItems
        [ResponseType(typeof(InwardDelItem))]
        public IHttpActionResult PostInwardDelItem(InwardDelItem inwardDelItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.InwardDelItems.Add(inwardDelItem);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (InwardDelItemExists(inwardDelItem.InwardDelItemID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = inwardDelItem.InwardDelItemID }, inwardDelItem);
        }

        // DELETE: api/InwardDelItems/5
        [ResponseType(typeof(InwardDelItem))]
        public IHttpActionResult DeleteInwardDelItem(string id)
        {
            InwardDelItem inwardDelItem = db.InwardDelItems.Find(id);
            if (inwardDelItem == null)
            {
                return NotFound();
            }

            db.InwardDelItems.Remove(inwardDelItem);
            db.SaveChanges();

            return Ok(inwardDelItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InwardDelItemExists(string id)
        {
            return db.InwardDelItems.Count(e => e.InwardDelItemID == id) > 0;
        }
    }
}
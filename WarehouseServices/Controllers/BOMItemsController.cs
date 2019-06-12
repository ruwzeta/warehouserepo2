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
    public class BOMItemsController : ApiController
    {
        private WarehouseDetailsEntities db = new WarehouseDetailsEntities();

        // GET: api/BOMItems
        public IQueryable<BOMItem> GetBOMItems()
        {
            return db.BOMItems;
        }

        // GET: api/BOMItems/5
        [ResponseType(typeof(BOMItem))]
        public IHttpActionResult GetBOMItem(string id)
        {
            BOMItem bOMItem = db.BOMItems.Find(id);
            if (bOMItem == null)
            {
                return NotFound();
            }

            return Ok(bOMItem);
        }

        // PUT: api/BOMItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBOMItem(string id, BOMItem bOMItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bOMItem.BOMItemID)
            {
                return BadRequest();
            }

            db.Entry(bOMItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BOMItemExists(id))
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

        // POST: api/BOMItems
        [ResponseType(typeof(BOMItem))]
        public IHttpActionResult PostBOMItem(BOMItem bOMItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BOMItems.Add(bOMItem);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BOMItemExists(bOMItem.BOMItemID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = bOMItem.BOMItemID }, bOMItem);
        }

        // DELETE: api/BOMItems/5
        [ResponseType(typeof(BOMItem))]
        public IHttpActionResult DeleteBOMItem(string id)
        {
            BOMItem bOMItem = db.BOMItems.Find(id);
            if (bOMItem == null)
            {
                return NotFound();
            }

            db.BOMItems.Remove(bOMItem);
            db.SaveChanges();

            return Ok(bOMItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BOMItemExists(string id)
        {
            return db.BOMItems.Count(e => e.BOMItemID == id) > 0;
        }
    }
}
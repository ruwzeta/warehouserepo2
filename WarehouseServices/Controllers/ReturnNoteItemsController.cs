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
    public class ReturnNoteItemsController : ApiController
    {
        private WarehouseDetailsEntities db = new WarehouseDetailsEntities();

        // GET: api/ReturnNoteItems
        public IQueryable<ReturnNoteItem> GetReturnNoteItems()
        {
            return db.ReturnNoteItems;
        }

        // GET: api/ReturnNoteItems/5
        [ResponseType(typeof(ReturnNoteItem))]
        public IHttpActionResult GetReturnNoteItem(string id)
        {
            ReturnNoteItem returnNoteItem = db.ReturnNoteItems.Find(id);
            if (returnNoteItem == null)
            {
                return NotFound();
            }

            return Ok(returnNoteItem);
        }

        // PUT: api/ReturnNoteItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReturnNoteItem(string id, ReturnNoteItem returnNoteItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != returnNoteItem.ReturnNoteItemID)
            {
                return BadRequest();
            }

            db.Entry(returnNoteItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReturnNoteItemExists(id))
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

        // POST: api/ReturnNoteItems
        [ResponseType(typeof(ReturnNoteItem))]
        public IHttpActionResult PostReturnNoteItem(ReturnNoteItem returnNoteItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ReturnNoteItems.Add(returnNoteItem);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ReturnNoteItemExists(returnNoteItem.ReturnNoteItemID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = returnNoteItem.ReturnNoteItemID }, returnNoteItem);
        }

        // DELETE: api/ReturnNoteItems/5
        [ResponseType(typeof(ReturnNoteItem))]
        public IHttpActionResult DeleteReturnNoteItem(string id)
        {
            ReturnNoteItem returnNoteItem = db.ReturnNoteItems.Find(id);
            if (returnNoteItem == null)
            {
                return NotFound();
            }

            db.ReturnNoteItems.Remove(returnNoteItem);
            db.SaveChanges();

            return Ok(returnNoteItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReturnNoteItemExists(string id)
        {
            return db.ReturnNoteItems.Count(e => e.ReturnNoteItemID == id) > 0;
        }
    }
}
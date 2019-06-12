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
    public class GoodsReceivedNotesController : ApiController
    {
        private WarehouseDetailsEntities db = new WarehouseDetailsEntities();

        // GET: api/GoodsReceivedNotes
        public IQueryable<GoodsReceivedNote> GetGoodsReceivedNotes()
        {
            return db.GoodsReceivedNotes;
        }

        // GET: api/GoodsReceivedNotes/5
        [ResponseType(typeof(GoodsReceivedNote))]
        public IHttpActionResult GetGoodsReceivedNote(string id)
        {
            GoodsReceivedNote goodsReceivedNote = db.GoodsReceivedNotes.Find(id);
            if (goodsReceivedNote == null)
            {
                return NotFound();
            }

            return Ok(goodsReceivedNote);
        }

        // PUT: api/GoodsReceivedNotes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGoodsReceivedNote(string id, GoodsReceivedNote goodsReceivedNote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != goodsReceivedNote.RecNoteID)
            {
                return BadRequest();
            }

            db.Entry(goodsReceivedNote).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoodsReceivedNoteExists(id))
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

        // POST: api/GoodsReceivedNotes
        [ResponseType(typeof(GoodsReceivedNote))]
        public IHttpActionResult PostGoodsReceivedNote(GoodsReceivedNote goodsReceivedNote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GoodsReceivedNotes.Add(goodsReceivedNote);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (GoodsReceivedNoteExists(goodsReceivedNote.RecNoteID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = goodsReceivedNote.RecNoteID }, goodsReceivedNote);
        }

        // DELETE: api/GoodsReceivedNotes/5
        [ResponseType(typeof(GoodsReceivedNote))]
        public IHttpActionResult DeleteGoodsReceivedNote(string id)
        {
            GoodsReceivedNote goodsReceivedNote = db.GoodsReceivedNotes.Find(id);
            if (goodsReceivedNote == null)
            {
                return NotFound();
            }

            db.GoodsReceivedNotes.Remove(goodsReceivedNote);
            db.SaveChanges();

            return Ok(goodsReceivedNote);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GoodsReceivedNoteExists(string id)
        {
            return db.GoodsReceivedNotes.Count(e => e.RecNoteID == id) > 0;
        }
    }
}
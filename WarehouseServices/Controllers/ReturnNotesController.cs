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
    public class ReturnNotesController : ApiController
    {
        private WarehouseDetailsEntities db = new WarehouseDetailsEntities();

        // GET: api/ReturnNotes
        public IQueryable<ReturnNote> GetReturnNotes()
        {
            return db.ReturnNotes;
        }

        // GET: api/ReturnNotes/5
        [ResponseType(typeof(ReturnNote))]
        public IHttpActionResult GetReturnNote(string id)
        {
            ReturnNote returnNote = db.ReturnNotes.Find(id);
            if (returnNote == null)
            {
                return NotFound();
            }

            return Ok(returnNote);
        }

        // PUT: api/ReturnNotes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReturnNote(string id, ReturnNote returnNote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != returnNote.ReturnNoteID)
            {
                return BadRequest();
            }

            db.Entry(returnNote).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReturnNoteExists(id))
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

        // POST: api/ReturnNotes
        [ResponseType(typeof(ReturnNote))]
        public IHttpActionResult PostReturnNote(ReturnNote returnNote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ReturnNotes.Add(returnNote);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ReturnNoteExists(returnNote.ReturnNoteID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = returnNote.ReturnNoteID }, returnNote);
        }

        // DELETE: api/ReturnNotes/5
        [ResponseType(typeof(ReturnNote))]
        public IHttpActionResult DeleteReturnNote(string id)
        {
            ReturnNote returnNote = db.ReturnNotes.Find(id);
            if (returnNote == null)
            {
                return NotFound();
            }

            db.ReturnNotes.Remove(returnNote);
            db.SaveChanges();

            return Ok(returnNote);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReturnNoteExists(string id)
        {
            return db.ReturnNotes.Count(e => e.ReturnNoteID == id) > 0;
        }
    }
}
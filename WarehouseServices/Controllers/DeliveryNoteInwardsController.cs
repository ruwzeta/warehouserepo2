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
    public class DeliveryNoteInwardsController : ApiController
    {
        private WarehouseDetailsEntities db = new WarehouseDetailsEntities();

        // GET: api/DeliveryNoteInwards
        public IQueryable<DeliveryNoteInward> GetDeliveryNoteInwards()
        {
            return db.DeliveryNoteInwards;
        }

        // GET: api/DeliveryNoteInwards/5
        [ResponseType(typeof(DeliveryNoteInward))]
        public IHttpActionResult GetDeliveryNoteInward(string id)
        {
            DeliveryNoteInward deliveryNoteInward = db.DeliveryNoteInwards.Find(id);
            if (deliveryNoteInward == null)
            {
                return NotFound();
            }

            return Ok(deliveryNoteInward);
        }

        // PUT: api/DeliveryNoteInwards/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDeliveryNoteInward(string id, DeliveryNoteInward deliveryNoteInward)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deliveryNoteInward.InwardDelID)
            {
                return BadRequest();
            }

            db.Entry(deliveryNoteInward).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryNoteInwardExists(id))
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

        // POST: api/DeliveryNoteInwards
        [ResponseType(typeof(DeliveryNoteInward))]
        public IHttpActionResult PostDeliveryNoteInward(DeliveryNoteInward deliveryNoteInward)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DeliveryNoteInwards.Add(deliveryNoteInward);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DeliveryNoteInwardExists(deliveryNoteInward.InwardDelID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = deliveryNoteInward.InwardDelID }, deliveryNoteInward);
        }

        // DELETE: api/DeliveryNoteInwards/5
        [ResponseType(typeof(DeliveryNoteInward))]
        public IHttpActionResult DeleteDeliveryNoteInward(string id)
        {
            DeliveryNoteInward deliveryNoteInward = db.DeliveryNoteInwards.Find(id);
            if (deliveryNoteInward == null)
            {
                return NotFound();
            }

            db.DeliveryNoteInwards.Remove(deliveryNoteInward);
            db.SaveChanges();

            return Ok(deliveryNoteInward);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DeliveryNoteInwardExists(string id)
        {
            return db.DeliveryNoteInwards.Count(e => e.InwardDelID == id) > 0;
        }
    }
}
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
    public class DeliveryNoteOutwardsController : ApiController
    {
        private WarehouseDetailsEntities db = new WarehouseDetailsEntities();

        // GET: api/DeliveryNoteOutwards
        public IQueryable<DeliveryNoteOutward> GetDeliveryNoteOutwards()
        {
            return db.DeliveryNoteOutwards;
        }

        // GET: api/DeliveryNoteOutwards/5
        [ResponseType(typeof(DeliveryNoteOutward))]
        public IHttpActionResult GetDeliveryNoteOutward(string id)
        {
            DeliveryNoteOutward deliveryNoteOutward = db.DeliveryNoteOutwards.Find(id);
            if (deliveryNoteOutward == null)
            {
                return NotFound();
            }

            return Ok(deliveryNoteOutward);
        }

        // PUT: api/DeliveryNoteOutwards/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDeliveryNoteOutward(string id, DeliveryNoteOutward deliveryNoteOutward)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deliveryNoteOutward.OutwardDelID)
            {
                return BadRequest();
            }

            db.Entry(deliveryNoteOutward).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryNoteOutwardExists(id))
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

        // POST: api/DeliveryNoteOutwards
        [ResponseType(typeof(DeliveryNoteOutward))]
        public IHttpActionResult PostDeliveryNoteOutward(DeliveryNoteOutward deliveryNoteOutward)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DeliveryNoteOutwards.Add(deliveryNoteOutward);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DeliveryNoteOutwardExists(deliveryNoteOutward.OutwardDelID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = deliveryNoteOutward.OutwardDelID }, deliveryNoteOutward);
        }

        // DELETE: api/DeliveryNoteOutwards/5
        [ResponseType(typeof(DeliveryNoteOutward))]
        public IHttpActionResult DeleteDeliveryNoteOutward(string id)
        {
            DeliveryNoteOutward deliveryNoteOutward = db.DeliveryNoteOutwards.Find(id);
            if (deliveryNoteOutward == null)
            {
                return NotFound();
            }

            db.DeliveryNoteOutwards.Remove(deliveryNoteOutward);
            db.SaveChanges();

            return Ok(deliveryNoteOutward);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DeliveryNoteOutwardExists(string id)
        {
            return db.DeliveryNoteOutwards.Count(e => e.OutwardDelID == id) > 0;
        }
    }
}
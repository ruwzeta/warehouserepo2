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
    public class CourierDeliveriesController : ApiController
    {
        private WarehouseDetailsEntities db = new WarehouseDetailsEntities();

        // GET: api/CourierDeliveries
        public IQueryable<CourierDelivery> GetCourierDeliveries()
        {
            return db.CourierDeliveries;
        }

        // GET: api/CourierDeliveries/5
        [ResponseType(typeof(CourierDelivery))]
        public IHttpActionResult GetCourierDelivery(string id)
        {
            CourierDelivery courierDelivery = db.CourierDeliveries.Find(id);
            if (courierDelivery == null)
            {
                return NotFound();
            }

            return Ok(courierDelivery);
        }

        // PUT: api/CourierDeliveries/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCourierDelivery(string id, CourierDelivery courierDelivery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != courierDelivery.CourierDelID)
            {
                return BadRequest();
            }

            db.Entry(courierDelivery).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourierDeliveryExists(id))
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

        // POST: api/CourierDeliveries
        [ResponseType(typeof(CourierDelivery))]
        public IHttpActionResult PostCourierDelivery(CourierDelivery courierDelivery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CourierDeliveries.Add(courierDelivery);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CourierDeliveryExists(courierDelivery.CourierDelID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = courierDelivery.CourierDelID }, courierDelivery);
        }

        // DELETE: api/CourierDeliveries/5
        [ResponseType(typeof(CourierDelivery))]
        public IHttpActionResult DeleteCourierDelivery(string id)
        {
            CourierDelivery courierDelivery = db.CourierDeliveries.Find(id);
            if (courierDelivery == null)
            {
                return NotFound();
            }

            db.CourierDeliveries.Remove(courierDelivery);
            db.SaveChanges();

            return Ok(courierDelivery);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CourierDeliveryExists(string id)
        {
            return db.CourierDeliveries.Count(e => e.CourierDelID == id) > 0;
        }
    }
}
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
    public class CouriersController : ApiController
    {
        private WarehouseDetailsEntities db = new WarehouseDetailsEntities();

        // GET: api/Couriers
        public IQueryable<Courier> GetCouriers()
        {
            return db.Couriers;
        }

        // GET: api/Couriers/5
        [ResponseType(typeof(Courier))]
        public IHttpActionResult GetCourier(string id)
        {
            Courier courier = db.Couriers.Find(id);
            if (courier == null)
            {
                return NotFound();
            }

            return Ok(courier);
        }

        // PUT: api/Couriers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCourier(string id, Courier courier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != courier.CourierID)
            {
                return BadRequest();
            }

            db.Entry(courier).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourierExists(id))
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

        // POST: api/Couriers
        [ResponseType(typeof(Courier))]
        public IHttpActionResult PostCourier(Courier courier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Couriers.Add(courier);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CourierExists(courier.CourierID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = courier.CourierID }, courier);
        }

        // DELETE: api/Couriers/5
        [ResponseType(typeof(Courier))]
        public IHttpActionResult DeleteCourier(string id)
        {
            Courier courier = db.Couriers.Find(id);
            if (courier == null)
            {
                return NotFound();
            }

            db.Couriers.Remove(courier);
            db.SaveChanges();

            return Ok(courier);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CourierExists(string id)
        {
            return db.Couriers.Count(e => e.CourierID == id) > 0;
        }
    }
}
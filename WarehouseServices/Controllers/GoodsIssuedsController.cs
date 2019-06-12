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
    public class GoodsIssuedsController : ApiController
    {
        private WarehouseDetailsEntities db = new WarehouseDetailsEntities();

        // GET: api/GoodsIssueds
        public IQueryable<GoodsIssued> GetGoodsIssueds()
        {
            return db.GoodsIssueds;
        }

        // GET: api/GoodsIssueds/5
        [ResponseType(typeof(GoodsIssued))]
        public IHttpActionResult GetGoodsIssued(string id)
        {
            GoodsIssued goodsIssued = db.GoodsIssueds.Find(id);
            if (goodsIssued == null)
            {
                return NotFound();
            }

            return Ok(goodsIssued);
        }

        // PUT: api/GoodsIssueds/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGoodsIssued(string id, GoodsIssued goodsIssued)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != goodsIssued.GoodsIssuedID)
            {
                return BadRequest();
            }

            db.Entry(goodsIssued).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoodsIssuedExists(id))
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

        // POST: api/GoodsIssueds
        [ResponseType(typeof(GoodsIssued))]
        public IHttpActionResult PostGoodsIssued(GoodsIssued goodsIssued)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GoodsIssueds.Add(goodsIssued);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (GoodsIssuedExists(goodsIssued.GoodsIssuedID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = goodsIssued.GoodsIssuedID }, goodsIssued);
        }

        // DELETE: api/GoodsIssueds/5
        [ResponseType(typeof(GoodsIssued))]
        public IHttpActionResult DeleteGoodsIssued(string id)
        {
            GoodsIssued goodsIssued = db.GoodsIssueds.Find(id);
            if (goodsIssued == null)
            {
                return NotFound();
            }

            db.GoodsIssueds.Remove(goodsIssued);
            db.SaveChanges();

            return Ok(goodsIssued);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GoodsIssuedExists(string id)
        {
            return db.GoodsIssueds.Count(e => e.GoodsIssuedID == id) > 0;
        }
    }
}
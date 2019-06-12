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
    public class GoodsReceiveItemsController : ApiController
    {
        private WarehouseDetailsEntities db = new WarehouseDetailsEntities();

        // GET: api/GoodsReceiveItems
        public IQueryable<GoodsReceiveItem> GetGoodsReceiveItems()
        {
            return db.GoodsReceiveItems;
        }

        // GET: api/GoodsReceiveItems/5
        [ResponseType(typeof(GoodsReceiveItem))]
        public IHttpActionResult GetGoodsReceiveItem(string id)
        {
            GoodsReceiveItem goodsReceiveItem = db.GoodsReceiveItems.Find(id);
            if (goodsReceiveItem == null)
            {
                return NotFound();
            }

            return Ok(goodsReceiveItem);
        }

        // PUT: api/GoodsReceiveItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGoodsReceiveItem(string id, GoodsReceiveItem goodsReceiveItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != goodsReceiveItem.ReceivedItemID)
            {
                return BadRequest();
            }

            db.Entry(goodsReceiveItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoodsReceiveItemExists(id))
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

        // POST: api/GoodsReceiveItems
        [ResponseType(typeof(GoodsReceiveItem))]
        public IHttpActionResult PostGoodsReceiveItem(GoodsReceiveItem goodsReceiveItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GoodsReceiveItems.Add(goodsReceiveItem);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (GoodsReceiveItemExists(goodsReceiveItem.ReceivedItemID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = goodsReceiveItem.ReceivedItemID }, goodsReceiveItem);
        }

        // DELETE: api/GoodsReceiveItems/5
        [ResponseType(typeof(GoodsReceiveItem))]
        public IHttpActionResult DeleteGoodsReceiveItem(string id)
        {
            GoodsReceiveItem goodsReceiveItem = db.GoodsReceiveItems.Find(id);
            if (goodsReceiveItem == null)
            {
                return NotFound();
            }

            db.GoodsReceiveItems.Remove(goodsReceiveItem);
            db.SaveChanges();

            return Ok(goodsReceiveItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GoodsReceiveItemExists(string id)
        {
            return db.GoodsReceiveItems.Count(e => e.ReceivedItemID == id) > 0;
        }
    }
}
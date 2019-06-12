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
    public class GoodsIssuedItemsController : ApiController
    {
        private WarehouseDetailsEntities db = new WarehouseDetailsEntities();

        // GET: api/GoodsIssuedItems
        public IQueryable<GoodsIssuedItem> GetGoodsIssuedItems()
        {
            return db.GoodsIssuedItems;
        }

        // GET: api/GoodsIssuedItems/5
        [ResponseType(typeof(GoodsIssuedItem))]
        public IHttpActionResult GetGoodsIssuedItem(string id)
        {
            GoodsIssuedItem goodsIssuedItem = db.GoodsIssuedItems.Find(id);
            if (goodsIssuedItem == null)
            {
                return NotFound();
            }

            return Ok(goodsIssuedItem);
        }

        // PUT: api/GoodsIssuedItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGoodsIssuedItem(string id, GoodsIssuedItem goodsIssuedItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != goodsIssuedItem.GoodsIssuedItemID)
            {
                return BadRequest();
            }

            db.Entry(goodsIssuedItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoodsIssuedItemExists(id))
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

        // POST: api/GoodsIssuedItems
        [ResponseType(typeof(GoodsIssuedItem))]
        public IHttpActionResult PostGoodsIssuedItem(GoodsIssuedItem goodsIssuedItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GoodsIssuedItems.Add(goodsIssuedItem);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (GoodsIssuedItemExists(goodsIssuedItem.GoodsIssuedItemID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = goodsIssuedItem.GoodsIssuedItemID }, goodsIssuedItem);
        }

        // DELETE: api/GoodsIssuedItems/5
        [ResponseType(typeof(GoodsIssuedItem))]
        public IHttpActionResult DeleteGoodsIssuedItem(string id)
        {
            GoodsIssuedItem goodsIssuedItem = db.GoodsIssuedItems.Find(id);
            if (goodsIssuedItem == null)
            {
                return NotFound();
            }

            db.GoodsIssuedItems.Remove(goodsIssuedItem);
            db.SaveChanges();

            return Ok(goodsIssuedItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GoodsIssuedItemExists(string id)
        {
            return db.GoodsIssuedItems.Count(e => e.GoodsIssuedItemID == id) > 0;
        }
    }
}
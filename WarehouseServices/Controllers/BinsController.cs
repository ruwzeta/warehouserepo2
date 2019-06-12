using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WarehouseDataAccess;

namespace WarehouseServices.Controllers
{
    public class BinsController : ApiController
    {
        public IEnumerable<Bin> Get()
        {
            using (WarehouseDetailsEntities entities = new WarehouseDetailsEntities())
            {
                return entities.Bins.ToList();
            }

        }
        public Bin Get(int binid)
        {
            using (WarehouseDetailsEntities entities = new WarehouseDetailsEntities())
            {

                return entities.Bins.First(e => e.BinID.Equals(binid));
            }
        }

    }
}

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
    public class EmployeeTypesController : ApiController
    {
        private WarehouseDetailsEntities db = new WarehouseDetailsEntities();

        // GET: api/EmployeeTypes
        public IQueryable<EmployeeType> GetEmployeeTypes()
        {
            return db.EmployeeTypes;
        }

        // GET: api/EmployeeTypes/5
        [ResponseType(typeof(EmployeeType))]
        public IHttpActionResult GetEmployeeType(string id)
        {
            EmployeeType employeeType = db.EmployeeTypes.Find(id);
            if (employeeType == null)
            {
                return NotFound();
            }

            return Ok(employeeType);
        }

        // PUT: api/EmployeeTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployeeType(string id, EmployeeType employeeType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employeeType.empTypeID)
            {
                return BadRequest();
            }

            db.Entry(employeeType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeTypeExists(id))
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

        // POST: api/EmployeeTypes
        [ResponseType(typeof(EmployeeType))]
        public IHttpActionResult PostEmployeeType(EmployeeType employeeType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmployeeTypes.Add(employeeType);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EmployeeTypeExists(employeeType.empTypeID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = employeeType.empTypeID }, employeeType);
        }

        // DELETE: api/EmployeeTypes/5
        [ResponseType(typeof(EmployeeType))]
        public IHttpActionResult DeleteEmployeeType(string id)
        {
            EmployeeType employeeType = db.EmployeeTypes.Find(id);
            if (employeeType == null)
            {
                return NotFound();
            }

            db.EmployeeTypes.Remove(employeeType);
            db.SaveChanges();

            return Ok(employeeType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeTypeExists(string id)
        {
            return db.EmployeeTypes.Count(e => e.empTypeID == id) > 0;
        }
    }
}
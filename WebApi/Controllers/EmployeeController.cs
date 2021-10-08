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
using UserManagement.Data.Context;
using UserManagement.Data.Models;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        private UserManagementContext db = new UserManagementContext();

        // GET api/User
        public IQueryable<User> GetEmployees()
        {
            return db.Users;
        }

        // GET api/User/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetEmployee(Guid id)
        {
            User User = db.Users.Find(id);
            if (User == null)
            {
                return NotFound();
            }

            return Ok(User);
        }

        // PUT api/User/5
        public IHttpActionResult PutEmployee(Guid id, User User)
        {

            if (id != User.Id)
            {
                return BadRequest();
            }

            db.Entry(User).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST api/User
        [ResponseType(typeof(User))]
        public IHttpActionResult PostEmployee(User User)
        {
            if(User.Id == Guid.Empty)
            {
                User.Id = Guid.NewGuid();
            }
            db.Users.Add(User);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = User.Id }, User);
        }

        // DELETE api/User/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteEmployee(Guid id)
        {
            User User = db.Users.Find(id);
            if (User == null)
            {
                return NotFound();
            }

            db.Users.Remove(User);
            db.SaveChanges();

            return Ok(User);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(Guid id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserManagement.Data.Context;
using UserManagement.Data.Models;

namespace UserManagement.API.Controllers
{
    public class UserController : ApiController
    {
        //Create instance of Linq-To-Sql class as db  
        UserManagementContext db = new UserManagementContext();

        [Route("")]
        [HttpGet]
        //This action method return all members records.  
        // GET api/<controller>  
        public IEnumerable<User> GetAllEmployees()
        {
            //returning all records of table tblMember.  
            return db.Users.ToList().AsEnumerable();
        }



        //This action method will fetch and filter for specific member id record  
        // GET api/<controller>/5  
        public HttpResponseMessage GetById(Guid id)
        {
            //fetching and filter specific member id record   
            var memberdetail = (from a in db.Users where a.Id == id select a).FirstOrDefault();


            //checking fetched or not with the help of NULL or NOT.  
            if (memberdetail != null)
            {
                //sending response as status code OK with memberdetail entity.  
                return Request.CreateResponse(HttpStatusCode.OK, memberdetail);
            }
            else
            {
                //sending response as error status code NOT FOUND with meaningful message.  
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Code or Member Not Found");
            }
        }


        //To add a new member record  
        // POST api/<controller>  
        public HttpResponseMessage Post([FromBody] User _member)
        {
            try
            {
                //To add an new member record  
                db.Users.AddOrUpdate(_member);

                //Save the submitted record  
                db.SaveChanges();

                //return response status as successfully created with member entity  
                var msg = Request.CreateResponse(HttpStatusCode.Created, _member);

                //Response message with requesturi for check purpose  
                msg.Headers.Location = new Uri(Request.RequestUri + _member.Id.ToString());

                return msg;
            }
            catch (Exception ex)
            {

                //return response as bad request  with exception message.  
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }



        //To update member record  
        // PUT api/<controller>/5  
        public HttpResponseMessage Put(Guid id, [FromBody] User _member)
        {
            //fetching and filter specific member id record   
            var memberdetail = (from a in db.Users where a.Id == id select a).FirstOrDefault();

            //checking fetched or not with the help of NULL or NOT.  
            if (memberdetail != null)
            {
                memberdetail = _member;
                ////set received _member object properties with memberdetail  
                //memberdetail.MemberName = _member.MemberName;
                //memberdetail.PhoneNumber = _member.PhoneNumber;
                //save set allocation.  
                db.SaveChanges();

                //return response status as successfully updated with member entity  
                return Request.CreateResponse(HttpStatusCode.OK, memberdetail);
            }
            else
            {
                //return response error as NOT FOUND  with message.  
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Code or Member Not Found");
            }


        }

        // DELETE api/<controller>/5  
        public HttpResponseMessage Delete(Guid id)
        {

            try
            {
                //fetching and filter specific member id record   
                var _DeleteMember = (from a in db.Users where a.Id == id select a).FirstOrDefault();

                //checking fetched or not with the help of NULL or NOT.  
                if (_DeleteMember != null)
                {

                    db.Users.Remove(_DeleteMember);
                    db.SaveChanges();

                    //return response status as successfully deleted with member id  
                    return Request.CreateResponse(HttpStatusCode.OK, id);
                }
                else
                {
                    //return response error as Not Found  with exception message.  
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Member Not Found or Invalid " + id.ToString());
                }
            }

            catch (Exception ex)
            {

                //return response error as bad request  with exception message.  
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}
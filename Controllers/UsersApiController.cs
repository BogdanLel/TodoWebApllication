using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TodoWebAplication.BussinesLogic;
using TodoWebAplication.Models;
using TodoWebAplication.Models.ViewModels;
using TodoWebApllication.Models;

namespace TodoWebAplication.Controllers
{
    [RoutePrefix("api/UsersApi")]
    public class UsersApiController : ApiController
    {

        private Entities _db;

        public UsersApiController()
        {
            _db = new Entities();
        }

        [Route("GetUsers")]
        [HttpGet]
        public IHttpActionResult GetUsers()
        {
            try
            {
                UsersLogic userLogic = new UsersLogic(_db);
                var result = userLogic.GetUsers();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong!");
            }
        }

        [Route("DeleteUser")]
        [HttpDelete]
        public IHttpActionResult DeleteUser(string userId)
        {
            try
            {
                UsersLogic userLogic = new UsersLogic(_db);
                userLogic.DeleteUser(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Update cannot be executed!");
            }
        }
    }
}
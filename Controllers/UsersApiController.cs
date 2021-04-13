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
                UsersLogic priorityLogic = new UsersLogic(_db);
                var result = priorityLogic.GetUsers();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong!");
            }
        }
    }
}
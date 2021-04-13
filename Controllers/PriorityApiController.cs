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
    [RoutePrefix("api/PriorityApi")]
    public class PriorityApiController : ApiController
    {
        private Entities _db;

        public PriorityApiController()
        {
            _db = new Entities();
        }

        [Route("GetPrioritys")]
        [HttpGet]
        public IHttpActionResult GetPrioritys()
        {
            try
            {
                PriorityLogic priorityLogic = new PriorityLogic(_db);
                var result = priorityLogic.GetPrioritys();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong!");
            }
        }
    }
}
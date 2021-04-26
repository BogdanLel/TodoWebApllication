using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TodoWebApllication.Controllers
{
    public class PriorityController : Controller
    {
        // GET: Priority
        [Authorize(Roles = "Admin,User")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
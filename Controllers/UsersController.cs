using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TodoWebApllication.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace TodoWebApllication.Controllers
{
    public class UsersController : Controller
    {
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using TodoWebApllication.Models.ViewModels;

namespace TodoWebApllication.Controllers
{
    public class RoleApiController : ApiController
    {
        private ApplicationRoleManager _roleManager;



        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }


        [HttpGet]
        public IHttpActionResult GetRoles()
        {
            if (ModelState.IsValid)
            {
                var roles = RoleManager.Roles.Select(x => new RolesViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();
                return Ok(roles);
            }
            return null;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TodoWebAplication.Models.Services;

namespace TodoWebApllication.Models.Services
{
    public class RolesService : BaseService<AspNetRole>
    {
        public RolesService(DbContext context) : base(context)
        {

        }
    }
}
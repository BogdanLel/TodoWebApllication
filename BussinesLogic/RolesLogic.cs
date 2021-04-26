using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TodoWebApllication.Models.Services;

namespace TodoWebApllication.BussinesLogic
{
    public class RolesLogic
    {
        private DbContext _db;
        private RolesService _rolesService;

        public RolesLogic(DbContext db)
        {
            _db = db;
            _rolesService = new RolesService(_db);
        }
    }
}
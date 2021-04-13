using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TodoWebAplication.Models.Services;
using TodoWebApllication.Models;

namespace TodoWebAplication.Models.Services
{
    public class UsersService : BaseService<AspNetUser>
    {
        public UsersService(DbContext context) : base(context)
        {

        }
    }
}
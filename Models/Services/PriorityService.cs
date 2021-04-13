using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TodoWebAplication.Models.Services;
using TodoWebApllication.Models;

namespace TodoWebAplication.Models.Services
{
    public class PriorityService : BaseService<Priority>
    {
        public PriorityService(DbContext context) : base(context)
        {

        }
    }
}
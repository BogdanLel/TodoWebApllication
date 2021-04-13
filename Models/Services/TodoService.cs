using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TodoWebAplication.Models.Services;
using TodoWebApllication.Models;

namespace TodoWebAplication.Models.Services
{
    public class TodoService : BaseService<Todo>
    {
        public TodoService(DbContext context) : base(context)
        {

        }
    }
}
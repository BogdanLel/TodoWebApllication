using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TodoWebAplication.Models;
using TodoWebAplication.Models.Services;
using TodoWebAplication.Models.ViewModels;

namespace TodoWebAplication.BussinesLogic
{
    public class PriorityLogic
    {
        private DbContext _db;
        private PriorityService _priorityService;

        public PriorityLogic(DbContext db)
        {
            _db = db;
            _priorityService = new PriorityService(_db);
        }

        public List<PriorityViewModel> GetPrioritys()
        {
            return _priorityService.GetAll().Select(x => new PriorityViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                PriorityLevel = x.PriorityLevel
            }).ToList();
        }
    }
}
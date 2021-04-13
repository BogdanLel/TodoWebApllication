using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoWebAplication.Models.ViewModels
{
    public class PriorityViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PriorityLevel { get; set; }
    }
}
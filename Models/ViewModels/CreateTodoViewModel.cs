using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoWebAplication.Models.ViewModels
{
    public class CreateTodoViewModel
    {
        public string Name { get; set; }
        public bool Status { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public int PriorityId { get; set; }
        public string UserId { get; set; }
    }
}
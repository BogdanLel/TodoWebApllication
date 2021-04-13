using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoWebAplication.Models.ViewModels
{
    public class TodoViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public int PriorityId { get; set; }
        public string PriorityName { get; set; }
        public int PriorityLevel { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoWebAplication.Models.ViewModels
{
    public class UsersViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public int CountTask { get; set; }
        public bool IsDeleted { get; set; }
        public List<UserTodosViewModel> Todos { get; set; }
    }
    public class UserTodosViewModel
    {
        public string TaskName { get; set; }
        public bool Status { get; set; }
        public string Priority { get; set; }
        public int PriorityLevel { get; set; }
    }
}
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TodoWebAplication.Models;
using TodoWebAplication.Models.Services;
using TodoWebAplication.Models.ViewModels;
using TodoWebApllication;
using TodoWebApllication.Models;

namespace TodoWebAplication.BussinesLogic
{
    public class UsersLogic
    {
        private DbContext _db;
        private UsersService _usersService;

        public UsersLogic(DbContext db)
        {
            _db = db;
            _usersService = new UsersService(_db);
        }

        public List<UsersViewModel> GetUsers()
        {
            return _usersService.GetAll().Where(x => x.IsDeleted != true).Select(x => new UsersViewModel()
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                CountTask = x.Todos.Count,
                Todos = x.Todos.Select(y => new UserTodosViewModel()
                {
                    TaskName = y.Name,
                    Priority = y.Priority.Name,
                    Status = y.Status,
                    PriorityLevel = y.Priority.PriorityLevel
                }).ToList(),
                UserRole = x.AspNetRoles.FirstOrDefault()?.Name,
            }).ToList();
        }

        public void DeleteUser(string userId)
        {
            var oldUser = _usersService.Get(userId);
            var userTabel = new AspNetUser()
            {
                Id = oldUser.Id,
                UserName = oldUser.Id,
                Email = oldUser.Id,
                IsDeleted = true
            };
            _usersService.Update(userTabel, userId);
        }
    }
}
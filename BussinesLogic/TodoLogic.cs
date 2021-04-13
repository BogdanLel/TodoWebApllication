using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TodoWebAplication.Models;
using TodoWebAplication.Models.Services;
using TodoWebAplication.Models.ViewModels;
using TodoWebApllication.Models;

namespace TodoWebAplication.BussinesLogic
{
    public class TodoLogic
    {
        private DbContext _db;
        private TodoService _todoService;

        public TodoLogic(DbContext db)
        {
            _db = db;
            _todoService = new TodoService(_db);
        }

        public PagingListViewModel<TodoViewModel> GetTodos(Filter filter)
        {
            PagingListViewModel<TodoViewModel> pagingTodos = new PagingListViewModel<TodoViewModel>();

            var todos = _todoService.GetAllQuerable();

            if (!String.IsNullOrEmpty(filter.FilterText))
            {
                todos = todos.Where(x => x.Name.Contains(filter.FilterText));
            }

            switch (filter.OrderBy)
            {
                case "sortAscendingByName":
                    todos = todos.OrderBy(x => x.Name);
                    break;
                case "sortDescendingByName":
                    todos = todos.OrderByDescending(x => x.Name);
                    break;
                case "sortAscendingByDate":
                    todos = todos.OrderBy(x => x.CreatedDateTime);
                    break;
                case "sortDescendingByDate":
                    todos = todos.OrderByDescending(x => x.CreatedDateTime);
                    break;
                case "sortByHighestPriority":
                    todos = todos.OrderBy(x => x.PriorityId);
                    break;
                case "sortByLowestPriority":
                    todos = todos.OrderByDescending(x => x.PriorityId);
                    break;
                default:
                    todos = todos.OrderBy(x => x.Name);
                    break;
            }

            pagingTodos.Count = todos.Count();
            int skipPages = filter.PageSize * (filter.CurrentPage - 1);
            pagingTodos.Items = todos.Skip(skipPages).Take(filter.PageSize).Select(x => new TodoViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                PriorityId = x.PriorityId,
                Status = x.Status,
                CreatedDateTime = x.CreatedDateTime,
                PriorityName = x.Priority.Name,
                PriorityLevel = x.Priority.PriorityLevel,
                UserId = x.UserId,
                UserName = x.AspNetUser.UserName
            }).ToList();

            // verificare
            // through new exeption

            pagingTodos.NumberOfPages = pagingTodos.Count / filter.PageSize;

            if (pagingTodos.Count % filter.PageSize > 0)
            {
                pagingTodos.NumberOfPages += 1;
            }

            return pagingTodos;
        }

        public TodoViewModel GetTodoById(int Id)
        {
            var currentTodo = _todoService.Get(Id);
            TodoViewModel todoTable = new TodoViewModel
            {
                Id = currentTodo.Id,
                Name = currentTodo.Name,
                Status = currentTodo.Status,
                PriorityId = currentTodo.PriorityId,
                UserId = currentTodo.UserId
            };
            return todoTable;
        }

        public void AddTodo(CreateTodoViewModel todo)
        {
            var todoTable = new Todo()
            {
                Name = todo.Name,
                Status = todo.Status,
                CreatedDateTime = todo.CreatedDateTime,
                PriorityId = todo.PriorityId,
                UserId = todo.UserId
            };
            _todoService.Add(todoTable);
        }

        public void UpdateTodo(TodoViewModel todo)
        {
            var oldTodo = _todoService.Get(todo.Id);
            var todoTable = new Todo()
            {
                Id = oldTodo.Id,
                Name = todo.Name,
                Status = todo.Status,
                PriorityId = todo.PriorityId,
                UserId = todo.UserId
            };
            _todoService.Update(todoTable, todoTable.Id);
        }

        public void DeleteTodo(int todoId)
        {
            var todo = _todoService.Get(todoId);
            _todoService.Delete(todo);
        }
        
        public PagingListViewModel<TodoViewModel> GetDoneTodos(int CurrentPage, int PageSize)
        {
            PagingListViewModel<TodoViewModel> pagingTodos = new PagingListViewModel<TodoViewModel>();

            var todos = _todoService.GetAllQuerable().Where(x => x.Status == true);

            todos = todos.OrderBy(x => x.Name);
            todos = todos.OrderBy(x => x.CreatedDateTime);
            pagingTodos.Count = todos.Count();
            int skipPages = PageSize * (CurrentPage - 1);
            pagingTodos.Items = todos.Skip(skipPages).Take(PageSize).Select(x => new TodoViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                PriorityId = x.PriorityId,
                Status = x.Status,
                CreatedDateTime = x.CreatedDateTime,
                UserId = x.UserId
            }).ToList();

            pagingTodos.NumberOfPages = pagingTodos.Count / PageSize;

            if (pagingTodos.Count % PageSize > 0)
            {
                pagingTodos.NumberOfPages += 1;
            }

            return pagingTodos;
        }
        public PagingListViewModel<TodoViewModel> GetToBeDoneTodos(int CurrentPage, int PageSize)
        {
            PagingListViewModel<TodoViewModel> pagingTodos = new PagingListViewModel<TodoViewModel>();

            var todos = _todoService.GetAllQuerable().Where(x => x.Status == false);

            todos = todos.OrderBy(x => x.Name);

            pagingTodos.Count = todos.Count();
            int skipPages = PageSize * (CurrentPage - 1);
            pagingTodos.Items = todos.Skip(skipPages).Take(PageSize).Select(x => new TodoViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                PriorityId = x.PriorityId,
                Status = x.Status,
                UserId = x.UserId
            }).ToList();

            pagingTodos.NumberOfPages = pagingTodos.Count / PageSize;

            if (pagingTodos.Count % PageSize > 0)
            {
                pagingTodos.NumberOfPages += 1;
            }

            return pagingTodos;
        }


        public List<TodoViewModel> GetTodosForUser(string userId)
        {
            var todos = _todoService.GetAllQuerable();

            todos = todos.Where(x => x.UserId == userId);
            
            var userTodos = todos.Select(x => new TodoViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                PriorityId = x.PriorityId,
                Status = x.Status,
                CreatedDateTime = x.CreatedDateTime,
                PriorityName = x.Priority.Name,
                PriorityLevel = x.Priority.PriorityLevel,
                UserId = x.UserId,
            }).ToList();
            return userTodos;
        }
    }
}
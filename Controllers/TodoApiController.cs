using System;
using System.Web.Http;
using TodoWebAplication.BussinesLogic;
using TodoWebAplication.Models;
using TodoWebAplication.Models.ViewModels;
using TodoWebApllication.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace TodoWebAplication.Controllers
{
    [RoutePrefix("api/TodoApi")]
    public class TodoApiController : ApiController
    {

        private Entities _db;

        public TodoApiController()
        {
            _db = new Entities();
        }

        [Route("GetMyTodos")]
        [HttpPost]
        public IHttpActionResult GetMyTodos(Filter filter)
        {
            try
            {
                TodoLogic todoLogic = new TodoLogic(_db);
                var currentUser = User.Identity.GetUserId();
                var result = todoLogic.GetTodos(filter,currentUser);
                if (result.Items.Count == 0)
                {
                    // exception
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong!");
            }
        }

        [Route("GetTodos")]
        [HttpPost]
        public IHttpActionResult GetTodos(Filter filter)
        {
            try
            {
                TodoLogic todoLogic = new TodoLogic(_db);
                var result = todoLogic.GetTodos(filter);
                if (result.Items.Count == 0)
                {
                    // exception
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong!");
            }
        }

        [Route("GetTodoById")]
        [HttpPost]
        public IHttpActionResult GetTodoById([FromBody] int Id)
        {
            try
            {
                TodoLogic todoLogic = new TodoLogic(_db);
                return Ok(todoLogic.GetTodoById(Id));
            }
            catch (Exception ex)
            {
                return BadRequest("Id cannot be found!");
            }
        }

        [Route("AddTodo")]
        [HttpPost]
        public IHttpActionResult AddTodo(CreateTodoViewModel todo)
        {
            try
            {
                TodoLogic todoLogic = new TodoLogic(_db);
                todoLogic.AddTodo(todo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Todo cannot be added!");
            }
        }
        [Route("AddMyTodo")]
        [HttpPost]
        public IHttpActionResult AddMyTodo(CreateTodoViewModel todo)
        {
            try
            {
                TodoLogic todoLogic = new TodoLogic(_db);
                var currentUser = User.Identity.GetUserId();
                todoLogic.AddTodo(todo, currentUser);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Todo cannot be added!");
            }
        }

        [Route("UpdateTodo")]
        [HttpPost]
        public IHttpActionResult UpdateTodo(TodoViewModel todo)
        {
            try
            {
                TodoLogic todoLogic = new TodoLogic(_db);
                todoLogic.UpdateTodo(todo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Update cannot be executed!");
            }
        }

        [Route("DeleteTodo")]
        [HttpPost]
        public IHttpActionResult DeleteTodo([FromBody] int todoId)
        {
            try
            {
                TodoLogic todoLogic = new TodoLogic(_db);
                todoLogic.DeleteTodo(todoId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Update cannot be executed!");
            }
        }

        [Route("GetDoneTodos")]
        [HttpGet]
        public IHttpActionResult GetDoneTodos(int CurrentPage, int PageSize)
        {

            try
            {
                TodoLogic todoLogic = new TodoLogic(_db);
                var result = todoLogic.GetDoneTodos(CurrentPage, PageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Update cannot be executed!");
            }
        }

        [Route("GetMyDoneTodos")]
        [HttpGet]
        public IHttpActionResult GetMyDoneTodos(int CurrentPage, int PageSize)
        {

            try
            {
                TodoLogic todoLogic = new TodoLogic(_db);
                var currentUser = User.Identity.GetUserId();
                var result = todoLogic.GetDoneTodos(CurrentPage, PageSize, currentUser);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Update cannot be executed!");
            }
        }

        [Route("GetToBeDoneTodos")]
        [HttpGet]
        public IHttpActionResult GetToBeDoneTodos(int CurrentPage, int PageSize)
        {
            try
            {
                TodoLogic todoLogic = new TodoLogic(_db);
                var result = todoLogic.GetToBeDoneTodos(CurrentPage, PageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Update cannot be executed!");
            }
        }

        [Route("GetMyToBeDoneTodos")]
        [HttpGet]
        public IHttpActionResult GetMyToBeDoneTodos(int CurrentPage, int PageSize)
        {
            try
            {
                TodoLogic todoLogic = new TodoLogic(_db);
                var currentUser = User.Identity.GetUserId();
                var result = todoLogic.GetToBeDoneTodos(CurrentPage, PageSize, currentUser);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Update cannot be executed!");
            }
        }

        [Route("GetTodosForUser")]
        [HttpGet]
        public IHttpActionResult GetTodosForUser(string userId)
        {
            try
            {
                TodoLogic todoLogic = new TodoLogic(_db);
                var result = todoLogic.GetTodosForUser(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong!");
            }
        }

    }
}
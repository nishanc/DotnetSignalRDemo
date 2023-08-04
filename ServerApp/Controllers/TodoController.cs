﻿using Microsoft.AspNetCore.Mvc;
using ServerApp.Data.DTOs;
using ServerApp.Data.Models;
using ServerApp.Repositories;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _repo;

        public TodoController(ITodoRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Todo>>> GetTodos()
        {
            var todos = await _repo.GetTodos();
            return todos;
        }

        [HttpPost]
        public async Task<ActionResult> PostTodo(TodoDto todo)
        {
            var newTodo = new Todo
            {
                Name = todo.Name
            };
            await _repo.AddTodo(newTodo);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            await _repo.DeleteTodo(id);

            return NoContent();
        }
    }
}

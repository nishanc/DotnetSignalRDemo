using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ServerApp.Data.DTOs;
using ServerApp.Data.Models;
using ServerApp.Repositories;
using ServerApp.SignalR;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _repo;
        private IHubContext<NotificationHub, INotificationHub> _notificationHub;

        public TodoController(ITodoRepository repo, IHubContext<NotificationHub, INotificationHub> notificationHub)
        {
            _repo = repo;
            _notificationHub = notificationHub;
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
            await _notificationHub.Clients.All.SendNotificationAsync("Added Item Notification from Server");
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

using ServerApp.Data.Models;

namespace ServerApp.Repositories
{
    public interface ITodoRepository
    {
        Task<List<Todo>> GetTodos();
        Task<Todo> AddTodo(Todo todo);
        Task DeleteTodo(int id);
    }
}

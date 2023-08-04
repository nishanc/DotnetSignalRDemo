using Microsoft.EntityFrameworkCore;
using ServerApp.Data;
using ServerApp.Data.Models;

namespace ServerApp.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly DatabaseContext _context;
        public TodoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Todo>> GetTodos()
        {
            return await _context.Todos.ToListAsync();
        }

        public async Task<Todo> AddTodo(Todo todo)
        {
            await _context.Todos.AddAsync(todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task DeleteTodo(int id)
        {
            var todo = await _context.Todos.FirstOrDefaultAsync(x => x.Id == id);
            if (todo != null)
            {
                _context.Remove(todo);
                await _context.SaveChangesAsync();
            }
        }
    }
}

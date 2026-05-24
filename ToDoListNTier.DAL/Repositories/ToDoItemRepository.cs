using Microsoft.EntityFrameworkCore;
using ToDoListNTier.DAL.Interfaces;
using ToDoListNTier.DataAccess;
using ToDoListNTier.Models;
using ToDoListNTier.Models.Entities;

namespace ToDoListNTier.DAL.Repositories
{
    public class TodoItemRepository : BaseRepository<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TodoItem>> GetPendingByTodoListIdAsync(Guid todoListId)
        {
            return await _context.TodoItems
                .Where(i => i.TodoListId == todoListId && !i.IsCompleted)
                .ToListAsync();
        }
    }
}
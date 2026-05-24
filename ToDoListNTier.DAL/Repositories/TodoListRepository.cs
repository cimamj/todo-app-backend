using Microsoft.EntityFrameworkCore;
using ToDoListNTier.DAL.Interfaces;
using ToDoListNTier.DataAccess;
using ToDoListNTier.Models.Entities;

namespace ToDoListNTier.DAL.Repositories
{
    public class TodoListRepository : BaseRepository<TodoList>, ITodoListRepository
    {
        public TodoListRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<TodoList?> GetByIdWithItemsAsync(Guid id)
        {
            return await _context.TodoLists
                .Include(t => t.Items)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
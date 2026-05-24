using ToDoListNTier.Models.Entities;

namespace ToDoListNTier.DAL.Interfaces
{
    public interface ITodoItemRepository : IBaseRepository<TodoItem>
    {
        Task<IEnumerable<TodoItem>> GetPendingByTodoListIdAsync(Guid todoListId);
    }
}
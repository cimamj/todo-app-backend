using ToDoListNTier.Models.Entities;

namespace ToDoListNTier.DAL.Interfaces
{
    public interface ITodoListRepository : IBaseRepository<TodoList>
    {
        // Specifična metoda za TodoList
        Task<TodoList?> GetByIdWithItemsAsync(Guid id);
    }
}
using ToDoListNTier.Models.Entities;
using ToDoListNTier.Models.DTOs;
using ToDoListNTier.BLL.Results;

namespace ToDoListNTier.BLL.Interfaces
{
    public interface ITodoListService
    {
        Task<Result<IEnumerable<TodoListGetDto>>> GetAllAsync();
        Task<Result<TodoListGetDto>> GetByIdAsync(Guid id);
        Task<Result<TodoListIdDto>> CreateAsync(TodoListCreateDto dto);
        Task<Result> UpdateAsync(Guid id, TodoListUpdateDto dto);
        Task<Result> DeleteAsync(Guid id);
    }
}

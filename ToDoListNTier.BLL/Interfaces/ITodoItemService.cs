using ToDoListNTier.Models.Entities;
using ToDoListNTier.Models.DTOs;
using ToDoListNTier.BLL.Results;

namespace ToDoListNTier.BLL.Interfaces
{
    public interface ITodoItemService
    {
        Task<Result<IEnumerable<TodoItemGetDto>>> GetAllAsync();
        Task<Result<TodoItemGetDto>> GetByIdAsync(Guid id);
        Task<Result<TodoItemIdDto>> CreateAsync(TodoItemCreateDto dto);
        Task<Result> UpdateAsync(Guid id, TodoItemUpdateDto dto);
        Task<Result> DeleteAsync(Guid id);
    }
}

using ToDoListNTier.BLL.Interfaces;
using ToDoListNTier.BLL.Results;
using ToDoListNTier.BLL.Validators;
using ToDoListNTier.DAL.Interfaces;
using ToDoListNTier.Models.Entities;
using ToDoListNTier.Models.DTOs;

namespace ToDoListNTier.BLL.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _repo;
        private readonly ITodoListRepository _listRepo;

        public TodoItemService(ITodoItemRepository repo, ITodoListRepository listRepo )
        {
            _repo = repo;
            _listRepo = listRepo;
        }

        public async Task<Result<TodoItemIdDto>> CreateAsync(TodoItemCreateDto dto)
        {
            var validator = new TodoItemCreateDtoValidator();
            var validation = validator.Validate(dto);
            if (!validation.IsValid)
            {
                var errors = string.Join("; ", validation.Errors.Select(e => e.ErrorMessage));
                return Result<TodoItemIdDto>.Failure(errors);
            }

            var parent = await _listRepo.GetByIdAsync(dto.TodoListId);
            if (parent == null)
                return Result<TodoItemIdDto>.Failure("Todo list not found");

            var entity = new TodoItem
            {
                TodoListId = dto.TodoListId,
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                IsCompleted = false
            };

            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();

            return Result<TodoItemIdDto>.Ok(new TodoItemIdDto { Id = entity.Id });
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) 
                return Result.Failure("Not found", 404);

            _repo.Delete(entity);

            await _repo.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result<IEnumerable<TodoItemGetDto>>> GetAllAsync()
        {
            var items = await _repo.GetAllAsync();
            return Result<IEnumerable<TodoItemGetDto>>.Ok(items.Select(MapToGetDto).ToList());
        }

        public async Task<Result<TodoItemGetDto>> GetByIdAsync(Guid id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null)
                return Result<TodoItemGetDto>.Failure("Not found", 404);

            return Result<TodoItemGetDto>.Ok(MapToGetDto(item));
        }

        public async Task<Result> UpdateAsync(Guid id, TodoItemUpdateDto dto)
        {
            var validator = new TodoItemUpdateDtoValidator();
            var validation = validator.Validate(dto);
            if (!validation.IsValid)
            {
                var errors = string.Join("; ", validation.Errors.Select(e => e.ErrorMessage));
                return Result.Failure(errors);
            }

            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) 
                return Result.Failure("Not found", 404);

            existing.Title = dto.Title;
            existing.Description = dto.Description;
            existing.IsCompleted = dto.IsCompleted;
            existing.DueDate = dto.DueDate;

            _repo.Update(existing);
            await _repo.SaveChangesAsync();

            return Result.Success();
        }

        private TodoItemGetDto MapToGetDto(TodoItem entity)
        {
            return new TodoItemGetDto
            {
                Id = entity.Id,
                TodoListId = entity.TodoListId,
                Title = entity.Title,
                IsCompleted = entity.IsCompleted,
                DueDate = entity.DueDate
            };
        }
    }
}

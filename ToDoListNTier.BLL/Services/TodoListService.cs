using ToDoListNTier.BLL.Interfaces;
using ToDoListNTier.BLL.Results;
using ToDoListNTier.BLL.Validators;
using ToDoListNTier.DAL.Interfaces;
using ToDoListNTier.Models.Entities;
using ToDoListNTier.Models.DTOs;

namespace ToDoListNTier.BLL.Services
{
    public class TodoListService : ITodoListService
    {
        private readonly ITodoListRepository _repo;

        public TodoListService(ITodoListRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<TodoListIdDto>> CreateAsync(TodoListCreateDto dto)
        {
            var validator = new TodoListCreateDtoValidator();
            var validation = validator.Validate(dto);
            if (!validation.IsValid)
            {
                var errors = string.Join("; ", validation.Errors.Select(e => e.ErrorMessage));
                return Result<TodoListIdDto>.Failure(errors);
            }

            var entity = new TodoList 
            { 
                Title = dto.Title
            };

            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();

            return Result<TodoListIdDto>.Ok(new TodoListIdDto { Id = entity.Id });
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) 
                return Result.Failure("Not found", 404);

            _repo.Delete(existing);
            await _repo.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<Result<IEnumerable<TodoListGetDto>>> GetAllAsync()
        {
            var lists = await _repo.GetAllAsync();
            return Result<IEnumerable<TodoListGetDto>>.Ok(lists.Select(MapToGetDto).ToList());
        }

        public async Task<Result<TodoListGetDto>> GetByIdAsync(Guid id)
        {
            var entity = await _repo.GetByIdWithItemsAsync(id);

            if (entity == null) 
                return Result<TodoListGetDto>.Failure("Not found", 404);

            return Result<TodoListGetDto>.Ok(MapToGetDto(entity));
        }

        public async Task<Result> UpdateAsync(Guid id, TodoListUpdateDto dto)
        {
            var validator = new TodoListUpdateDtoValidator();
            var validation = validator.Validate(dto);
            if (!validation.IsValid)
            {
                var errors = string.Join("; ", validation.Errors.Select(e => e.ErrorMessage));
                return Result.Failure(errors);
            }

            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) 
                return Result.Failure("Not found", 404);

            entity.Title = dto.Title;

            _repo.Update(entity);
            await _repo.SaveChangesAsync();

            return Result.Success();
        }

        private TodoListGetDto MapToGetDto(TodoList entity)
        {
            return new TodoListGetDto
            {
                Id = entity.Id,
                Title = entity.Title,
                CreatedAt = entity.CreatedAt,
                Items = entity.Items.Select(i => new TodoItemGetDto
                {
                    Id = i.Id,
                    TodoListId = i.TodoListId,
                    Title = i.Title,
                    IsCompleted = i.IsCompleted,
                    DueDate = i.DueDate
                }).ToList()
            };
        }
    }
}

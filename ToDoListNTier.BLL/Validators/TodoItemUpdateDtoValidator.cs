using FluentValidation;
using ToDoListNTier.Models.DTOs;
using ToDoListNTier.Models.Entities;

namespace ToDoListNTier.BLL.Validators
{
    public class TodoItemUpdateDtoValidator : AbstractValidator<TodoItemUpdateDto>
    {
        public TodoItemUpdateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(500).WithMessage($"Title must be at most {TodoItem.MaxTitleLength} characters");

            RuleFor(x => x.Description)
           .MaximumLength(TodoItem.MaxDescriptionLength).WithMessage($"Description must be at most {TodoItem.MaxDescriptionLength} characters");

            RuleFor(x => x.DueDate)
                .GreaterThanOrEqualTo(DateTime.UtcNow).When(x => x.DueDate.HasValue).WithMessage("Due date cannot be in the past");
        }
    }
}

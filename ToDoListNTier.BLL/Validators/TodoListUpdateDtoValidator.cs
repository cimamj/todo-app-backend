using FluentValidation;
using ToDoListNTier.Models.DTOs;

namespace ToDoListNTier.BLL.Validators
{
    public class TodoListUpdateDtoValidator : AbstractValidator<TodoListUpdateDto>
    {
        public TodoListUpdateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(200).WithMessage("Title must be at most 200 characters");
        }
    }
}

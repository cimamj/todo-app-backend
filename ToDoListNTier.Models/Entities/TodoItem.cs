

namespace ToDoListNTier.Models.Entities
{
    public class TodoItem
    {
        public const int MaxTitleLength = 300;
        public const int MaxDescriptionLength = 1000;

        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid TodoListId { get; set; }

        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public DateTime? DueDate { get; set; }
        public TodoList TodoList { get; set; } = null!;
    }
}

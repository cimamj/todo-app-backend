namespace ToDoListNTier.Models.DTOs
{
    public class TodoListGetDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<TodoItemGetDto> Items { get; set; } = new();
    }
}

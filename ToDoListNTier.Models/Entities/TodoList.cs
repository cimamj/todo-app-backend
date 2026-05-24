

namespace ToDoListNTier.Models.Entities
{
    public class TodoList
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; //ima problema s bazom, onlydate koristia
        public ICollection<TodoItem> Items { get; set; } = new List<TodoItem>();
    }
}

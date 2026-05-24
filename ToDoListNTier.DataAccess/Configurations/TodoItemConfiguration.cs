using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoListNTier.Models.Entities;

namespace ToDoListNTier.DataAccess.Configurations
{
    public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                   .IsRequired()
                   .HasMaxLength(TodoItem.MaxTitleLength);

            builder.Property(t => t.Description)
                   .HasMaxLength(TodoItem.MaxDescriptionLength); 

            builder.Property(t => t.TodoListId)
                   .IsRequired();
        }
    }
}
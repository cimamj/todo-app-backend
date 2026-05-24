using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoListNTier.Models.Entities;

namespace ToDoListNTier.DataAccess.Configurations
{
    public class TodoListConfiguration : IEntityTypeConfiguration<TodoList>
    {
        public void Configure(EntityTypeBuilder<TodoList> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.HasMany(t => t.Items)
                   .WithOne(i => i.TodoList)
                   .HasForeignKey(i => i.TodoListId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
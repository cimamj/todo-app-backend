using Microsoft.EntityFrameworkCore;
using ToDoListNTier.DataAccess;
using ToDoListNTier.DAL.Interfaces;
using ToDoListNTier.DAL.Repositories;
using ToDoListNTier.BLL.Interfaces;
using ToDoListNTier.BLL.Services;
using FluentValidation;
using ToDoListNTier.BLL.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DbContext with PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories and services
builder.Services.AddScoped<ITodoListRepository, TodoListRepository>();
builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();

builder.Services.AddScoped<ITodoListService, TodoListService>();
builder.Services.AddScoped<ITodoItemService, TodoItemService>();

// register FluentValidation validators from BLL
builder.Services.AddValidatorsFromAssemblyContaining<TodoListCreateDtoValidator>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Global exception handling middleware
app.UseMiddleware<ToDoListNTier.API.Middleware.ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();

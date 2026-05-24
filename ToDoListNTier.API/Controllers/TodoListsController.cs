using Microsoft.AspNetCore.Mvc;
using ToDoListNTier.BLL.Interfaces;
using ToDoListNTier.Models.DTOs;
using ToDoListNTier.API.Extensions;

namespace ToDoListNTier.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoListsController : ControllerBase
    {
        private readonly ITodoListService _service;

        public TodoListsController(ITodoListService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return result.ToActionResult(this);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return result.ToActionResult(this);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TodoListCreateDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return result.ToActionResult(this, created => CreatedAtAction(nameof(Get), new { id = created.Id }, created));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TodoListUpdateDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return result.ToActionResult(this);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            return result.ToActionResult(this);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ToDoListNTier.Models.DTOs;
using ToDoListNTier.BLL.Results;
using ToDoListNTier.API.Extensions;

namespace ToDoListNTier.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemsController : ControllerBase
    {
        private readonly ToDoListNTier.BLL.Interfaces.ITodoItemService _service;
        private readonly ToDoListNTier.BLL.Interfaces.ITodoListService _listService;

        public TodoItemsController(ToDoListNTier.BLL.Interfaces.ITodoItemService service, ToDoListNTier.BLL.Interfaces.ITodoListService listService)
        {
            _service = service;
            _listService = listService;
        }

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
        public async Task<IActionResult> Create([FromBody] TodoItemCreateDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return result.ToActionResult(this, created => CreatedAtAction(nameof(Get), new { id = created.Id }, created));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TodoItemUpdateDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return result.ToActionResult(this);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existing = await _service.GetByIdAsync(id);
            if (!existing.IsSuccess) return existing.ToActionResult(this);
            var result = await _service.DeleteAsync(id);
            return result.ToActionResult(this);
        }
    }
}

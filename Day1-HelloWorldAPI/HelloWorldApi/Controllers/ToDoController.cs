using HelloWorldApi.Models;
using HelloWorldApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoService? _toDoService;

        public ToDoController(ToDoService? toDoService) => _toDoService = toDoService;

        [HttpPost]
        public IActionResult CreateToDo(ToDo toDo, ToDoService? _toDoService)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Errors = ModelState.Values.SelectMany(v => v.Errors)});
            }
            _toDoService?.Create(toDo);
            return CreatedAtAction(nameof(RetrieveTodos), toDo);
        }

        [HttpGet]
        public async Task<ActionResult<List<ToDo>>> RetrieveTodos()
        {
            var todos = await _toDoService.RetrieveAll();
            return Ok(todos);
        }

        [HttpGet("id")]
        public async Task<IActionResult> RetrieveTodosById(int id)
        {
            var todos = await _toDoService.RetrieveById(id);
            if(todos == null)
            {
                return NotFound(new { Message = "Todo Not Found" });
            }
            return Ok(todos);
        }


        [HttpPut("id")]
        public async Task<IActionResult> UpdateTodo(int id, ToDo toDo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Errors = ModelState.Values.SelectMany(v => v.Errors) });
            }
            var update = await _toDoService?.Update(id, toDo);
            if (!update)
            {
                return NotFound(new { Message = "Item not found" });
            }
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Errors = ModelState.Values.SelectMany(v => v.Errors) });
            }
           var update = await _toDoService?.Delete(id);
            if (!update)
            {
                return NotFound(new { Message = "Item Not Found" });
            }
            return NoContent();
        }

    }
}

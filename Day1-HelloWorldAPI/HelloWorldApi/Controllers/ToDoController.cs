using HelloWorldApi.Models;
using HelloWorldApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HelloWorldApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoService? _toDoService;

        public ToDoController(ToDoService? toDoService) => _toDoService = toDoService;

        [HttpPost]
        public IActionResult CreateToDo(ToDo toDo, ToDoService? _toDoService)
        {
            Log.Information("Creating ToDo");
            if (!ModelState.IsValid)
            {
                Log.Warning("Todo Failed", ModelState);
                return BadRequest(new { Errors = ModelState.Values.SelectMany(v => v.Errors)});
            }
            _toDoService?.Create(toDo);
            return CreatedAtAction(nameof(RetrieveTodos), toDo);
        }

        [HttpGet]
        public async Task<ActionResult<List<ToDo>>> RetrieveTodos()
        {
            Log.Information("Fetching All ToDos");
            var todos = await _toDoService.RetrieveAll();
            return Ok(todos);
        }

        [HttpGet("id")]
        public async Task<IActionResult> RetrieveTodosById(int id)
        {
            Log.Information("Fetching ToDo by {@id}", id);
            var todos = await _toDoService.RetrieveById(id);
            if(todos == null)
            {
                Log.Warning("ToDo not available.");
                return NotFound(new { Message = "Todo Not Found" });
            }
            Log.Information("Fetched {@id} successfully",id);
            return Ok(todos);
        }


        [HttpPut("id")]
        public async Task<IActionResult> UpdateTodo(int id, ToDo toDo)
        {
            if (!ModelState.IsValid)
            {
                Log.Warning("Update failed due to ", ModelState);
                return BadRequest(new { Errors = ModelState.Values.SelectMany(v => v.Errors) });
            }
            var update = await _toDoService?.Update(id, toDo);
            if (!update)
            {
                Log.Warning("Sorry, failed updating ToDo");
                return NotFound(new { Message = "Todo not found" });
            }
            Log.Information("ToDo Updated successfully ", update);
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                Log.Warning("Update failed due to ", ModelState);
                return BadRequest(new { Errors = ModelState.Values.SelectMany(v => v.Errors) });
            }
           var update = await _toDoService?.Delete(id);
            if (!update)
            {
                Log.Warning("Sorry, failed deleting ToDo");
                return NotFound(new { Message = "Todo Not Found" });
            }
            Log.Information("ToDo deleted successfully", update);
            return NoContent();
        }

    }
}

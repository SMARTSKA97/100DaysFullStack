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
            _toDoService?.Create(toDo);
            return CreatedAtAction(nameof(RetrieveTodos), toDo);
        }

        [HttpGet]
        public async Task<ActionResult<List<ToDo>>> RetrieveTodos()
        {
            var todos = await _toDoService.RetrieveAll();
            return Ok(todos);
        }


        [HttpPut("id")]
        public IActionResult UpdateTodo(int id, ToDo toDo)
        {
            _toDoService?.Update(id, toDo);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            _toDoService?.Delete(id);
            return NoContent();
        }

    }
}

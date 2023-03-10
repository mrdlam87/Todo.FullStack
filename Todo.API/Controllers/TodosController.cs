using Microsoft.AspNetCore.Mvc;
using TodoApp.ServiceInterfaces;
using TodoApp.ViewModels;

namespace TodoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private ILogger<TodosController> _logger;
        public TodosController(ITodoService todoService, ILogger<TodosController> logger)
        {
            _todoService = todoService;
            _logger = logger;
        }

        //  /todos
        [HttpGet]
        public async Task<IEnumerable<TodoDTO>> GetAll() => await _todoService.GetAllTodo();

        //  /todos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoDTO>> Get(int id)
        {
            var todoDto = await _todoService.GetTodo(id);

            if (todoDto is null)
                return NotFound();

            return Ok(todoDto);
        }

        //  /todos
        [HttpPost]
        public async Task<ActionResult<TodoDTO>> Post(TodoDTO todoDto)
        {
            var todo = await _todoService.CreateTodo(todoDto);
            return Ok(todo);
        }

        //  /todos/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, TodoDTO todoDto)
        {
            bool successful = await _todoService.UpdateTodo(id, todoDto);

            if (!successful)
                return NotFound();

            return NoContent();
        }

        //  /todos/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool successful = await _todoService.DeleteTodo(id);

            if (!successful)
                return NotFound();

            return NoContent();
        }
    }
}

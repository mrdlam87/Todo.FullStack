using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoApp.API.ViewModels;
using TodoApp.Application.Common.DTO;
using TodoApp.Application.Services.Interfaces;

namespace TodoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly IMapper _mapper;

        public TodosController(ITodoService todoService, IMapper mapper)
        {
            _todoService = todoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<TodoVm>> GetAllTodos()
        {
            IEnumerable<TodoDto> todos = await _todoService.GetAllTodos();

            return _mapper.Map<IEnumerable<TodoVm>>(todos);
        }

        [HttpPost]
        public async Task<ActionResult<TodoVm>> CreateTodo(TodoVm todoData)
        {
            TodoDto todo = await _todoService.CreateTodo(_mapper.Map<TodoDto>(todoData));

            return Ok(_mapper.Map<TodoVm>(todo));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoVm>> GetTodo(int id)
        {
            TodoDto todo = await _todoService.GetTodo(id);

            return Ok(_mapper.Map<TodoVm>(todo));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TodoVm>> Put(int id, TodoVm todoData)
        {
            TodoDto todo = await _todoService.UpdateTodo(id, _mapper.Map<TodoDto>(todoData));

            return Ok(_mapper.Map<TodoVm>(todo));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _todoService.DeleteTodo(id);

            return NoContent();
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoApp.API.ViewModels;
using TodoApp.Application.Common.DTO;
using TodoApp.Application.Services.Interfaces;

namespace TodoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserVm>>> GetAll()
        {
            IEnumerable<UserDto> users = await _userService.GetAllUsers();

            if (!users.Any())
            {
                return NoContent();
            }

            return Ok(_mapper.Map<IEnumerable<UserVm>>(users));
        }

        [HttpPost]
        public async Task<ActionResult<UserVm>> CreateUser(UserVm userData)
        {
            UserDto user = await _userService.CreateUser(_mapper.Map<UserDto>(userData));

            return Ok(_mapper.Map<UserVm>(user));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<UserVm>> GetUser(int id)
        {
            UserDto user = await _userService.GetUser(id);

            return Ok(_mapper.Map<UserVm>(user));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, UserVm userData)
        {
            UserDto user = await _userService.UpdateUser(id, _mapper.Map<UserDto>(userData));

            return Ok(_mapper.Map<UserVm>(user));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUser(id);

            return NoContent();
        }

        [HttpGet("{id}/todos")]
        public async Task<ActionResult<IEnumerable<TodoVm>>> GetTodosByUserId(int id)
        {
            IEnumerable<TodoDto> todos = await _userService.GetTodosByUser(id);

            if (!todos.Any())
            {
                return NoContent();
            }

            return Ok(_mapper.Map<IEnumerable<TodoVm>>(todos));
        }

        [HttpPost("{id}/todos")]
        public async Task<ActionResult<TodoVm>> PostTodo(int id, TodoVm todoData)
        {
            TodoDto todo = await _userService.CreateUserTodo(id, _mapper.Map<TodoDto>(todoData));

            return Ok(todo);
        }

        [HttpGet("{userId}/todos/{todoId}")]
        public async Task<ActionResult<TodoVm>> GetTodoByUserId(int userId, int todoId)
        {
            TodoDto todo = await _userService.GetSingleTodoByUser(userId, todoId);

            return Ok(_mapper.Map<TodoVm>(todo));
        }

        [HttpPut("{userId}/todos/{todoId}")]
        public async Task<ActionResult<TodoVm>> PutTodo(int userId, int todoId, TodoVm todoData)
        {
            TodoDto todo = await _userService.UpdateUserTodo(userId, todoId, _mapper.Map<TodoDto>(todoData));

            return Ok(_mapper.Map<TodoVm>(todo));
        }

        [HttpDelete("{userId}/todos/{todoId}")]
        public async Task<ActionResult> DeleteTodo(int userId, int todoId)
        {
            await _userService.DeleteUserTodo(userId, todoId);

            return NoContent();
        }
    }
}

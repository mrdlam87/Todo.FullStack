using Microsoft.AspNetCore.Mvc;
using TodoApp.ServiceInterfaces;
using TodoApp.ViewModels;

namespace TodoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITodoService _todoService;
        private ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ITodoService todoService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _todoService = todoService;
            _logger = logger;
        }

        //  /users
        [HttpGet]
        public async Task<IEnumerable<UserDTO>> GetAll() => await _userService.GetAllUsers();

        //  /users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> Get(int id)
        {
            var userDto = await _userService.GetUser(id);

            if (userDto is null)
                return NotFound();

            return Ok(userDto);
        }

        //  /users/{id}/todos
        [HttpGet("{id}/todos")]
        public async Task<IEnumerable<TodoDTO>> GetTodosByUserId(int id)
        {
            return await _userService.GetTodosByUser(id);
        }

        //  /users/{userId}/todos/{todoId}
        [HttpGet("{userId}/todos/{todoId}")]
        public async Task<ActionResult<TodoDTO>> GetTodoByUserId(int userId, int todoId)
        {
            var todo = await _userService.GetSingleTodoByUser(userId, todoId);

            if (todo is null)
                return NotFound();

            return Ok(todo);
        }

        //  /users
        [HttpPost]
        public async Task<ActionResult<UserDTO>> Post(UserDTO userDto)
        {
            var user = await _userService.CreateUser(userDto);
            return Ok(user);
        }

        //  /users/{id}/todos
        [HttpPost("{id}/todos")]
        public async Task<ActionResult<TodoDTO>> PostTodo(int id, TodoDTO todoDto)
        {
            var user = await _userService.GetUser(id);
            if (user is null)
                return NotFound();

            todoDto.UserId = id;
            var todo = await _todoService.CreateTodo(todoDto);
            return Ok(todo);
        }

        //  /users/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, UserDTO userDto)
        {
            bool successful = await _userService.UpdateUser(id, userDto);

            if (!successful)
                return NotFound();

            return NoContent();
        }

        //  /users/{userId}/todos/{todoId}
        [HttpPut("{userId}/todos/{todoId}")]
        public async Task<ActionResult> PutTodo(int userId, int todoId, TodoDTO todoDto)
        {
            todoDto.UserId = userId;
            bool successful = await _userService.UpdateUserTodo(userId, todoId, todoDto);

            if (!successful)
                return NotFound();

            return NoContent();
        }

        //  /users/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool successful = await _userService.DeleteUser(id);

            if (!successful)
                return NotFound();

            return NoContent();
        }

        //  /users/{userId}/todos/{todoId}
        [HttpDelete("{userId}/todos/{todoId}")]
        public async Task<ActionResult> DeleteTodo(int userId, int todoId)
        {
            bool successful = await _userService.DeleteUserTodo(userId, todoId);

            if (!successful)
                return NotFound();

            return NoContent();
        }

    }
}

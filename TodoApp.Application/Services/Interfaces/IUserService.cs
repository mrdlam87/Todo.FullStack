using TodoApp.Application.Common.DTO;

namespace TodoApp.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsers();
        Task<UserDto> GetUser(int id);
        Task<UserDto> CreateUser(UserDto userDTO);
        Task<UserDto> UpdateUser(int id, UserDto userDTO);
        Task DeleteUser(int id);
        Task<IEnumerable<TodoDto>> GetTodosByUser(int userId);
        Task<TodoDto> GetSingleTodoByUser(int userId, int todoId);
        Task<TodoDto> CreateUserTodo(int userId, TodoDto todoDto);
        Task<TodoDto> UpdateUserTodo(int userId, int todoId, TodoDto todoDto);
        Task DeleteUserTodo(int userId, int todoId);
    }
}

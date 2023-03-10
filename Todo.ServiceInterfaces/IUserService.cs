using TodoApp.ViewModels;

namespace TodoApp.ServiceInterfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<UserDTO> GetUser(int id);
        Task<UserDTO> CreateUser(UserDTO userDTO);
        Task<bool> UpdateUser(int id, UserDTO userDTO);
        Task<bool> DeleteUser(int id);
        Task<IEnumerable<TodoDTO>> GetTodosByUser(int userId);
        Task<TodoDTO> GetSingleTodoByUser(int userId, int todoId);
        Task<bool> UpdateUserTodo(int userId, int todoId, TodoDTO todoDto);
        Task<bool> DeleteUserTodo(int userId, int todoId);
    }
}

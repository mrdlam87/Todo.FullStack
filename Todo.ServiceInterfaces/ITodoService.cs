using TodoApp.ViewModels;

namespace TodoApp.ServiceInterfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoDTO>> GetAllTodo();
        Task<TodoDTO> GetTodo(int id);
        Task<TodoDTO> CreateTodo(TodoDTO todoDto);
        Task<bool> UpdateTodo(int id, TodoDTO todoDto);
        Task<bool> DeleteTodo(int id);
    }
}

using TodoApp.Application.Common.DTO;

namespace TodoApp.Application.Services.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoDto>> GetAllTodos();
        Task<TodoDto> GetTodo(int id);
        Task<TodoDto> CreateTodo(TodoDto todoDto);
        Task<TodoDto> UpdateTodo(int id, TodoDto todoDto);
        Task DeleteTodo(int id);

    }
}

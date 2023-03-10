using Microsoft.EntityFrameworkCore;
using TodoApp.ViewModels;
using TodoApp.ApiDatabase;
using TodoApp.ApiDatabase.DomainModels;
using TodoApp.ServiceInterfaces;

namespace TodoApp.Dependencies
{
    public class TodoService : ITodoService
    {
        private readonly ApiDbContext _database;
        public TodoService(ApiDbContext db)
        {
            _database = db;
        }

        public async Task<IEnumerable<TodoDTO>> GetAllTodo()
        {
            return await _database.Todos
                .Select(td => ItemToDTO(td))
                .ToListAsync();
        }

        public async Task<TodoDTO> GetTodo(int id)
        {
            var todo = await _database.Todos.FirstOrDefaultAsync(todo => todo.Id == id);

            if (todo is null)
                return null;

            return ItemToDTO(todo);
        }

        public async Task<TodoDTO> CreateTodo(TodoDTO todoDto)
        {
            Todo todo = new Todo
            {
                Name = todoDto.Name,
                Complete = todoDto.Complete,
                DateCompleted = todoDto.DateCompleted,
                DateCreated = todoDto.DateCreated,
                UserId = todoDto.UserId,
            };
            todo.DateCreated = DateTime.Now;
            _database.Todos.Add(todo);
            await _database.SaveChangesAsync();
            return ItemToDTO(todo);
        }

        public async Task<bool> UpdateTodo(int id, TodoDTO todoDto)
        {
            var updatedTodo = await _database.Todos.FirstOrDefaultAsync(todo => todo.Id == id);
            if (updatedTodo is null)
                return false;

            updatedTodo.Name = todoDto.Name;
            updatedTodo.Complete = todoDto.Complete;
            updatedTodo.DateCompleted = todoDto.DateCompleted;

            await _database.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTodo(int id)
        {
            var todo = await _database.Todos.FirstOrDefaultAsync(todo => todo.Id == id);
            if (todo is null)
                return false;

            _database.Todos.Remove(todo);
            await _database.SaveChangesAsync();
            return true;
        }

        private static TodoDTO ItemToDTO(Todo todo) =>
            new TodoDTO
            {
                Id = todo.Id,
                Name = todo.Name,
                Complete = todo.Complete,
                DateCreated = todo.DateCreated,
                DateCompleted = todo.DateCompleted,
                UserId = todo.UserId,
            };
    }
}

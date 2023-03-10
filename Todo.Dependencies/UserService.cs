using Microsoft.EntityFrameworkCore;
using TodoApp.ApiDatabase;
using TodoApp.ApiDatabase.DomainModels;
using TodoApp.ServiceInterfaces;
using TodoApp.ViewModels;

namespace TodoApp.Dependencies
{
    public class UserService : IUserService
    {
        private readonly ApiDbContext _database;

        public UserService(ApiDbContext db)
        {
            _database = db;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            return await _database.Users
                .Select(us => ItemToDTO(us))
                .ToListAsync();
        }
        public async Task<UserDTO> GetUser(int id)
        {
            var user = await _database.Users.FirstOrDefaultAsync(us => us.Id == id);

            if (user is null)
                return null;

            return ItemToDTO(user);
        }
        public async Task<UserDTO> CreateUser(UserDTO userDTO)
        {
            User user = new User
            {
                Name = userDTO.Name,
            };
            _database.Users.Add(user);
            await _database.SaveChangesAsync();
            return ItemToDTO(user);
        }

        public async Task<bool> UpdateUser(int id, UserDTO userDTO)
        {
            var updatedUser = await _database.Users.FirstOrDefaultAsync(us => us.Id == id);

            if (updatedUser is null)
                return false;

            updatedUser.Name = userDTO.Name;

            await _database.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteUser(int id)
        {
            var user = await _database.Users.FirstOrDefaultAsync(us => us.Id == id);
            if (user is null)
                return false;

            _database.Users.Remove(user);
            await _database.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TodoDTO>> GetTodosByUser(int userId)
        {
            return await _database.Todos
                .Where(td => td.UserId == userId)
                .Select(td => ItemToDTO(td))
                .ToListAsync();
        }

        public async Task<TodoDTO> GetSingleTodoByUser(int userId, int todoId)
        {
            var todo = await _database.Todos.FirstOrDefaultAsync(td => td.UserId == userId && td.Id == todoId);

            if (todo is null)
                return null;

            return ItemToDTO(todo);
        }

        public async Task<bool> UpdateUserTodo(int userId, int todoId, TodoDTO todoDto)
        {
            var updatedTodo = await _database.Todos.FirstOrDefaultAsync(td => td.UserId == userId && td.Id == todoId);

            if (updatedTodo is null)
                return false;

            updatedTodo.Name = todoDto.Name;
            updatedTodo.Complete = todoDto.Complete;

            await _database.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserTodo(int userId, int todoId)
        {
            var todo = await _database.Todos.FirstOrDefaultAsync(td => td.UserId == userId && td.Id == todoId);

            if (todo is null)
                return false;

            _database.Todos.Remove(todo);
            await _database.SaveChangesAsync();

            return true;
        }

        private static UserDTO ItemToDTO(User user) =>
            new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
            };
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

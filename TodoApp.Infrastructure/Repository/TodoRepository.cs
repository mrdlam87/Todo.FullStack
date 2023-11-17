using Microsoft.EntityFrameworkCore.ChangeTracking;
using TodoApp.Application.Common.Interfaces;
using TodoApp.Domain.Entities;
using TodoApp.Infrastructure.Data;

namespace TodoApp.Infrastructure.Repository
{
    public class TodoRepository : Repository<Todo>, ITodoRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public TodoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Todo Update(Todo todo)
        {
            EntityEntry<Todo> result = _dbContext.Todos.Update(todo);

            return result.Entity;
        }
    }
}

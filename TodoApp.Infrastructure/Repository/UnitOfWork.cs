using TodoApp.Application.Common.Interfaces;
using TodoApp.Infrastructure.Data;

namespace TodoApp.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public IUserRepository User { get; private set; }
        public ITodoRepository Todo { get; private set; }
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            User = new UserRepository(_dbContext);
            Todo = new TodoRepository(_dbContext);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

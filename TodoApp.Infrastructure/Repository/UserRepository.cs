using Microsoft.EntityFrameworkCore.ChangeTracking;
using TodoApp.Application.Common.Interfaces;
using TodoApp.Domain.Entities;
using TodoApp.Infrastructure.Data;

namespace TodoApp.Infrastructure.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public User Update(User user)
        {
            EntityEntry<User> result = _dbContext.Users.Update(user);

            return result.Entity;
        }
    }
}

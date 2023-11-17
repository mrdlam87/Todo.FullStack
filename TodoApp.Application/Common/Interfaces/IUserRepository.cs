using TodoApp.Domain.Entities;

namespace TodoApp.Application.Common.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User Update(User user);
    }
}

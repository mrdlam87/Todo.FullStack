using TodoApp.Domain.Entities;

namespace TodoApp.Application.Common.Interfaces
{
    public interface ITodoRepository : IRepository<Todo>
    {
        Todo Update(Todo todo);
    }
}

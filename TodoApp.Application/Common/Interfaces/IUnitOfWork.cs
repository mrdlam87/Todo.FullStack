namespace TodoApp.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        ITodoRepository Todo { get; }
        Task SaveAsync();
    }
}

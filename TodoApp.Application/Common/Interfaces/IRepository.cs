using System.Linq.Expressions;
using TodoApp.Domain.Entities.Base;

namespace TodoApp.Application.Common.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>>? predicate = null, string? includeProperties = null, bool tracked = false);
        Task<TEntity> Get(Expression<Func<TEntity, bool>>? predicate = null, string? includeProperties = null, bool tracked = false);
        Task<TEntity> GetById(int id, string? includeProperties = null, bool tracked = false);
        Task<bool> Any(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> Add(TEntity entity);
        void Remove(TEntity entity);
    }
}

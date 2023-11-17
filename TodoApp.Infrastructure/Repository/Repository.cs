using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using TodoApp.Application.Common.Interfaces;
using TodoApp.Domain.Entities.Base;
using TodoApp.Infrastructure.Data;

namespace TodoApp.Infrastructure.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;
        internal DbSet<TEntity> dbSet;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>>? predicate = null, string? includeProperties = null, bool tracked = false)
        {
            IQueryable<TEntity> query = PrepareQuery(predicate, includeProperties, tracked);

            return await query.ToListAsync();
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>>? predicate = null, string? includeProperties = null, bool tracked = false)
        {
            IQueryable<TEntity> query = PrepareQuery(predicate, includeProperties, tracked);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetById(int id, string? includeProperties = null, bool tracked = false)
        {
            if (string.IsNullOrEmpty(includeProperties) && !tracked)
            {
                return await dbSet.FindAsync(id);
            }
            else
            {
                IQueryable<TEntity> query = tracked ? dbSet : dbSet.AsNoTracking();

                string[]? actualIncludeProperties = includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var includeProp in actualIncludeProperties)
                {
                    query = query.Include(includeProp.Trim());
                }

                return await query.FirstOrDefaultAsync(e => e.Id == id);
            }
        }

        public async Task<bool> Any(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.AnyAsync(predicate);
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            EntityEntry<TEntity> entry = await dbSet.AddAsync(entity);

            return entry.Entity;
        }

        public void Remove(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        private IQueryable<TEntity> PrepareQuery(Expression<Func<TEntity, bool>>? predicate = null, string? includeProperties = null, bool tracked = false)
        {
            IQueryable<TEntity> query = tracked ? dbSet : dbSet.AsNoTracking();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp.Trim());
                }
            }

            return query;
        }
    }
}

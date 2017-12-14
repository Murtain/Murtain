using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Domain.Repository
{
    /// <summary>
    /// This interface must be implemented by all repositories to identify them by convention.
    /// Implement generic version instead of this one.
    /// </summary>
    public interface IRepository
    {

    }
    public interface IRepository<TEntity> : IRepository<TEntity, long>
        where TEntity : class, IEntity<long>
    {

    }
    public interface IRepository<TEntity, TPrimaryKey> : IRepository
        where TEntity : class, IEntity<TPrimaryKey>
    {
        IQueryable<TEntity> Sources { get; }
        IQueryable<TEntity> Models { get; }

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> lambda, Expression<Func<TEntity, object>> includes = null);
        Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> lambda, Expression<Func<TEntity, object>> includes = null);

        TEntity Add(TEntity model);
        Task<TEntity> AddAsync(TEntity model);
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> models);
        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> models);

        void Update(TEntity model);
        Task UpdateAsync(TEntity model);
        void UpdateProperty(TEntity model, Expression<Func<TEntity, object>> lambda);
        Task UpdatePropertyAsync(TEntity model, Expression<Func<TEntity, object>> lambda);
        void UpdateCompare(TEntity model, TEntity source);
        Task UpdateCompareAsync(TEntity model, TEntity source);

        TEntity Remove(TEntity model);
        Task<TEntity> RemoveAsync(TEntity model);
        Task RemoveRange(IEnumerable<TEntity> models);
        Task RemoveRangeAsync(IEnumerable<TEntity> models);

        TEntity Remove(TPrimaryKey key);
        Task<TEntity> RemoveAsync(TPrimaryKey key);
        Task RemoveRange(IEnumerable<TPrimaryKey> keys);
        Task RemoveRangeAsync(IEnumerable<TPrimaryKey> keys);

        bool Any(Expression<Func<TEntity, bool>> lambda);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> lambda);

        int Count();
        Task<int> CountAsync();
        int Count(Expression<Func<TEntity, bool>> lambda);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> lambda);

        TEntity Find(TPrimaryKey key);
        Task<TEntity> FindAsync(TPrimaryKey key);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> lambda);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> lambda);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> lambda, Expression<Func<TEntity, object>> includes);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> lambda, Expression<Func<TEntity, object>> includes);
    }
}

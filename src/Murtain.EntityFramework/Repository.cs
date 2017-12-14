using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Murtain.Domain;
using Murtain.EntityFramework.Provider;

namespace Murtain.EntityFramework
{
    public class Repository<TDbContext, TEntity> : Repository<TDbContext, TEntity, long>
        where TEntity : class, IEntity<long>
        where TDbContext : DbContext
    {
        protected Repository(IEntityFrameworkDbContextProvider<TDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
    public class Repository<TDbContext, TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
        where TDbContext : DbContext
    {

        private readonly IEntityFrameworkDbContextProvider<TDbContext> dbContextProvider;

        protected Repository(IEntityFrameworkDbContextProvider<TDbContext> dbContextProvider)
        {
            this.dbContextProvider = dbContextProvider;
        }

        protected TDbContext dbContext => dbContextProvider.GetDbContext();
        protected DbSet<TEntity> dbSet => dbContext.Set<TEntity>();

        public virtual IQueryable<TEntity> Sources => dbSet.AsQueryable();
        public virtual IQueryable<TEntity> Models => dbSet.AsNoTracking();


        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> lambda)
        {
            return dbSet.Where(lambda).AsNoTracking();
        }

        private void SaveChanges()
        {
            dbContext.SaveChanges();
        }
        private void SaveChangesAsync()
        {
            dbContext.SaveChangesAsync();
        }

        public virtual TEntity Add(TEntity model)
        {
            return dbSet.Add(model).Entity;
        }
        public virtual void AddRange(IEnumerable<TEntity> models)
        {
            dbSet.AddRange(models);
        }

        public virtual void Update(TEntity model)
        {
            AttachIfNot(model);
            dbContext.Entry(model).State = EntityState.Modified;
        }
        public virtual void UpdateProperty(TEntity model, Expression<Func<TEntity, object>> lambda)
        {
            ReadOnlyCollection<MemberInfo> memberInfos = ((dynamic)lambda.Body).Members;
            AttachIfNot(model);
            foreach (MemberInfo memberInfo in memberInfos)
            {
                dbContext.Entry(model).Property(memberInfo.Name).IsModified = true;
            }
        }
        public virtual void UpdateCompare(TEntity model, TEntity source)
        {
            dbContext.Entry(source).CurrentValues.SetValues(model);
        }

        public virtual TEntity Remove(TEntity model)
        {
            return dbSet.Remove(model).Entity;
        }
        public virtual void RemoveRange(IEnumerable<TEntity> models)
        {
            dbSet.RemoveRange(models);
        }
        public virtual TEntity Remove(TPrimaryKey key)
        {
            return Remove(dbSet.Find(key));
        }
        public virtual void RemoveRange(IEnumerable<TPrimaryKey> keys)
        {
            List<TEntity> range = new List<TEntity>();
            foreach (var key in keys)
            {
                var model = dbSet.Find(key);
                range.Add(model);
            }
            RemoveRange(range);
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> lambda)
        {
            return dbSet.Any(lambda);
        }

        public virtual int Count()
        {
            return dbSet.Count();
        }
        public virtual int Count(Expression<Func<TEntity, bool>> lambda)
        {
            return dbSet.Count(lambda);
        }

        public virtual IQueryable<TEntity> FromSql(string sql, params object[] parameters)
        {
            return dbSet.FromSql(sql, parameters);
        }

        public virtual TEntity Find(TPrimaryKey key)
        {
            return dbSet.Find(key);
        }
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> lambda)
        {
            return dbSet.FirstOrDefault(lambda);
        }

        protected virtual void AttachIfNot(TEntity model)
        {
            if (!dbSet.Local.Contains(model))
            {
                dbSet.Attach(model);
            }
        }

        public virtual Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> lambda)
        {
            return Task.FromResult(Get(lambda));
        }

        public virtual Task<TEntity> AddAsync(TEntity model)
        {
            return Task.FromResult(Add(model));
        }
        public virtual Task AddRangeAsync(IEnumerable<TEntity> models)
        {
            AddRange(models);
            return Task.FromResult(0);
        }

        public virtual Task UpdateAsync(TEntity model)
        {
            Update(model);
            return Task.FromResult(0);
        }
        public virtual Task UpdatePropertyAsync(TEntity model, Expression<Func<TEntity, object>> lambda)
        {
            UpdateProperty(model, lambda);
            return Task.FromResult(0);
        }
        public virtual Task UpdateCompareAsync(TEntity model, TEntity source)
        {
            UpdateCompare(model, source);
            return Task.FromResult(0);
        }

        public virtual Task<TEntity> RemoveAsync(TEntity model)
        {
            return Task.FromResult(Remove(model));
        }
        public virtual async Task RemoveRangeAsync(IEnumerable<TEntity> models)
        {
            RemoveRange(models);
            await Task.FromResult(0);
        }
        public virtual Task<TEntity> RemoveAsync(TPrimaryKey key)
        {
            return Task.FromResult(Remove(key));
        }
        public virtual async Task RemoveRangeAsync(IEnumerable<TPrimaryKey> keys)
        {
            RemoveRange(keys);
            await Task.FromResult(0);
        }

        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> lambda)
        {
            return Task.FromResult(Any(lambda));
        }

        public virtual Task<int> CountAsync()
        {
            return Task.FromResult(Count());
        }
        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> lambda)
        {
            return Task.FromResult(Count(lambda));
        }

        public virtual Task<TEntity> FindAsync(TPrimaryKey key)
        {
            return Task.FromResult(Find(key));
        }
        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> lambda)
        {
            return Task.FromResult(FirstOrDefault(lambda));
        }

    }
}

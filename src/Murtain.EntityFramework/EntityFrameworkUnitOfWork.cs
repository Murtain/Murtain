using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Murtain.Domain.UnitOfWork;

namespace Murtain.EntityFramework
{
    public class EntityFrameworkUnitOfWork : UnitOfWorkBase
    {

        protected readonly IDictionary<Type, DbContext> ActiveDbContexts;
        protected TransactionScope CurrentTransaction;

        private readonly IServiceProvider serviceProvider;


        public EntityFrameworkUnitOfWork(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.ActiveDbContexts = new Dictionary<Type, DbContext>();
        }

        public virtual TDbContext GetOrCreateDbContext<TDbContext>()
            where TDbContext : DbContext
        {
            DbContext dbContext;
            if (!ActiveDbContexts.TryGetValue(typeof(TDbContext), out dbContext))
            {
                dbContext = (TDbContext)serviceProvider.GetService(typeof(TDbContext));

                ActiveDbContexts[typeof(TDbContext)] = dbContext;
            }

            return (TDbContext)dbContext;
        }

        public override void Begin()
        {
            var transactionOptions = new TransactionOptions();

            if (UnitOfWorkOption?.IsTransactional != true)
            {
                return;
            }

            if (UnitOfWorkOption?.IsolationLevel != null)
            {
                transactionOptions.IsolationLevel = (IsolationLevel)UnitOfWorkOption?.IsolationLevel.GetValueOrDefault(IsolationLevel.ReadUncommitted);
            };

            if (UnitOfWorkOption?.Timeout != null)
            {
                transactionOptions.Timeout = UnitOfWorkOption.Timeout.Value;
            }

            CurrentTransaction = new TransactionScope(
                (TransactionScopeOption)UnitOfWorkOption?.Scope.GetValueOrDefault(TransactionScopeOption.Required),
                transactionOptions,
                (TransactionScopeAsyncFlowOption)UnitOfWorkOption?.AsyncFlowOption.GetValueOrDefault(TransactionScopeAsyncFlowOption.Enabled)
                );
        }
        public override void Begin(UnitOfWorkOption options)
        {
            if (options?.IsTransactional != true)
            {
                return;
            }

            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = (IsolationLevel)options?.IsolationLevel.GetValueOrDefault(IsolationLevel.ReadUncommitted),
            };
            if (options?.Timeout != null)
            {
                transactionOptions.Timeout = options.Timeout.Value;
            }
            CurrentTransaction = new TransactionScope(
              (TransactionScopeOption)options?.Scope.GetValueOrDefault(TransactionScopeOption.Required),
               transactionOptions,
              (TransactionScopeAsyncFlowOption)options?.AsyncFlowOption.GetValueOrDefault(TransactionScopeAsyncFlowOption.Enabled)
               );

            Begin();
        }

        public override void Complete()
        {
            SaveChanges();
            if (CurrentTransaction != null)
            {
                CurrentTransaction.Complete();
            }
        }
        public override async Task CompleteAsync()
        {
            await SaveChangesAsync();
            if (CurrentTransaction != null)
            {
                CurrentTransaction.Complete();
            }
        }

        public override void Dispose()
        {
            foreach (var dbContext in ActiveDbContexts.Values)
            {
                Release(dbContext);
            }
            if (CurrentTransaction != null)
            {
                CurrentTransaction.Dispose();
            }
        }

        public override void SaveChanges()
        {
            foreach (var dbContext in ActiveDbContexts.Values)
            {
                SaveChangesInDbContext(dbContext);
            }
        }
        public override async Task SaveChangesAsync()
        {
            foreach (var dbContext in ActiveDbContexts.Values)
            {
                await SaveChangesInDbContextAsync(dbContext);
            }
        }

        protected virtual void SaveChangesInDbContext(DbContext dbContext)
        {
            dbContext.SaveChanges();
        }

        protected virtual async Task SaveChangesInDbContextAsync(DbContext dbContext)
        {
            await Task.FromResult(dbContext.SaveChanges());
        }

        protected virtual void Release(DbContext dbContext)
        {
            dbContext.Dispose();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Murtain.Domain.UnitOfWork.Configuration;
using Murtain.Domain.UnitOfWork.EventHandlers;

namespace Murtain.Domain.UnitOfWork
{
    public abstract class UnitOfWorkBase : IUnitOfWork
    {


        public event EventHandler Completed;
        public event EventHandler Disposed;
        public event EventHandler Failed;


        public string Id { get; private set; }
        public IUnitOfWork Outer { get; set; }
        public UnitOfWorkOption UnitOfWorkOption { get; private set; }
        public bool IsDisposed { get; set; }


        public UnitOfWorkBase()
        {
            Id = Guid.NewGuid().ToString("N");
        }

        public void BeforeBegin()
        {
            Begin();
        }
        public void BeforeBegin(UnitOfWorkOption option)
        {
            Begin(option);
        }
        public void BeforeComplete()
        {
            try
            {
                Complete();
                Completed?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception e)
            {
                Failed?.Invoke(this, new EventFailedArgs(e));
                throw e;
            }
        }
        public async Task BeforeCompleteAsync()
        {
            try
            {
                await CompleteAsync();
                Completed?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception e)
            {
                Failed?.Invoke(this, new EventFailedArgs(e));
                throw;
            }
        }
        public void BeforeDispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;

            Dispose();
            Disposed?.Invoke(this, EventArgs.Empty);
        }

        public abstract void Begin();
        public abstract void Begin(UnitOfWorkOption options);
        public abstract void Complete();
        public abstract Task CompleteAsync();
        public abstract void SaveChanges();
        public abstract Task SaveChangesAsync();
        public abstract void Dispose();
    }
}

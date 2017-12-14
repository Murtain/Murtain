using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Domain.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {

        string Id { get; }

        bool IsDisposed { get; }
        IUnitOfWork Outer { get; set; }
        UnitOfWorkOption UnitOfWorkOption { get; }


        void Begin(UnitOfWorkOption options);


        void Complete();
        Task CompleteAsync();

        void SaveChanges();
        Task SaveChangesAsync();
    }
}

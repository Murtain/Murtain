using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Murtain.Domain.UnitOfWork
{
    /// <summary>
    /// Unit of work manager.
    /// Used to begin and control a unit of work.
    /// </summary>
    public interface IUnitOfWorkManager
    {
       
        IUnitOfWork Current { get; }

        IUnitOfWork Begin();

        IUnitOfWork Begin(TransactionScopeOption scope);

        IUnitOfWork Begin(UnitOfWorkOption option);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Murtain.Domain.UnitOfWork
{
    public class UnitOfWorkOption
    {
        public TransactionScopeOption? Scope { get; set; }
        public bool? IsTransactional { get; set; }
        public TimeSpan? Timeout { get; set; }
        public IsolationLevel? IsolationLevel { get; set; }
        public TransactionScopeAsyncFlowOption? AsyncFlowOption { get; set; }
    }
}

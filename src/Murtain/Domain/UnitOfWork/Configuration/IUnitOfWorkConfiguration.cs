using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Murtain.Domain.UnitOfWork.Configuration
{
    public interface IUnitOfWorkConfiguration
    {
        UnitOfWorkOption DefaultUnitOfWorkOption { get; set; }

        //EventHandler UnitOfWorkCompletedHandler { get; set; }
        //EventHandler UnitOfWorkFailedHandler { get; set; }
        //EventHandler UnitOfWorkDisposeHandler { get; set; }
    }
}

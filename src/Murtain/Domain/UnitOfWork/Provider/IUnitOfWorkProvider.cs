using System;
using System.Collections.Generic;
using System.Text;

namespace Murtain.Domain.UnitOfWork.Provider
{

    public interface IUnitOfWorkProvider
    {
        IUnitOfWork Current { get; set; }
    }
}

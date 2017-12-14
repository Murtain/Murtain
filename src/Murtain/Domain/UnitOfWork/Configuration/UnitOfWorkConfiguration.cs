using System;
using System.Collections.Generic;
using System.Text;

namespace Murtain.Domain.UnitOfWork.Configuration
{
    public class UnitOfWorkConfiguration : IUnitOfWorkConfiguration
    {

        public UnitOfWorkConfiguration()
        {
        }

        public UnitOfWorkOption DefaultUnitOfWorkOption { get; set; }
    }
}

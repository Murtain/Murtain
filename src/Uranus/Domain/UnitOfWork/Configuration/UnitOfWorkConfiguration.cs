using System;
using System.Collections.Generic;
using System.Text;

namespace Uranus.Domain.UnitOfWork.Configuration
{
    public class UnitOfWorkConfiguration : IUnitOfWorkConfiguration
    {

        public UnitOfWorkConfiguration()
        {
        }

        public UnitOfWorkOption DefaultUnitOfWorkOption { get; set; }
    }
}

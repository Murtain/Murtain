using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Murtain.Collections;

namespace Murtain.Configuration
{
    public class AppConfiguration : IAppConfiguration
    {
        public IEnumerable<Assembly> AppDomainAssembly { get; set; }
    }
}

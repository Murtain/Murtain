using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Murtain.Caching.Configuration;
using Murtain.Collections;
using Murtain.GlobalSetting.Configuration;

namespace Murtain.Configuration
{
    public interface IAppConfiguration
    {
        IEnumerable<Assembly> AppDomainAssembly { get; set; }
    }
}

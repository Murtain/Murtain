using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Murtain.Builder
{
    public interface IAppServiceCollectionBuilder
    {
        IServiceCollection Services { get; }

        IEnumerable<Assembly> AppDomainAssembly { get; }
    }
}

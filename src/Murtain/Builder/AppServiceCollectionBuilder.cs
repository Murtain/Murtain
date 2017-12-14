using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Murtain.Collections;

namespace Murtain.Builder
{
    public class AppServiceCollectionBuilder : IAppServiceCollectionBuilder
    {
        public IServiceCollection Services { get; }
        public IEnumerable<Assembly> AppDomainAssembly { get; }

        public AppServiceCollectionBuilder(IServiceCollection services, IEnumerable<Assembly> assembly)
        {
            this.Services = services;
            this.AppDomainAssembly = assembly;
        }
    }
}

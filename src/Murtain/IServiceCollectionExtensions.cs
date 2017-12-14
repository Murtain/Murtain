using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

using Murtain;
using Murtain.Builder;
using Murtain.Collections;
using Murtain.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {

        public static IAppServiceCollectionBuilder AddMurtain(this IServiceCollection services)
        {
            services.AddSingleton<IAppServiceCollectionBuilder, AppServiceCollectionBuilder>();
            return new AppServiceCollectionBuilder(services, AssemblyLoader.GetAssemblies());
        }

    }
}

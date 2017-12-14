using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Murtain.AutoMapper.Configuration;
using Murtain.Builder;

namespace Microsoft.AspNetCore.Builder
{
    public static class IAppServiceCollectionBuilderExtensions
    {
        public static IAppServiceCollectionBuilder AddAutoMapper(this IAppServiceCollectionBuilder builder, Action<IAutoMapperConfiguration> invoke = null)
        {
            builder.Services.AddSingleton<IAutoMapperConfiguration>(provider =>
            {
                var c = new AutoMapperConfiguration();
                invoke?.Invoke(c);
                return c;
            });

            return builder;
        }

    }
}

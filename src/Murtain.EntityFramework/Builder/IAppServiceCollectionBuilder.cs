using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Murtain.Builder;
using Murtain.Domain.UnitOfWork;
using Murtain.EntityFramework;
using Murtain.EntityFramework.Configuration;
using Murtain.EntityFramework.Provider;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IAppServiceCollectionBuilderExtensions
    {
        public static IAppServiceCollectionBuilder AddEntityFrameWork(this IAppServiceCollectionBuilder builder)
        {
            builder.Services.AddSingleton<IEntityFrameworkConfiguration, EntityFrameworkConfiguration>();
            builder.Services.AddTransient<IUnitOfWork, EntityFrameworkUnitOfWork>();
            builder.Services.AddTransient(typeof(IEntityFrameworkDbContextProvider<>), typeof(EntityFrameworkDbContextProvider<>));

            return builder;
        }

    }


}

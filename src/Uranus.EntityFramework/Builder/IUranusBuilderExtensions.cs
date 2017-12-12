using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Uranus.Builder;
using Uranus.Domain.UnitOfWork;
using Uranus.EntityFramework;
using Uranus.EntityFramework.Configuration;
using Uranus.EntityFramework.Provider;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IUranusBuilderExtensions
    {
        public static IUranusBuilder AddEntityFrameWork(this IUranusBuilder builder)
        {
            //builder.Services.AddSingleton<IEntityFrameworkConfiguration>(provider =>
            //{
            //    var c = new EntityFrameworkConfiguration();
            //    setupAction(c);
            //    return c;
            //});

            builder.Services.AddSingleton<IEntityFrameworkConfiguration, EntityFrameworkConfiguration>();
            builder.Services.AddTransient<IUnitOfWork, EntityFrameworkUnitOfWork>();
            builder.Services.AddTransient(typeof(IEntityFrameworkDbContextProvider<>), typeof(EntityFrameworkDbContextProvider<>));

            return builder;
        }

    }


}

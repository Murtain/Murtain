using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Uranus.AutoMapper.Configuration;
using Uranus.Builder;

namespace Microsoft.AspNetCore.Builder
{
    public static class IUranusBuilderExtensions
    {
        public static IUranusBuilder AddAutoMapper(this IUranusBuilder builder)
        {
            //builder.Services.AddSingleton<IEntityFrameworkConfiguration>(provider =>
            //{
            //    var c = new EntityFrameworkConfiguration();
            //    setupAction(c);
            //    return c;
            //});

            builder.Services.AddSingleton<IAutoMapperConfiguration, AutoMapperConfiguration>();
        
            return builder;
        }

    }
}

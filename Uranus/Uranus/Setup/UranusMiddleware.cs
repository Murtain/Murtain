using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

using Uranus;
using Uranus.Configuration;

namespace Microsoft.AspNetCore.Builder
{
    public static class UranusMiddleware
    {

        public static IServiceCollection AddUranus(this IServiceCollection services)
        {
            return services;
        }

        public static IApplicationBuilder UseUranus(this IApplicationBuilder builder, UranusConfiguration configuration = null)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return builder;
        }
    }
}

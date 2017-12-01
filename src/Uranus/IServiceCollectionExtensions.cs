using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

using Uranus;
using Uranus.Builder;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {

        public static IUranusBuilder AddUranus(this IServiceCollection services)
        {
            var builder = new UranusBuilder(services);
            return builder;
        }

    }
}

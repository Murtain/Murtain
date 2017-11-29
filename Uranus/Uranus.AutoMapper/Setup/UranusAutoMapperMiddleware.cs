using System;
using System.Collections.Generic;
using System.Text;
using Uranus.AutoMapper.Configuration;

namespace Microsoft.AspNetCore.Builder
{
    public static class UranusAutoMapperMiddleware
    {
        public static IApplicationBuilder UseAutoMapper(this IApplicationBuilder builder, IAutoMapperConfiguration configuration = null)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return builder;
        }
    }
}

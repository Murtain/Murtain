using System;
using System.Collections.Generic;
using System.Text;
using Murtain.Builder;
using Murtain.Collections;
using Murtain.Extensions;
using Murtain.Middleware;

namespace Microsoft.AspNetCore.Builder
{
    public static class IApplicationBuilderExtensions
    {
        public static IAppBuilder UseMurtain(this IApplicationBuilder app)
        {
            app.UseMiddleware<MurtainMiddleware>();

            return new AppBuilder(app);
        }
    }
}


using System;
using System.Collections.Generic;
using System.Text;
using Uranus.Middleware;

namespace Microsoft.AspNetCore.Builder
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseUranus(this IApplicationBuilder app)
        {
            app.UseMiddleware<UranusMiddleware>();
            return app;
        }
    }
}


using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Uranus.Session;

namespace Uranus.Middleware
{
    public class UranusMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;

        public UranusMiddleware(RequestDelegate next, ILogger<UranusMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            logger.LogTrace("Uranus working ...");
            await next(context);
        }

    }
}

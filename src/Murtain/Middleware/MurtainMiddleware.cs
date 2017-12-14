using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Murtain.Session;

namespace Murtain.Middleware
{
    public class MurtainMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;

        public MurtainMiddleware(RequestDelegate next, ILogger<MurtainMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            logger.LogTrace("Murtain working ...");
            await next(context);
        }

    }
}

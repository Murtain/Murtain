using Dora.Interception;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Interception
{
    public class LoggerInterceptor
    {
        private readonly InterceptDelegate next;
        private readonly ILogger logger;

        public LoggerInterceptor(InterceptDelegate next, ILoggerFactory loggerFactory, string category)
        {
            this.next = next;
            this.logger = loggerFactory.CreateLogger(category);
        }

        public async Task InvokeAsync(InvocationContext context)
        {
            try
            {
                logger.LogInformation(context.Method.Name, context.Arguments);
                await next(context);
                logger.LogInformation(context.Method.Name, context.ReturnValue);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                throw;
            }
        }
    }
}

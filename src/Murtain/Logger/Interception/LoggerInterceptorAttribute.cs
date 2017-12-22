using Dora.Interception;
using System;
using System.Collections.Generic;
using System.Text;

namespace Murtain.Interception
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class LoggerInterceptorAttribute : InterceptorAttribute
    {
        public LoggerInterceptorAttribute()
        {
        }
        public override void Use(IInterceptorChainBuilder builder)
        {
            builder.Use<LoggerInterceptor>(this.Order);
        }
    }
}

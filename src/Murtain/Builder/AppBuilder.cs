using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using System.Reflection;

namespace Murtain.Builder
{
    public class AppBuilder : IAppBuilder
    {
        public IApplicationBuilder Builder { get; set; }

        public AppBuilder(IApplicationBuilder app)
        {
            this.Builder = app;
        }
    }
}

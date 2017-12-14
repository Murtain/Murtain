using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Murtain.Builder
{
    public interface IAppBuilder
    {
        IApplicationBuilder Builder { get; set; }
    }
}

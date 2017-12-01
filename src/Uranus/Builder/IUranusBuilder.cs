using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Uranus.Builder
{
    public interface IUranusBuilder
    {
        IServiceCollection Services { get; }
    }
}

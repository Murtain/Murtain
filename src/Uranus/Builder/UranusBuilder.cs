using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Uranus.Builder
{
    public class UranusBuilder : IUranusBuilder
    {

        public IServiceCollection Services { get; }

        public UranusBuilder(IServiceCollection services)
        {
            this.Services = services;
        }
    }
}

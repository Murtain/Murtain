﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

namespace Uranus.AutoMapper.Configuration
{
    public class AutoMapperConfiguration : IAutoMapperConfiguration
    {
        public List<Action<IMapperConfigurationExpression>> Configurators { get; }

        public AutoMapperConfiguration()
        {
            Configurators = new List<Action<IMapperConfigurationExpression>>();
        }
    }
}

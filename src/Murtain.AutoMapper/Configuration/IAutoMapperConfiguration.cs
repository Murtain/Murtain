using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Murtain.Collections;

namespace Murtain.AutoMapper.Configuration
{
    public interface IAutoMapperConfiguration
    {
        IEnumerable<Action<IMapperConfigurationExpression>> Configurators { get; }
    }
}

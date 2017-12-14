using AutoMapper;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Murtain.AutoMapper;
using Murtain.AutoMapper.Configuration;
using Murtain.Builder;
using Murtain.Collections;
using Murtain.Extensions;

namespace Microsoft.AspNetCore.Builder
{
    public static class IAppBuilderExtension
    {

        private static bool MappingConfigure;
        private static readonly object MappingLock = new object();

        public static IAppBuilder ConfigureAutoMapper(this IAppBuilder app)
        {

            var cfg = app.Builder.ApplicationServices.GetService(typeof(IAutoMapperConfiguration)).TryAs<IAutoMapperConfiguration>();

            lock (MappingLock)
            {
                if (!MappingConfigure)
                {
                    Mapper.Initialize(configuration =>
                    {
                        MapAutoAttributes(configuration, AssemblyLoader.GetFilterAssembliesDefinedTypes()
                                       .Where(type => type.IsDefined(typeof(AutoMapAttribute)) || type.IsDefined(typeof(AutoMapFromAttribute)) || type.IsDefined(typeof(AutoMapToAttribute))));

                        MapOtherMappings(configuration, AssemblyLoader.GetFilterAssembliesDefinedTypes()
                                               .Where(type => typeof(IAutoMaping).IsAssignableFrom(type) && type != typeof(IAutoMaping) && !type.IsAbstract));

                        foreach (var configurator in cfg.Configurators)
                        {
                            configurator(configuration);
                        }

                    });
                }
               
                MappingConfigure = true;
            }
            return app;
        }

        private static void MapAutoAttributes(IMapperConfigurationExpression configuration, IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                configuration.CreateAttributeMaps(type);
            }
        }

        private static void MapOtherMappings(IMapperConfigurationExpression configuration, IEnumerable<Type> types)
        {

            foreach (var t in types)
            {
                t.GetMethod(nameof(IAutoMaping.CreateMappings)).Invoke(Activator.CreateInstance(t), new object[] { configuration });
            }
        }
    }
}

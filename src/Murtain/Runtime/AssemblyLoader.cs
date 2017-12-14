using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Text.RegularExpressions;

namespace Murtain.Collections
{
    public static class AssemblyLoader
    {
        private const string ASSEMBLY_SKIP_LOADER_PARTTERN = "^System|^Microsoft|^AutoMapper";

        public static Assembly[] GetAssemblies()
        {
            return Assembly
                    .GetEntryAssembly()
                    .GetReferencedAssemblies()
                    .Where(assembly => !Regex.IsMatch(assembly.FullName, ASSEMBLY_SKIP_LOADER_PARTTERN, RegexOptions.IgnoreCase | RegexOptions.Compiled))
                    .Select(Assembly.Load)
                    .ToArray()
                    ;

        }

        public static IEnumerable<Type> GetFilterAssembliesDefinedTypes()
        {
            var types = new List<Type>();

            foreach (var assembly in GetAssemblies())
            {
                try
                {
                    types.AddRange(assembly.GetTypes().Where(type => type != null));
                }
                catch (ReflectionTypeLoadException e)
                {
                    throw e;
                }
            }
            return types;
        }

    }
}

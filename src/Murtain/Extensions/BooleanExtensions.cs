using System;
using System.Collections.Generic;
using System.Text;

namespace Murtain.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Boolean"/>.
    /// </summary>
    public static class BooleanExtensions
    {
        public static string ToLowerString(this bool value)
        {
            return value.ToString().ToLower();
        }

        public static string ToUpperString(this bool value)
        {
            return value.ToString().ToUpper();
        }
    }
}

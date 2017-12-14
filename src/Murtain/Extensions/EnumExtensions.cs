using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Murtain.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Enum"/>.
    /// </summary>
    public static class EnumExtensions
    {
        public static string TryGetName<T>(this T value)
        {
            Type type = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

            if (type == null || value == null || !type.IsEnum)
            {
                return string.Empty;
            }
            return System.Enum.GetName(type, value);
        }

        public static string TryGetDescription(this Enum enumValue)
        {
            string str = enumValue.ToString();
            FieldInfo field = enumValue.GetType().GetField(str);
            object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (objs == null || objs.Length == 0) return str;
            DescriptionAttribute da = (DescriptionAttribute)objs[0];
            return da.Description;
        }
    }
}

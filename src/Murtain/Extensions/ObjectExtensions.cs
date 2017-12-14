using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Globalization;
using System.Text;

namespace Murtain.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Object"/>.
    /// </summary>
    public static class ObjectExtensions
    {
        public static short TryShort(this object value)
        {
            return TryShort(value, short.MinValue);
        }
        public static short TryShort(this object value, short defValue)
        {
            short result = 0;
            return short.TryParse(value + "", out result) ? result : defValue;
        }
        public static short? TryShort(this object value, short? defValue)
        {
            short result = 0;
            return short.TryParse(value + "", out result) ? result : defValue;
        }
        public static int TryInt(this object value)
        {
            return TryInt(value, int.MinValue);
        }
        public static int TryInt(this object value, int defValue)
        {
            int temp = int.MinValue;
            return int.TryParse(value + "", out temp) ? temp : defValue;
        }
        public static int? TryInt(this object value, int? defValue)
        {
            int temp = int.MinValue;
            return int.TryParse(value + "", out temp) ? temp : defValue;
        }
        public static double TryDouble(this object value)
        {
            return TryDouble(value, double.MinValue);
        }
        public static double TryDouble(this object value, double defValue)
        {
            double temp = double.MinValue;
            return double.TryParse(value + "", out temp) ? temp : defValue;
        }
        public static double? TryDouble(this object value, double? defValue)
        {
            double temp = double.MinValue;
            return double.TryParse(value + "", out temp) ? temp : defValue;
        }
        public static decimal TryDecimal(this object value)
        {
            return TryDecimal(value, decimal.MinValue);
        }
        public static decimal TryDecimal(this object value, decimal defValue)
        {
            decimal temp = decimal.MinValue;
            return decimal.TryParse(value + "", out temp) ? temp : defValue;
        }
        public static decimal? TryDecimal(this object value, decimal? defValue)
        {
            decimal temp = decimal.MinValue;
            return decimal.TryParse(value + "", out temp) ? temp : defValue;
        }
        public static long TryLong(this object value)
        {
            return TryLong(value, long.MinValue);
        }
        public static long TryLong(this object value, long defValue)
        {
            long temp = long.MinValue;
            return long.TryParse(value + "", out temp) ? temp : defValue;
        }
        public static long? TryLong(this object value, long? defValue)
        {
            long temp = long.MinValue;
            return long.TryParse(value + "", out temp) ? temp : defValue;
        }
        public static bool TryBool(this object value)
        {
            return TryBool(value, false);
        }
        public static bool TryBool(this object value, bool defValue)
        {
            bool temp = false;
            return bool.TryParse(value + "", out temp) ? temp : defValue;
        }
        public static bool? TryBool(this object value, bool? defValue)
        {
            bool temp = false;
            return bool.TryParse(value + "", out temp) ? temp : defValue;
        }
        public static byte[] TryByte(this object s)
        {
            try
            {
                return Encoding.UTF8.GetBytes(s.TryString());
            }
            catch (Exception)
            {
                byte[] temp = null;
                return temp;
            }

        }
        public static Guid TryGuid(this object guid)
        {
            return TryGuid(guid, Guid.Empty);
        }
        public static Guid TryGuid(this object guid, Guid defvalue)
        {
            return guid == null ? defvalue : Guid.Parse(guid.ToString());
        }
        public static string TryString(this object s)
        {
            return TryString(s, "");
        }
        public static string TryString(this object s, string defvalue)
        {
            return s == null ? defvalue : s.ToString();
        }
        public static dynamic TryDynamic(this object value)
        {
            IDictionary<string, object> expando = new ExpandoObject();
            Type type = value.GetType();
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(type);
            foreach (PropertyDescriptor property in properties)
            {
                var val = property.GetValue(value);
                if (property.PropertyType.FullName.StartsWith("<>f__AnonymousType"))
                {
                    dynamic dval = val.TryDynamic();
                    expando.Add(property.Name, dval);
                }
                else
                {
                    expando.Add(property.Name, val);
                }
            }
            return expando as ExpandoObject;
        }
        public static T TryEmun<T>(this string s)
        {
            return (T)(Enum.Parse(typeof(T), s));
        }
        public static DateTime TryDateTime(this Object strText)
        {
            return TryDateTime(strText, "1900-01-01 00:00:00".TryDateTime());
        }
        public static string TryDefaultDateTime(this Object strText)
        {
            if (strText == null || ((DateTime)strText).Year == "1900-01-01 00:00:00".TryDateTime().Year)
            {
                return null;
            }
            return TryDateTime(strText, "1900-01-01 00:00:00".TryDateTime()).ToString();
        }
        public static DateTime TryDateTime(this Object strText, DateTime defValue)
        {
            DateTime temp = "1900-01-01 00:00:00".TryDateTime();
            return DateTime.TryParse(strText + "", out temp) ? temp : defValue;
        }

        /// <summary>
        /// Used to simplify and beautify casting an object to a type. 
        /// </summary>
        /// <typeparam name="T">Type to be casted</typeparam>
        /// <param name="obj">Object to cast</param>
        /// <returns>Casted object</returns>
        public static T TryAs<T>(this object obj)
            where T : class
        {
            return obj == null ? null : (T)obj;
        }

        /// <summary>
        /// Converts given object to a value type using <see cref="Convert.ChangeType(object,System.TypeCode)"/> method.
        /// </summary>
        /// <param name="obj">Object to be converted</param>
        /// <typeparam name="T">Type of the target object</typeparam>
        /// <returns>Converted object</returns>
        public static T TryTo<T>(this object obj)
            where T : struct
        {
            return (T)Convert.ChangeType(obj, typeof(T), CultureInfo.InvariantCulture);
        }

    }
}

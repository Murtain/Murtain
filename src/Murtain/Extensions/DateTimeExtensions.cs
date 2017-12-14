using System;
using System.Collections.Generic;
using System.Text;

namespace Murtain.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="DateTime"/>.
    /// </summary>
    public static class DateTimeExtensions
    {
        public static DateTime TryGetValue<DateTime>(this DateTime? value) where DateTime : struct
        {
            return value ?? default(DateTime);
        }

        public static DateTime FirstDayOfTheMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime LastDayOfTheMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }

        public static DateTime SetTime(this DateTime date, int hour)
        {
            return date.SetTime(hour, 0, 0, 0);
        }

        public static DateTime SetTime(this DateTime date, int hour, int minute)
        {
            return date.SetTime(hour, minute, 0, 0);
        }

        public static DateTime SetTime(this DateTime date, int hour, int minute, int second)
        {
            return date.SetTime(hour, minute, second, 0);
        }

        public static DateTime SetTime(this DateTime date, int hour, int minute, int second, int millisecond)
        {
            return new DateTime(date.Year, date.Month, date.Day, hour, minute, second, millisecond);
        }

        public static bool IsBefore(this DateTime date, DateTime value)
        {
            return date < value;
        }

        public static bool IsBeforeOrEqual(this DateTime date, DateTime value)
        {
            return date <= value;
        }

        public static bool IsAfter(this DateTime date, DateTime value)
        {
            return date > value;
        }

        public static bool IsAfterOrEqual(this DateTime date, DateTime value)
        {
            return date >= value;
        }

    }
}

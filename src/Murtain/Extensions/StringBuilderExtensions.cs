using System;
using System.Collections.Generic;
using System.Text;

namespace Murtain.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="StringBuilder"/>.
    /// </summary>
    public static class StringBuilderExtensions
    {

        public static StringBuilder TrimStart(this StringBuilder sb)
        {
            return sb.TrimStart(' ');
        }

        public static StringBuilder TrimStart(this StringBuilder sb, char c)
        {
            if (sb.Length == 0)
                return sb;
            while (c.Equals(sb[0]))
            {
                sb.Remove(0, 1);
            }
            return sb;
        }

        public static StringBuilder TrimStart(this StringBuilder sb, char[] cs)
        {
            return sb.TrimStart(new string(cs));
        }

        public static StringBuilder TrimStart(this StringBuilder sb, string str)
        {
            if (string.IsNullOrEmpty(str)
                || sb.Length == 0
                || str.Length > sb.Length)
                return sb;
            while (sb.SubString(0, str.Length).Equals(str))
            {
                sb.Remove(0, str.Length);
                if (str.Length > sb.Length)
                {
                    break;
                }
            }
            return sb;
        }

        public static StringBuilder TrimEnd(this StringBuilder sb)
        {
            return sb.TrimEnd(' ');
        }

        public static StringBuilder TrimEnd(this StringBuilder sb, char c)
        {
            if (sb.Length == 0)
                return sb;
            while (c.Equals(sb[sb.Length - 1]))
            {
                sb.Remove(sb.Length - 1, 1);
            }
            return sb;
        }

        public static StringBuilder TrimEnd(this StringBuilder sb, char[] chars)
        {
            return sb.TrimEnd(new string(chars));
        }

        public static StringBuilder TrimEnd(this StringBuilder sb, string str)
        {
            if (string.IsNullOrEmpty(str)
                || sb.Length == 0
                || str.Length > sb.Length)
                return sb;
            while (sb.SubString(sb.Length - str.Length, str.Length).Equals(str))
            {
                sb.Remove(sb.Length - str.Length, str.Length);
                if (sb.Length < str.Length)
                {
                    break;
                }
            }
            return sb;
        }

        public static StringBuilder Trim(this StringBuilder sb)
        {
            if (sb.Length == 0)
                return sb;
            return sb.TrimEnd().TrimStart();
        }

        public static string SubString(this StringBuilder sb, int start, int length)
        {
            if (start + length > sb.Length)
                throw new IndexOutOfRangeException();
            char[] cs = new char[length];
            for (int i = 0; i < length; i++)
            {
                cs[i] = sb[start + i];
            }
            return new string(cs);
        }
    }
}

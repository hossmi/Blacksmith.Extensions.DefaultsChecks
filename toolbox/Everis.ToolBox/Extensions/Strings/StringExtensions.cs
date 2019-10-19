using System;
using Everis.ToolBox;

namespace Everis.ToolBox.Extensions.Strings
{
    public static class StringExtensions
    {
        public static string format(this string format, params object[] args)
        {
            Asserts.isNotNull(format);
            Asserts.isNotNull(args);

            for (int i = 0, n = args.Length; i < n; ++i)
                Asserts.isNotNull(args[i]);

            return string.Format(format, args);
        }

        public static string format(this string format, IFormatProvider formatProvider, params object[] args)
        {
            Asserts.isNotNull(format);
            Asserts.isNotNull(args);
            Asserts.isNotNull(formatProvider);

            for (int i = 0, n = args.Length; i < n; ++i)
                Asserts.isNotNull(args[i]);

            return string.Format(formatProvider, format, args);
        }

        public static bool isFilled(this string text)
        {
            return string.IsNullOrWhiteSpace(text) == false;
        }

        public static bool isEmpty(this string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }
    }
}

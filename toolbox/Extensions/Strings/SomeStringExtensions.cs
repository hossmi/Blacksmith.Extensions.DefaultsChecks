using System;

namespace Blacksmith.Extensions.Strings
{
    public static class SomeStringExtensions
    {
        public static string getValueOrEmpty(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;
            else
                return text;
        }

    }
}

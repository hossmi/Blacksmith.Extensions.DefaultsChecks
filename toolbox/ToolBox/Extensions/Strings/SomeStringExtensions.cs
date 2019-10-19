using System;

namespace ToolBox.Extensions.Strings
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

        public static bool isFilled(this string text)
        {
            return false == string.IsNullOrWhiteSpace(text);
        }

        public static bool isEmpty(this string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }
    }
}

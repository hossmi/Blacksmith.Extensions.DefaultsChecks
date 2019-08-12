using System;
using Everis.ToolBox;

namespace Everis.ToolBox.Extensions.Strings
{
    public static class StringExtensions
    {
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

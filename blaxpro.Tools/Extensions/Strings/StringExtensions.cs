
namespace Blaxpro.Tools.Extensions.Strings
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

        public static string f(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
    }
}

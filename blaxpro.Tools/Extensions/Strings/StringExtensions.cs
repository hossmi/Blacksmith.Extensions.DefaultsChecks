
namespace blaxpro.Tools.Extensions.Strings
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

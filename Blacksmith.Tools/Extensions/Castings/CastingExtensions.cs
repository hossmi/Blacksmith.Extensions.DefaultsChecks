using Blacksmith.Tools;
using System;

namespace Blacksmith.Extensions.Castings
{
    public static class CastingExtensions
    {
        public static T defaultIfNull<T>(this T item, Func<T> buildDelegate) where T : class
        {
            if (item != null)
                return item;

            if (buildDelegate == null)
                throw new ArgumentNullException(nameof(buildDelegate));

            return buildDelegate()
                ?? throw new NullResultFromDelegateMethodException(nameof(buildDelegate));
        }

        public static T defaultIfNull<T>(this T item) where T : class, new()
        {
            if (item != null)
                return item;

            return new T();
        }

        public static bool isFilled(this string text)
        {
            return string.IsNullOrWhiteSpace(text) == false;
        }

        public static bool isEmpty(this string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        public static string defaultIfNullOrWhiteSpace(this string text)
        {
            return string.IsNullOrWhiteSpace(text)
                ? string.Empty
                : text;
        }

    }
}

using Blacksmith.Validations;
using System;
using System.Collections.Generic;

namespace Blacksmith.Extensions.Queryables
{
    public static class EnumerableExtensions
    {
        public static void apply<T>(this IEnumerable<T> items, Action<T> apply)
        {
            foreach (T item in items)
                apply(item);
        }

        public static Tout mapOrDefault<Tin, Tout>(this Tin item, Func<Tin, Tout> mapDelegate)
            where Tin : class
            where Tout : class
        {
            Tout result;

            Asserts.Assert.isNotNull(mapDelegate);

            if (item == null)
                return null;

            result = mapDelegate(item);
            Asserts.Assert.isNotNull(result);

            return result;
        }
    }
}

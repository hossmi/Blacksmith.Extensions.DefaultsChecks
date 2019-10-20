using Blacksmith.Tools.Extensions.Strings;
using Blacksmith.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public static IEnumerable<T> whereIf<T>(this IEnumerable<T> items, bool condition, Func<T, bool> predicate)
        {
            if (condition)
                return items.Where(predicate);
            else
                return items;
        }

        public static IEnumerable<T> whereIfStringIsFilled<T>(this IEnumerable<T> items, string text, Func<T, bool> predicate)
        {
            return whereIf<T>(items, text.isFilled(), predicate);
        }

        public static T single<T, TException>(this IEnumerable<T> items
            , Func<T, bool> predicate
            , Func<InvalidOperationException, TException> wrapException)
            where TException : Exception
        {
            try
            {
                return items.Single<T>(predicate);
            }
            catch (InvalidOperationException ex)
            {
                throw wrapException(ex);
            }
        }

        public static T single<T, TException>(this IEnumerable<T> items
            , Func<InvalidOperationException, TException> wrapException)
            where TException : Exception
        {
            try
            {
                return items.Single<T>();
            }
            catch (InvalidOperationException ex)
            {
                throw wrapException(ex);
            }
        }

        public static T singleOrDefault<T, TException>(this IEnumerable<T> items
            , Func<T, bool> predicate
            , Func<InvalidOperationException, TException> wrapException)
            where TException : Exception
        {
            try
            {
                return items.SingleOrDefault<T>(predicate);
            }
            catch (InvalidOperationException ex)
            {
                throw wrapException(ex);
            }
        }

        public static T singleOrDefault<T, TException>(this IEnumerable<T> items
            , Func<InvalidOperationException, TException> wrapException)
            where TException : Exception
        {
            try
            {
                return items.SingleOrDefault<T>();
            }
            catch (InvalidOperationException ex)
            {
                throw wrapException(ex);
            }
        }
    }
}

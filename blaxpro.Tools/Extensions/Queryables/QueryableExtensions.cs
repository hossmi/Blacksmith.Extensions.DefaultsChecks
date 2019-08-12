using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using blaxpro.Tools.Extensions.Strings;

namespace Everis.ToolBox.Extensions.Queryables
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> whereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            if (condition)
                return query.Where<T>(predicate);
            else
                return query;
        }

        public static IEnumerable<T> whereIf<T>(this IEnumerable<T> items, bool condition, Func<T, bool> predicate)
        {
            if (condition)
                return items.Where(predicate);
            else
                return items;
        }

        public static IQueryable<T> whereIfStringIsFilled<T>(this IQueryable<T> query, string text, Expression<Func<T, bool>> predicate)
        {
            return whereIf<T>(query, text.isFilled(), predicate);
        }

        public static IEnumerable<T> whereIfStringIsFilled<T>(this IEnumerable<T> items, string text, Func<T, bool> predicate)
        {
            return whereIf<T>(items, text.isFilled(), predicate);
        }
    }
}

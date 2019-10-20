using System;
using System.Linq;
using System.Linq.Expressions;
using Blacksmith.Tools.Extensions.Strings;

namespace Blacksmith.Tools.Extensions.Queryables
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

        public static IQueryable<T> whereIfStringIsFilled<T>(this IQueryable<T> query, string text, Expression<Func<T, bool>> predicate)
        {
            return whereIf<T>(query, text.isFilled(), predicate);
        }
    }
}

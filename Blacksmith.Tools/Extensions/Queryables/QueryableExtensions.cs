using System;
using System.Collections.Generic;
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

        public enum OrderDirection
        {
            Ascending,
            Descending,
        }

        public static IOrderedQueryable<TSource> orderBy<TSource, TKey>(
            this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, OrderDirection direction)
        {
            if (direction == OrderDirection.Descending)
                return source.OrderByDescending<TSource, TKey>(keySelector);
            else
                return source.OrderBy<TSource, TKey>(keySelector);
        }

        public static IOrderedQueryable<TSource> orderBy<TSource, TKey>(
            this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer, OrderDirection direction)
        {
            if (direction == OrderDirection.Descending)
                return source.OrderByDescending<TSource, TKey>(keySelector, comparer);
            else
                return source.OrderBy<TSource, TKey>(keySelector, comparer);
        }

        public static IQueryable<T> paginate<T>(IQueryable<T> source, int pageSize, int page)
        {
            return source
                .Skip(page * pageSize)
                .Take(pageSize);
        }

        public static IEnumerable<T> paginate<T>(IEnumerable<T> source, int pageSize, int page)
        {
            return source
                .Skip(page * pageSize)
                .Take(pageSize);
        }
    }
}

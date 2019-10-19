using Blacksmith.Queries;
using System.Linq;
using System;
using System.Linq.Expressions;
using Blacksmith.Tools.Extensions.Strings;
using Blacksmith.Validations;

namespace Blacksmith.Extensions.Queries
{
    public static class QueryExtensions
    {
        public static T fromPage<T>(this T query, int page, int pageSize) where T : IQuery
        {
            query.Page = page;
            query.PageSize = pageSize;

            return query;
        }

        public static IQueryable<T> setOrder<T>(this IQueryable<T> query, string sortBy, string sortDirection)
        {
            if (sortBy.isFilled() && sortDirection.isFilled())
            {
                bool directionIsAscOrDesc;

                directionIsAscOrDesc = 
                    sortDirection.ToUpper() == "ASC" 
                    || sortDirection.ToUpper() == "DESC";

                Asserts.Assert.isTrue(directionIsAscOrDesc, $"Only ASC or DESC {nameof(sortDirection)} values are allowed.");
                return prv_orderByField(query, sortBy, sortDirection.ToUpper() == "ASC");
            }
            else
            {
                return query;
            }
        }

        private static IQueryable<T> prv_orderByField<T>(IQueryable<T> q, string sortField, bool ascending)
        {
            string[] props = sortField.Split('_');
            MemberExpression prop;
            MemberExpression secondProp;
            LambdaExpression exp;
            MethodCallExpression mce;
            ParameterExpression param;
            string method;

            Type[] types;

            Asserts.Assert.isTrue(props.Length > 0 && props.Length <= 2, $"Only one or two chained properties are allowed");

            param = Expression.Parameter(typeof(T), "p");

            if (props.Length == 1)
            {
                prop = Expression.Property(param, props[0]);
                exp = Expression.Lambda(prop, param);
            }
            else
            {
                prop = Expression.Property(param, props[0]);
                secondProp = Expression.Property(prop, props[1]);
                exp = Expression.Lambda(secondProp, param);
            }

            method = ascending ? "OrderBy" : "OrderByDescending";
            types = new Type[] { q.ElementType, exp.Body.Type };
            mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);

            return q.Provider.CreateQuery<T>(mce);
        }
    }
}
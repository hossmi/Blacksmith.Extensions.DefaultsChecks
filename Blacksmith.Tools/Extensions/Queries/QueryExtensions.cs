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
    }
}
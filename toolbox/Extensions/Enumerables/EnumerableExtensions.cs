using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Blacksmith.Extensions.Strings;

namespace Blacksmith.Extensions.Queryables
{
    public static class EnumerableExtensions
    {
        public static void apply<T>(this IEnumerable<T> items, Action<T> apply)
        {
            foreach (T item in items)
                apply(item);
        }
    }
}

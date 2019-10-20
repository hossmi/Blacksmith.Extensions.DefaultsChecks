using System.Collections.Generic;

namespace Blacksmith.Queries
{
    public interface IQuery 
    {
        int Page { get; set; }
        int PageSize { get; set; }
        int Count { get; }
    }

    public interface IQuery<T> : IEnumerable<T>, IQuery
    {
    }
}
using System.Collections.Generic;

namespace Everis.ToolBox.Queries
{
    public interface IQuery 
    {
        int Page { get; set; }
        int PageSize { get; set; }
        int Count { get; }
        string SortBy{ get; set; }
        string SortDirection { get; set; }
    }

    public interface IQuery<T> : IEnumerable<T>, IQuery
    {

    }
}
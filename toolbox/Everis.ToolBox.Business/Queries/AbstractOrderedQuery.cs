using System.Linq;

namespace Everis.ToolBox.Queries
{
    public abstract class AbstractOrderedQuery<Tin, TOut> : AbstractQuery<Tin,TOut>
    {
        public AbstractOrderedQuery(IOrderedQueryable<Tin> query) : base(query, false)
        {
        }
    }
}

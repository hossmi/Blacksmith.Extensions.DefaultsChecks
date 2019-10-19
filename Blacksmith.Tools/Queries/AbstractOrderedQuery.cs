using System.Linq;

namespace Blacksmith.Queries
{
    public abstract class AbstractOrderedQuery<Tin, TOut> : AbstractQuery<Tin,TOut>
    {
        public AbstractOrderedQuery(IOrderedQueryable<Tin> query) : base(query, false)
        {
        }
    }
}

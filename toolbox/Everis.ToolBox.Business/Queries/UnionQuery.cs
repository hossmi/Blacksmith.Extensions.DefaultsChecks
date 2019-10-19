using System;
using System.Collections.Generic;
using System.Linq;

namespace Everis.ToolBox.Queries
{
    public class UnionQuery<Tin, Tout> : AbstractQuery<Tin, Tout>
    {
        private readonly Func<Tin, Tout> mapDelegate;

        public UnionQuery(IEnumerable<IQueryable<Tin>> queries, Func<Tin, Tout> mapDelegate) 
            : base(queries.Aggregate((union, q) => union.Concat(q)), true)
        {
            Asserts.isNotNull(mapDelegate);

            this.mapDelegate = mapDelegate;
        }

        protected override Tout prv_map(Tin dbEntity)
        {
            return this.mapDelegate(dbEntity);
        }
    }
}

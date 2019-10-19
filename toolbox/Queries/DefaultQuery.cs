using System;
using System.Collections;
using System.Linq;
using Blacksmith;

namespace Blacksmith.Queries
{
    public class DefaultQuery<Tin, Tout> : AbstractOrderedQuery<Tin, Tout>
    {
        private readonly Func<Tin, Tout> mapDelegate;

        public DefaultQuery(IOrderedQueryable<Tin> query, Func<Tin, Tout> mapDelegate) : base(query)
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

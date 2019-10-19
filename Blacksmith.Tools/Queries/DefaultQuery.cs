using System;
using System.Linq;
using Blacksmith.Validations;

namespace Blacksmith.Queries
{
    public class DefaultQuery<Tin, Tout> : AbstractOrderedQuery<Tin, Tout>
    {
        private readonly Func<Tin, Tout> mapDelegate;

        public DefaultQuery(IOrderedQueryable<Tin> query, Func<Tin, Tout> mapDelegate) : base(query)
        {
            Asserts.Assert.isNotNull(mapDelegate);

            this.mapDelegate = mapDelegate;
        }

        protected override Tout prv_map(Tin dbEntity)
        {
            return this.mapDelegate(dbEntity);
        }
    }
}

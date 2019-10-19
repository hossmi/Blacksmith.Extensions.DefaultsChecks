using System;
using Blacksmith;

namespace Blacksmith.Extensions.Mapping
{
    public static class MappingExtensions
    {
        public static Tout mapOrDefault<Tin, Tout>(this Tin item, Func<Tin, Tout> mapDelegate) 
            where Tin : class 
            where Tout : class
        {
            Tout result;

            Asserts.isNotNull(mapDelegate);

            if (item == null)
                return null;

            result = mapDelegate(item);
            Asserts.isNotNull(result);

            return result;
        }
    }
}

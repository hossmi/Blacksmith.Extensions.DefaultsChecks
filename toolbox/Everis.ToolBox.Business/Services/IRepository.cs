using System;

namespace Everis.ToolBox.Services
{
    public interface IRepository<TKey, T>
    {
        T singleOrDefault(TKey key);

        bool any(Func<T, bool> condition);
        void add(ref T entity);
    }
}
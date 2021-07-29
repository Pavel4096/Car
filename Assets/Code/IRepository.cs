using System.Collections.Generic;

namespace Car
{
    public interface IRepository<TKey, TValue> : IControllerBase
    {
        IReadOnlyDictionary<TKey, TValue> Items { get; }
    }
}

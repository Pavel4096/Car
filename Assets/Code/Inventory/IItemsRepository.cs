using System.Collections.Generic;

namespace Car.Inventory
{
    public interface IItemsRepository : IControllerBase
    {
        IReadOnlyDictionary<int, IItem> Items { get; }
    }
}

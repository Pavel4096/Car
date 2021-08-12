using System;
using System.Collections.Generic;

namespace Car.Inventory
{
    public interface IInventoryController : IControllerBase
    {
        IReadOnlyList<IItem> EquippedItems { get; }
        event Action Exit;
        void Show();
    }
}

using System;
using System.Collections.Generic;

namespace Car.Inventory
{
    public interface IInventoryView
    {
        event Action<IItem> Selected;
        event Action<IItem> Deselected;
        event Action Exit;
        void Init();
        void Display(IReadOnlyDictionary<int, IItem> items);
        void Dispose();
    }
}

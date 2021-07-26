using System;
using System.Collections.Generic;

namespace Car.Inventory
{
    public interface IInventoryView
    {
        event Action<IItem> Selected;
        event Action<IItem> Deselected;
        void Display(List<IItem> items);
    }
}

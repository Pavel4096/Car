using System.Collections.Generic;

namespace Car.Inventory
{
    public interface IInventoryModel
    {
        void Equip(IItem item);
        void Unequip(IItem item);
        IReadOnlyList<IItem> EquippedItems { get; }
    }
}

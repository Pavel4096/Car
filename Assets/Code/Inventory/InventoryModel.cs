using System.Collections.Generic;

namespace Car.Inventory
{
    public class InventoryModel : IInventoryModel
    {
        private List<IItem> equippedItems = new List<IItem>();

        public IReadOnlyList<IItem> EquippedItems => equippedItems;

        public void Equip(IItem item)
        {
            if(equippedItems.Contains(item))
                return;

            equippedItems.Add(item);
        }

        public void Unequip(IItem item)
        {
            if(equippedItems.Contains(item))
                equippedItems.Remove(item);
        }
    }
}

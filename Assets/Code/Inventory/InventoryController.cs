using System;
using System.Collections.Generic;

namespace Car.Inventory
{
    public class InventoryController : ControllerBase, IInventoryController
    {
        private IInventoryModel inventoryModel;
        private IInventoryView inventoryView;
        private IItemsRepository itemsRepository;

        public InventoryController(IInventoryModel _inventoryModel, IInventoryView _inventoryView, IItemsRepository _itemsRepository)
        {
            inventoryModel = _inventoryModel;
            inventoryView = _inventoryView;
            itemsRepository = _itemsRepository;
        }

        public void Show(Action exitHandler)
        {}

        public void Hide()
        {}
    }
}

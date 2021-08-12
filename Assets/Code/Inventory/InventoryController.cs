using Car.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Car.Inventory
{
    public class InventoryController : ControllerBase, IInventoryController
    {
        public event Action Exit;

        private IInventoryModel inventoryModel;
        private IInventoryView inventoryView;
        private IRepository<int, IItem> itemsRepository;

        private readonly ResourcePath inventoryViewPath = new ResourcePath("InventoryButtons");

        public InventoryController(IInventoryModel _inventoryModel, IInventoryView _inventoryView, IRepository<int, IItem> _itemsRepository, Transform uiroot)
        {
            inventoryModel = _inventoryModel;
            inventoryView = _inventoryView;
            itemsRepository = _itemsRepository;
            inventoryView.Init();
            inventoryView.Selected += ItemSelected;
            inventoryView.Deselected += ItemDeselected;
            inventoryView.Exit += OnExit;
        }

        public IReadOnlyList<IItem> EquippedItems => inventoryModel.EquippedItems;

        public void Show()
        {
            inventoryView.Display(itemsRepository.Items);
        }

        protected override void OnDispose()
        {
            inventoryView.Selected -= ItemSelected;
            inventoryView.Deselected -= ItemDeselected;
            inventoryView.Exit -= OnExit;
            inventoryView.Dispose();
        }

        private void ItemSelected(IItem item)
        {
            inventoryModel.Equip(item);
        }

        private void ItemDeselected(IItem item)
        {
            inventoryModel.Unequip(item);
        }

        private void OnExit()
        {
            Exit?.Invoke();
        }
    }
}

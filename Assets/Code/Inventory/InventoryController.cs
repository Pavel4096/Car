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
        private IItemsRepository itemsRepository;

        private readonly ResourcePath inventoryViewPath = new ResourcePath("InventoryButtons");

        public InventoryController(IInventoryModel _inventoryModel, IItemsRepository _itemsRepository, Transform uiroot)
        {
            inventoryModel = _inventoryModel;
            inventoryView = LoadView(uiroot);
            itemsRepository = _itemsRepository;
            inventoryView.Init();
            inventoryView.Selected += ItemSelected;
            inventoryView.Deselected += ItemDeselected;
            inventoryView.Exit += OnExit;
        }

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

        private IInventoryView LoadView(Transform uiroot)
        {
            var inventoryViewObject = UnityEngine.Object.Instantiate(ResourceLoader.Load(inventoryViewPath), uiroot, false);

            AddObject(inventoryViewObject);

            return inventoryViewObject.GetComponent<IInventoryView>();
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

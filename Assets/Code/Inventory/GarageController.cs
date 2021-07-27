using System.Collections.Generic;
using UnityEngine;

namespace Car.Inventory
{
    public class GarageController : ControllerBase, IGarageController
    {
        private UpgradeHandlersRepository upgradeHandlers;
        private IItemsRepository itemsRepository;
        private IInventoryModel inventoryModel;
        private IInventoryController inventoryController;
        private IUpgradableCar upgradableCar;

        public GarageController(List<ItemUpgradeConfig> configs, IInventoryView _inventoryView, IUpgradableCar _upgradableCar, Transform uiroot)
        {
            upgradableCar = _upgradableCar;
            upgradeHandlers = new UpgradeHandlersRepository(configs);
            AddController(upgradeHandlers);
            itemsRepository = new ItemsRepository(GetItemConfigs(configs));
            AddController(itemsRepository);
            inventoryModel = new InventoryModel();
            inventoryController = new InventoryController(inventoryModel, itemsRepository, uiroot);
            AddController(inventoryController);
            inventoryController.Exit += Exit;
        }

        public void Enter()
        {
            inventoryController.Show();
        }

        public void Exit()
        {
            ApplyEquippedUpgrades(upgradeHandlers.UpgradeHandlers);
        }

        protected override void OnDispose()
        {
            inventoryController.Exit -= Exit;
        }

        private List<ItemConfig> GetItemConfigs(List<ItemUpgradeConfig> configs)
        {
            var result = new List<ItemConfig>(configs.Count);

            foreach(var config in configs)
            {
                result.Add(config.Item);
            }

            return result;
        }

        private void ApplyEquippedUpgrades(IReadOnlyDictionary<int, IUpgradeCarHandler> handlers)
        {
            foreach(var upgrade in inventoryModel.EquippedItems)
            {
                if(handlers.TryGetValue(upgrade.Id, out var upgradeHandler))
                    upgradeHandler.Upgrade(upgradableCar);
            }
        }
    }
}

using System.Collections.Generic;

namespace Car.Inventory
{
    public class GarageController : ControllerBase, IGarageController
    {
        private UpgradeHandlersRepository upgradeHandlers;
        private IItemsRepository itemsRepository;
        private IInventoryModel inventoryModel;
        private IInventoryController inventoryController;
        private IUpgradableCar upgradableCar;

        public GarageController(List<ItemUpgradeConfig> configs, IUpgradableCar _upgradableCar)
        {
            upgradableCar = _upgradableCar;
            upgradeHandlers = new UpgradeHandlersRepository(configs);
            AddController(upgradeHandlers);
            itemsRepository = new ItemsRepository(GetItemConfigs(configs));
            AddController(itemsRepository);
            inventoryModel = new InventoryModel();
            inventoryController = new InventoryController(inventoryModel, null, itemsRepository);
            AddController(inventoryController);
        }

        public void Enter()
        {
            inventoryController.Show(Exit);
        }

        public void Exit()
        {
            ApplyEquippedUpgrades(upgradeHandlers.UpgradeHandlers);
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

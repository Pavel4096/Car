using System.Collections.Generic;
using UnityEngine;

namespace Car.Inventory
{
    public class GarageController : ControllerBase, IGarageController
    {
        private IRepository<int, IUpgradeCarHandler> upgradeHandlers;
        private IInventoryController inventoryController;
        private IUpgradableCar upgradableCar;
        private IReadOnlyProperty garageOpener;

        public GarageController(IItemUpgradeConfig[] configs, IReadOnlyProperty _garageOpener, IInventoryController _inventoryController, IUpgradableCar _upgradableCar)
        {
            garageOpener = _garageOpener;
            upgradableCar = _upgradableCar;
            upgradeHandlers = new UpgradeHandlersRepository(configs);
            AddController(upgradeHandlers);
            inventoryController = _inventoryController;
            AddController(inventoryController);
            inventoryController.Exit += Exit;
            garageOpener.Subscribe(Enter);
        }

        public void Enter()
        {
            inventoryController.Show();
        }

        public void Exit()
        {
            ApplyEquippedUpgrades(upgradeHandlers);
        }

        protected override void OnDispose()
        {
            inventoryController.Exit -= Exit;
        }

        private void ApplyEquippedUpgrades(IRepository<int, IUpgradeCarHandler> handlers)
        {
            foreach(var upgrade in inventoryController.EquippedItems)
            {
                if(handlers.Items.TryGetValue(upgrade.Id, out var upgradeHandler))
                    upgradeHandler.Upgrade(upgradableCar);
            }
        }
    }
}

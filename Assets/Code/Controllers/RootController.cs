using Car.Inventory;
using Car.Abilities;
using Car.Utilities;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Car
{
    public class RootController : ControllerBase
    {
        private readonly ResourcePath abilitiesViewPath = new ResourcePath("AbilitiesButtons");
        private readonly ResourcePath inventoryViewPath = new ResourcePath("InventoryButtons");

        public void CreateGameControllers(IItemUpgradeConfig[] configs, IAbilityConfig[] abilityConfigs, IAbilityActivator abilityActivator, Transform uiroot, IReadOnlyProperty garageOpener, IReadOnlyProperty abilitiesOpener, IUpgradableCar car)
        {
            var inventoryController = CreateInventoryController(configs, uiroot);
            var garageController = new GarageController(configs, garageOpener, inventoryController, car);
            AddController(garageController);
            var abilitiesController = CreateAbilitiesController(abilityConfigs, abilityActivator, inventoryController, uiroot, abilitiesOpener);
        }

        private IInventoryController CreateInventoryController(IItemUpgradeConfig[] configs, Transform uiroot)
        {
            var inventoryView = LoadAndAdd<IInventoryView>(inventoryViewPath, uiroot);

            var inventoryModel = new InventoryModel();

            var itemsRepository = new ItemsRepository(GetItemConfigs(configs));
            AddController(itemsRepository);

            var inventoryController = new InventoryController(inventoryModel, inventoryView, itemsRepository, uiroot);
            AddController(inventoryController);

            return inventoryController;
        }

        private IAbilitiesController CreateAbilitiesController(IAbilityConfig[] abilityConfigs, IAbilityActivator abilityActivator, IInventoryController inventoryController, Transform uiroot, IReadOnlyProperty abilitiesOpener)
        {
            var abilitiesRepository = new AbilitiesRepository(abilityConfigs);
            AddController(abilitiesRepository);

            var abilitiesView = LoadAndAdd<IAbilitiesView>(abilitiesViewPath, uiroot);

            var abilitiesController = new AbilitiesController(abilitiesRepository, abilityActivator, inventoryController, abilitiesView, abilitiesOpener);
            AddController(abilitiesController);

            return abilitiesController;
        }

        private T LoadAndAdd<T>(ResourcePath path, Transform uiroot)
        {
            var loadedObject = ResourceLoader.Load(path);
            var newObject = UnityEngine.Object.Instantiate(loadedObject, uiroot, false);
            AddObject(newObject);

            return newObject.GetComponent<T>();
        }

        private List<IItemConfig> GetItemConfigs(IItemUpgradeConfig[] configs)
        {
            var result = new List<IItemConfig>(configs.Length);

            foreach(var config in configs)
            {
                result.Add(config.Item);
            }

            return result;
        }
    }
}

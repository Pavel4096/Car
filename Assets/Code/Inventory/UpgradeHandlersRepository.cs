using System.Collections.Generic;

namespace Car.Inventory
{
    public class UpgradeHandlersRepository : ControllerBase, IRepository<int, IUpgradeCarHandler>
    {
        public IReadOnlyDictionary<int, IUpgradeCarHandler> Items => upgradeHandlers;

        private Dictionary<int, IUpgradeCarHandler> upgradeHandlers = new Dictionary<int, IUpgradeCarHandler>();

        public UpgradeHandlersRepository(IItemUpgradeConfig[] configs)
        {
            PopulateItems(upgradeHandlers, configs);
        }

        protected override void OnDispose()
        {
            upgradeHandlers.Clear();
        }

        private void PopulateItems(Dictionary<int, IUpgradeCarHandler> handlers, IItemUpgradeConfig[] configs)
        {
            foreach(var config in configs)
            {
                if(handlers.ContainsKey(config.Id))
                    continue;
                
                handlers.Add(config.Id, CreateHandler(config));
            }
        }

        private IUpgradeCarHandler CreateHandler(IItemUpgradeConfig config)
        {
            switch(config.Type)
            {
                case ItemUpgradeType.Speed:
                    return new SpeedCarUpgrade(config.Value);
                default:
                    return new NoUpgradeHandler();
            }
        }
    }
}

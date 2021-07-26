using System.Collections.Generic;

namespace Car.Inventory
{
    public class UpgradeHandlersRepository : ControllerBase
    {
        public IReadOnlyDictionary<int, IUpgradeCarHandler> UpgradeHandlers => upgradeHandlers;

        private Dictionary<int, IUpgradeCarHandler> upgradeHandlers = new Dictionary<int, IUpgradeCarHandler>();

        public UpgradeHandlersRepository(List<ItemUpgradeConfig> configs)
        {
            PopulateItems(upgradeHandlers, configs);
        }

        protected override void OnDispose()
        {
            upgradeHandlers.Clear();
        }

        private void PopulateItems(Dictionary<int, IUpgradeCarHandler> handlers, List<ItemUpgradeConfig> configs)
        {
            foreach(var config in configs)
            {
                if(handlers.ContainsKey(config.Id))
                    continue;
                
                handlers.Add(config.Id, CreateHandler(config));
            }
        }

        private IUpgradeCarHandler CreateHandler(ItemUpgradeConfig config)
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

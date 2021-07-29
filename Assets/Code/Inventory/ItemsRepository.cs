using System.Collections.Generic;

namespace Car.Inventory
{
    public class ItemsRepository : ControllerBase, IRepository<int, IItem>
    {
        public IReadOnlyDictionary<int, IItem> Items => items;

        private Dictionary<int, IItem> items = new Dictionary<int, IItem>();

        public ItemsRepository(List<IItemConfig> configs) => Populate(items, configs);

        protected override void OnDispose()
        {
            items.Clear();
        }

        private void Populate(Dictionary<int, IItem> items, List<IItemConfig> configs)
        {
            foreach(var config in configs)
            {
                if(items.ContainsKey(config.Id))
                    continue;
                items.Add(config.Id, CreateItem(config));
            }
        }

        private IItem CreateItem(IItemConfig config)
        {
            return new Item
            {
                Id = config.Id,
                Info = new ItemInfo
                {
                    Title = config.Title
                }
            };
        }
    }
}

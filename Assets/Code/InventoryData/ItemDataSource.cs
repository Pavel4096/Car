using System.Collections.Generic;
using UnityEngine;

namespace Car.Inventory
{
    [CreateAssetMenu(fileName = "ItemDataSource", menuName = "Item data source")]
    public class ItemDataSource : ScriptableObject
    {
        public List<ItemUpgradeConfig> configs;
    }
}

using Car.Inventory;
using UnityEngine;

namespace Car
{
    [CreateAssetMenu(fileName = "ItemUpgradeConfig", menuName = "Item upgrade config")]
    public class ItemUpgradeConfig : ScriptableObject, IItemUpgradeConfig
    {
        [SerializeField]
        private ItemConfig item;
        [SerializeField]
        private ItemUpgradeType type;
        [SerializeField]
        private float value;

        public IItemConfig Item => item;
        public int Id => item.Id;
        public ItemUpgradeType Type => type;
        public float Value => value;
    }

    public enum ItemUpgradeType
    {
        None = 0,
        Speed = 1
    }
}

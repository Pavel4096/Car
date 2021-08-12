using Car.Inventory;
using UnityEngine;

namespace Car
{
    [CreateAssetMenu(fileName = "ItemConfig", menuName = "Item config")]
    public class ItemConfig : ScriptableObject, IItemConfig
    {
        [SerializeField]
        private int id;
        [SerializeField]
        private string title;

        public int Id => id;
        public string Title => title;
    }
}

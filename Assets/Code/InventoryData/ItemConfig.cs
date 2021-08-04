using UnityEngine;

namespace Car
{
    [CreateAssetMenu(fileName = "ItemConfig", menuName = "Item config")]
    public class ItemConfig : ScriptableObject
    {
        [SerializeField]
        private int id;
        [SerializeField]
        private string title;

        public int Id => id;
        public string Title => title;
    }
}

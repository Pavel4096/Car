using Car.Abilities;
using UnityEngine;

namespace Car
{
    public class AbilityConfig : ScriptableObject
    {
        public ItemConfig item;
        public AbilityType type;
        public GameObject view;
        public float value;
        public int Id => item.Id;
    }
}

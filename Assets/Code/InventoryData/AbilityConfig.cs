using Car.Inventory;
using Car.Abilities;
using UnityEngine;

namespace Car
{
    [CreateAssetMenu(fileName = "AbilityConfig", menuName = "Ability config")]
    public class AbilityConfig : ScriptableObject, IAbilityConfig
    {
        public ItemConfig item;
        public AbilityType type;
        public GameObject view;
        public float value;

        public IItemConfig Item => item;
        public int Id => item.Id;
        public AbilityType Type => type;
        public GameObject View => view;
        public float Value => value;
    }
}

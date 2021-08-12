using Car.Inventory;
using UnityEngine;

namespace Car.Abilities
{
    public interface IAbilityConfig
    {
        IItemConfig Item { get; }
        int Id { get; }
        AbilityType Type { get; }
        GameObject View { get; }
        float Value { get; }
    }
}

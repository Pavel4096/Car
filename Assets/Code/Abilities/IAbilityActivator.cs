using UnityEngine;

namespace Car.Abilities
{
    public interface IAbilityActivator
    {
        GameObject GetViewObject();
        PlayerProfile GetPlayerProfile();
    }
}

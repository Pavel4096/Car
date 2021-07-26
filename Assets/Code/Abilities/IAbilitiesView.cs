using Car.Inventory;
using System;
using System.Collections.Generic;

namespace Car.Abilities
{
    public interface IAbilitiesView
    {
        event Action<IAbility> Selected;
        void Display(IReadOnlyList<IItem> items, IAbilityRepository abilityRepository);
        void Dispose();
    }
}

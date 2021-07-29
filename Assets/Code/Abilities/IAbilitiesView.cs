using Car.Inventory;
using System;
using System.Collections.Generic;

namespace Car.Abilities
{
    public interface IAbilitiesView
    {
        bool IsDisplayed { get; }
        event Action<IAbility> Selected;
        void Display(IReadOnlyList<IItem> items, IRepository<int, IAbility> abilityRepository);
        void Hide();
        void Dispose();
    }
}

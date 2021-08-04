using System.Collections.Generic;

namespace Car.Abilities
{
    public interface IAbilityRepository
    {
        IReadOnlyDictionary<int, IAbility> Abilities { get; }
    }
}

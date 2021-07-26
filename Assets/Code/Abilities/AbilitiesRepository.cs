using System.Collections.Generic;

namespace Car.Abilities
{
    public class AbilitiesRepository : ControllerBase, IAbilityRepository
    {
        public IReadOnlyDictionary<int, IAbility> Abilities => abilities;

        private Dictionary<int, IAbility> abilities = new Dictionary<int, IAbility>();

        public AbilitiesRepository(List<AbilityConfig> configs) => Populate(abilities, configs);

        protected override void OnDispose()
        {
            abilities.Clear();
        }

        private void Populate(Dictionary<int, IAbility> abilities, List<AbilityConfig> configs)
        {
            foreach(var config in configs)
            {
                if(abilities.ContainsKey(config.Id))
                    continue;
                abilities.Add(config.Id, CreateAbility(config));
            }
        }

        private IAbility CreateAbility(AbilityConfig config)
        {
            switch(config.type)
            {
                case AbilityType.Speedup:
                    return new SpeedupAbility(config.value);
                default:
                    return new NoAbility();
            }
        }
    }
}

using System.Collections.Generic;

namespace Car.Abilities
{
    public class AbilitiesRepository : ControllerBase, IRepository<int, IAbility>
    {
        public IReadOnlyDictionary<int, IAbility> Items => abilities;

        private Dictionary<int, IAbility> abilities = new Dictionary<int, IAbility>();

        public AbilitiesRepository(IAbilityConfig[] configs) => Populate(abilities, configs);

        protected override void OnDispose()
        {
            abilities.Clear();
        }

        private void Populate(Dictionary<int, IAbility> abilities, IAbilityConfig[] configs)
        {
            foreach(var config in configs)
            {
                if(abilities.ContainsKey(config.Id))
                    continue;
                abilities.Add(config.Id, CreateAbility(config));
            }
        }

        private IAbility CreateAbility(IAbilityConfig config)
        {
            switch(config.Type)
            {
                case AbilityType.Speedup:
                    return new SpeedupAbility(config.Value);
                case AbilityType.Grenade:
                    return new GrenadeAbility(config.Value);
                default:
                    return new NoAbility();
            }
        }
    }
}

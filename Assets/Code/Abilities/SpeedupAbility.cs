namespace Car.Abilities
{
    public class SpeedupAbility : IAbility
    {
        private float speedMultiplier;

        public SpeedupAbility(float _speedMultiplier)
        {
            speedMultiplier = _speedMultiplier;
        }

        public void Apply(IAbilityActivator activator)
        {
            activator.GetPlayerProfile().Car.SpeedMultiplier = speedMultiplier;
        }
    }
}

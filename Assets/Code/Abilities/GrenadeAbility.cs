using Car.Utilities;
using UnityEngine;

namespace Car.Abilities
{
    public class GrenadeAbility : IAbility
    {
        private readonly ResourcePath grenadePath = new ResourcePath("Grenade");
        private GameObject grenade;
        private float force;

        public GrenadeAbility(float _force)
        {
            force = _force;
            grenade = ResourceLoader.Load(grenadePath);
        }

        public void Apply(IAbilityActivator activator)
        {
            var currentGrenade = Object.Instantiate(grenade);

            currentGrenade.GetComponent<Rigidbody2D>().AddForce(activator.GetViewObject().transform.right*force, ForceMode2D.Impulse);
        }
    }
}

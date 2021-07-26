using Car.Utilities;
using Car.Abilities;
using UnityEngine;

namespace Car
{
    internal sealed class CarController : ControllerBase, IAbilityActivator
    {
        private CarView carView;
        private PlayerProfile playerProfile;
        private readonly ResourcePath carViewPath = new ResourcePath("Car");

        public CarController(PlayerProfile _playerProfile)
        {
            playerProfile = _playerProfile;
            carView = LoadView();
        }

        public GameObject GetViewObject()
        {
            return carView.gameObject;
        }

        public PlayerProfile GetPlayerProfile()
        {
            return playerProfile;
        }

        private CarView LoadView()
        {
            var carViewObject = Object.Instantiate(ResourceLoader.Load(carViewPath));

            AddObject(carViewObject);

            return carViewObject.GetComponent<CarView>();
        }
    }
}

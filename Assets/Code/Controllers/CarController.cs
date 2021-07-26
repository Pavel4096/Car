using Car.Utilities;
using UnityEngine;

namespace Car
{
    internal sealed class CarController : ControllerBase
    {
        private CarView carView;
        private PlayerProfile playerProfile;
        private readonly ResourcePath carViewPath = new ResourcePath("Car");

        public CarController(PlayerProfile _playerProfile)
        {
            playerProfile = _playerProfile;
            carView = LoadView();
        }

        private CarView LoadView()
        {
            var carViewObject = Object.Instantiate(ResourceLoader.Load(carViewPath));

            AddObject(carViewObject);

            return carViewObject.GetComponent<CarView>();
        }
    }
}

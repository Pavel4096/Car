namespace Car.Inventory
{
    public class SpeedCarUpgrade : IUpgradeCarHandler
    {
        private readonly float speed;

        public SpeedCarUpgrade(float _speed)
        {
            speed = _speed;
        }

        public IUpgradableCar Upgrade(IUpgradableCar upgradableCar)
        {
            upgradableCar.Speed = speed;

            return upgradableCar;
        }
    }
}

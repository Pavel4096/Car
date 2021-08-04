namespace Car.Inventory
{
    public class NoUpgradeHandler : IUpgradeCarHandler
    {
        public IUpgradableCar Upgrade(IUpgradableCar upgradableCar) => upgradableCar;
    }
}

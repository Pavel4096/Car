namespace Car.Inventory
{
    public interface IUpgradeCarHandler
    {
        IUpgradableCar Upgrade(IUpgradableCar upgradableCar);
    }
}

namespace Car.Inventory
{
    public interface IItemUpgradeConfig
    {
        IItemConfig Item { get; }
        int Id { get; }
        ItemUpgradeType Type { get; }
        float Value { get; }
    }
}

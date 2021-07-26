namespace Car.Inventory
{
    public interface IItem
    {
        int Id { get; }
        ItemInfo Info { get; }
    }
}

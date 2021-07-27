namespace Car
{
    public interface IUpgradableCar
    {
        float Speed { get; set; }
        float SpeedMultiplier { get; set; }
        void Reset();
    }
}

namespace Car
{
    public class Car : IUpgradableCar
    {
        public float Speed { get; set; }
        public float SpeedMultiplier { get; set; }

        private readonly float defaultSpeed;

        private const float defaultSpeedMultiplier = 1.0f;

        public Car(float _speed)
        {
            defaultSpeed = _speed;
            Reset();
        }

        public void Reset()
        {
            Speed = defaultSpeed;
            SpeedMultiplier = defaultSpeedMultiplier;
        }
    }
}

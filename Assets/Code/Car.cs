namespace Car
{
    public class Car : IUpgradableCar
    {
        public float Speed { get; set; }

        private readonly float defaultSpeed;

        public Car(float _speed)
        {
            defaultSpeed = _speed;
            Reset();
        }

        public void Reset()
        {
            Speed = defaultSpeed;
        }
    }
}

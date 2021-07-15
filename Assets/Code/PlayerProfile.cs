namespace Car
{
    internal sealed class PlayerProfile
    {
        public Car Car { get; }
        public Property<GameState> GameState { get; }

        public PlayerProfile(Car car, Property<GameState> gameState)
        {
            Car = car;
            GameState = gameState;
        }
    }
}

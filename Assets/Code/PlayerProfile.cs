namespace Car
{
    public class PlayerProfile
    {
        public Car Car { get; }
        public float SpeedMultiplier { get; set; }
        public Property<GameState> GameState { get; }
        public IAnalytics AnalyticsUtility { get; }
        public IAds AdsUtility { get; }

        public PlayerProfile(Car car, Property<GameState> gameState, IAnalytics _analyticsUtility, IAds _adsUtility)
        {
            Car = car;
            SpeedMultiplier = 1.0f;
            GameState = gameState;
            AnalyticsUtility = _analyticsUtility;
            AdsUtility = _adsUtility;
        }
    }
}

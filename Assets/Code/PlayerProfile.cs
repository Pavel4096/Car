namespace Car
{
    internal sealed class PlayerProfile
    {
        public Car Car { get; }
        public Property<GameState> GameState { get; }
        public IAnalytics AnalyticsUtility { get; }
        public IAds AdsUtility { get; }

        public PlayerProfile(Car car, Property<GameState> gameState, IAnalytics _analyticsUtility, IAds _adsUtility)
        {
            Car = car;
            GameState = gameState;
            AnalyticsUtility = _analyticsUtility;
            AdsUtility = _adsUtility;
        }
    }
}

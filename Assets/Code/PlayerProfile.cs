using System;
using Car.Notifications;

namespace Car
{
    public class PlayerProfile
    {
        public Car Car { get; }
        public float SpeedMultiplier { get; set; }
        public Property<GameState> GameState { get; }
        public IAnalytics AnalyticsUtility { get; }
        public IAds AdsUtility { get; }
        public INotificationUtility NotificationUtility { get; }
        public int? CurrentRewardsNotificationIdentifier { get; set; }

        public PlayerProfile(Car car, Property<GameState> gameState, IAnalytics analyticsUtility, IAds adsUtility, INotificationUtility notificationUtility)
        {
            Car = car;
            SpeedMultiplier = 1.0f;
            GameState = gameState;
            AnalyticsUtility = analyticsUtility;
            AdsUtility = adsUtility;
            NotificationUtility = notificationUtility;
        }
    }
}

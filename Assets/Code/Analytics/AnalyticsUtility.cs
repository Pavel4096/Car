using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

namespace Car
{
    public class AnalyticsUtility : IAnalytics
    {
        private Dictionary<string, object> parameters = new Dictionary<string, object>();
        private const string gameStartTimeEventName = "GameStartTime";
        private const string gameStartTimeParameterName = "Time";
        private const string menuEnteredEventName = "MenuEntered";
        private const string menuEnteredParameterName = "MenuName";

        public void GameStartTime()
        {
#if ENABLE_CLOUD_SERVICES_ANALYTICS
            parameters.Clear();
            parameters.Add(gameStartTimeParameterName, Time.realtimeSinceStartup);
            Analytics.CustomEvent(gameStartTimeEventName, parameters);

#endif
        }

        public void MenuEntered(string menuName)
        {
#if ENABLE_CLOUD_SERVICES_ANALYTICS
            AnalyticsEvent.ScreenVisit(menuName);
#endif
        }

        public void Send(string name, params (string name, object value)[] eventParameters)
        {
#if ENABLE_CLOUD_SERVICES_ANALYTICS
            parameters.Clear();
            foreach(var parameter in eventParameters)
            {
                parameters.Add(parameter.name, parameter.value);
            }
            Analytics.CustomEvent(name, parameters);
#endif
        }
    }
}

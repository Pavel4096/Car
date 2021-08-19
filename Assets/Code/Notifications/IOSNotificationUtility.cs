using System;
using System.Collections.Generic;
using Unity.Notifications.iOS;

namespace Car.Notifications
{
    public class IOSNotificationUtility : INotificationUtility, IDisposable
    {
        private Dictionary<int, string> _identifiers = new Dictionary<int, string>();
        private int nextIdentifier;

        public IOSNotificationUtility()
        {
            iOSNotificationCenter.OnNotificationReceived += NotificationReceivedHandler;
        }

        public int Send(NotificationType notificationType, string title, string text, DateTime date, TimeSpan? repeatInterval = null)
        {
            int identifier;

            var notification = new iOSNotification {
                Title = title,
                Body = text,
            };

            if(repeatInterval.HasValue)
            {
                var trigger = new iOSNotificationTimeIntervalTrigger();
                trigger.Repeats = true;
                trigger.TimeInterval = repeatInterval.Value;
                notification.Trigger = trigger;
            }
            else
            {
                var trigger = new iOSNotificationCalendarTrigger();
                trigger.Year = date.Year;
                trigger.Month = date.Month;
                trigger.Day = date.Day;
                trigger.Hour = date.Hour;
                trigger.Minute = date.Minute;
                trigger.Second = date.Second;
                notification.Trigger = trigger;
            }

            identifier = AddIdentifier(notification.Identifier);
            iOSNotificationCenter.ScheduleNotification(notification);

            return identifier;
        }

        public void RemoveNotification(int notificationIdentifier)
        {
            if(_identifiers.TryGetValue(notificationIdentifier, out var identifier))
            {
                iOSNotificationCenter.RemoveDeliveredNotification(identifier);
                iOSNotificationCenter.RemoveScheduledNotification(identifier);
                _identifiers.Remove(notificationIdentifier);
            }
        }

        public void Dispose()
        {
            iOSNotificationCenter.OnNotificationReceived -= NotificationReceivedHandler;
        }

        private int AddIdentifier(string identifier)
        {
            _identifiers.Add(nextIdentifier, identifier);
            return nextIdentifier++;
        }

        private void NotificationReceivedHandler(iOSNotification notification)
        {
            foreach(var identifier in _identifiers)
            {
                if(identifier.Value == notification.Identifier)
                {
                    RemoveNotification(identifier.Key);
                    break;
                }
            }
        }
    }
}

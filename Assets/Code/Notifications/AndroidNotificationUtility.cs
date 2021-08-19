using System;
using System.Collections.Generic;
using Unity.Notifications.Android;

namespace Car.Notifications
{
    public class AndroidNotificationUtility : INotificationUtility, IDisposable
    {
        private Dictionary<NotificationType, AndroidNotificationChannel> _channels = new Dictionary<NotificationType, AndroidNotificationChannel>();

        public AndroidNotificationUtility()
        {
            CreateChannels();
            AndroidNotificationCenter.OnNotificationReceived += NotificationReceivedHandler;
        }

        public int Send(NotificationType notificationType, string title, string text, DateTime date, TimeSpan? repeatInterval = null)
        {
            AndroidNotification notification;
            AndroidNotificationChannel notificationChannel;

            if(!_channels.TryGetValue(notificationType, out notificationChannel))
            {
                throw new ArgumentException("Specified notification type is not valid.");
            }

            notification = new AndroidNotification {
                Title = title,
                Text = text,
                FireTime = date,
                RepeatInterval = repeatInterval
            };
            
            return AndroidNotificationCenter.SendNotification(notification, notificationChannel.Id);
        }

        public void RemoveNotification(int notificationIdentifier)
        {
            AndroidNotificationCenter.CancelNotification(notificationIdentifier);
        }

        public void Dispose()
        {
            AndroidNotificationCenter.OnNotificationReceived -= NotificationReceivedHandler;
        }

        private void CreateChannels()
        {
            var currentChannel = new AndroidNotificationChannel {
                Name = "Ordinary",
                Id = "Ornidary",
                Description = "Ordinary channel",
                LockScreenVisibility = LockScreenVisibility.Public,
                Importance = Importance.Low
            };

            AndroidNotificationCenter.RegisterNotificationChannel(currentChannel);
            _channels.Add(NotificationType.Ordinary, currentChannel);

            currentChannel = new AndroidNotificationChannel {
                Name = "Special",
                Id = "Special",
                Description = "Special channel",
                LockScreenVisibility = LockScreenVisibility.Public,
                Importance = Importance.Default
            };
            AndroidNotificationCenter.RegisterNotificationChannel(currentChannel);
            _channels.Add(NotificationType.Special, currentChannel);
        }

        private void NotificationReceivedHandler(AndroidNotificationIntentData data)
        {
            RemoveNotification(data.Id);
        }
    }
}

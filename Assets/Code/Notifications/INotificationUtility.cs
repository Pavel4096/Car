using System;

namespace Car.Notifications
{
    public interface INotificationUtility
    {
        int Send(NotificationType notificationType, string title, string text, DateTime date, TimeSpan? repeatInterval = null);
        void RemoveNotification(int notificationIdentifier);
    }
}

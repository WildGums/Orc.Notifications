namespace Orc.Notifications
{
    using System;

    public class NotificationEventArgs : EventArgs
    {
        public NotificationEventArgs(INotification notification)
        {
            ArgumentNullException.ThrowIfNull(notification);

            Notification = notification;
        }

        public INotification Notification { get; private set; }
    }
}

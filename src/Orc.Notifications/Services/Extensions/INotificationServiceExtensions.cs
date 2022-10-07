namespace Orc.Notifications
{
    using System;

    public static class INotificationServiceExtensions
    {
        public static void ShowNotification(this INotificationService notificationService, string title, string message)
        {
            ArgumentNullException.ThrowIfNull(notificationService);

            var notification = new Notification
            {
                Title = title,
                Message = message
            };

            notificationService.ShowNotification(notification);
        }

        public static void ShowWarningNotification(this INotificationService notificationService, string title, string message)
        {
            ArgumentNullException.ThrowIfNull(notificationService);

            var notification = new WarningNotification
            {
                Title = title,
                Message = message
            };

            notificationService.ShowNotification(notification);
        }

        public static void ShowErrorNotification(this INotificationService notificationService, string title, string message)
        {
            ArgumentNullException.ThrowIfNull(notificationService);

            var notification = new ErrorNotification
            {
                Title = title,
                Message = message
            };

            notificationService.ShowNotification(notification);
        }
    }
}

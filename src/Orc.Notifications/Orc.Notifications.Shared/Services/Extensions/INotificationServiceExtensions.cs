// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INotificationServiceExtensions.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Notifications
{
    using Catel;

    public static class INotificationServiceExtensions
    {
        public static void ShowNotification(this INotificationService notificationService, string title, string message)
        {
            Argument.IsNotNull(() => notificationService);

            var notification = new Notification
            {
                Title = title,
                Message = message
            };

            notificationService.ShowNotification(notification);
        }
    }
}
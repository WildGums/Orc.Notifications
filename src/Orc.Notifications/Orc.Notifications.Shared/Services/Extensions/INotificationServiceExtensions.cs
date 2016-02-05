﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INotificationServiceExtensions.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
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
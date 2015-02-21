// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationEventArgs.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Notifications
{
    using System;

    public class NotificationEventArgs : EventArgs
    {
        public NotificationEventArgs(INotification notification)
        {
            Notification = notification;
        }

        public INotification Notification { get; private set; }
    }
}
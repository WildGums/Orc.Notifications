// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationEventArgs.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
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
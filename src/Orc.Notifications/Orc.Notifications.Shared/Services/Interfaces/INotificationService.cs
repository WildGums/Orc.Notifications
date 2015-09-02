// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INotificationService.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Notifications
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Media;

    public interface INotificationService
    {        
        SolidColorBrush DefaultBorderBrush { get; set; }
        SolidColorBrush DefaultBackgroundBrush { get; set; }
        SolidColorBrush DefaultFontBrush { get; set; }
        ObservableCollection<INotification> CurrentNotifications { get; }
        bool IsSuspended { get; }

        event EventHandler<NotificationEventArgs> OpenedNotification;
        event EventHandler<NotificationEventArgs> ClosedNotification;

        void Suspend();
        void Resume();
        void ShowNotification(INotification notification);
    }
}
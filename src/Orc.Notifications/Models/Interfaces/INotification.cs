// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INotification.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Notifications
{
    using System;
    using System.Windows.Input;
    using System.Windows.Media;

    public interface INotification
    {
        string Title { get; set; }

        string Message { get; set; }

        ICommand Command { get; set; }

        TimeSpan ShowTime { get; set; }

        SolidColorBrush BorderBrush { get; set; }

        SolidColorBrush BackgroundBrush { get; set; }

        SolidColorBrush FontBrush { get; set; }

        bool IsClosable { get; set; }

        NotificationPriority Priority { get; set; }
    }
}
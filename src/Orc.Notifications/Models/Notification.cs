﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Notification.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Notifications
{
    using System;
    using System.Windows.Input;
    using System.Windows.Media;
    using Catel;

    public class Notification : INotification
    {
        public Notification()
        {
            Id = UniqueIdentifierHelper.GetUniqueIdentifier<Notification>();
            ShowTime = TimeSpan.FromSeconds(5);
            IsClosable = true;
            Priority = NotificationPriority.Normal;
            Level = NotificationLevel.Normal;
        }

        public int Id { get; private set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public TimeSpan ShowTime { get; set; }

        public ICommand Command { get; set; }

        public SolidColorBrush BorderBrush { get; set; }

        public SolidColorBrush BackgroundBrush { get; set; }

        public SolidColorBrush FontBrush { get; set; }

        public bool IsClosable { get; set; }

        public NotificationPriority Priority { get; set; }

        public NotificationLevel Level { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Notification.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Notifications
{
    using System;
    using System.Windows.Input;
    using System.Windows.Media;

    public class Notification : INotification
    {
        public Notification()
        {
            ShowTime = TimeSpan.FromSeconds(4);
        }

        public string Title { get; set; }

        public string Message { get; set; }

        public TimeSpan ShowTime { get; set; }

        public ICommand Command { get; set; }

        public SolidColorBrush BorderBrush { get; set; }

        public SolidColorBrush BackgroundBrush { get; set; }

        public SolidColorBrush FontBrush { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
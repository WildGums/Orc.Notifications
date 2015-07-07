// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Notification.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Notifications
{
    using System;
    using System.ComponentModel;
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

        public override string ToString()
        {
            return Title;
        }
    }
}
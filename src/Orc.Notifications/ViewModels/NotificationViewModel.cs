// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationViewModel.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Notifications
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;
    using Catel;
    using Catel.MVVM;

    public class NotificationViewModel : ViewModelBase
    {
        private DispatcherTimer _dispatcherTimer;

        public NotificationViewModel(INotification notification, INotificationService notificationService)
        {
            Argument.IsNotNull(() => notification);

            Notification = notification;
            Title = notification.Title;
            Message = notification.Message;
            Command = notification.Command;
            ShowTime = notification.ShowTime;

            BorderBrush = notification.BorderBrush ?? notificationService.DefaultBorderBrush;
            BackgroundBrush = notification.BackgroundBrush ?? notificationService.DefaultBackgroundBrush;
            FontBrush = notification.FontBrush ?? notificationService.DefaultFontBrush;
        }

        #region Properties
        public INotification Notification { get; private set; }

        public string Message { get; private set; }

        public ICommand Command { get; private set; }

        public TimeSpan ShowTime { get; private set; }

        public SolidColorBrush BorderBrush { get; private set; }

        public SolidColorBrush BackgroundBrush { get; private set; }

        public SolidColorBrush FontBrush { get; private set; }
        #endregion

        #region Methods
        protected override async Task Initialize()
        {
            await base.Initialize();

            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = ShowTime;
            _dispatcherTimer.Tick += OnDispatcherTimerTick;
            _dispatcherTimer.Start();
        }

        protected override async Task Close()
        {
            _dispatcherTimer.Stop();
            _dispatcherTimer.Tick -= OnDispatcherTimerTick;
            _dispatcherTimer = null;

            await base.Close();
        }

        private async void OnDispatcherTimerTick(object sender, EventArgs e)
        {
            await SaveAndCloseViewModel();
        }
        #endregion
    }
}
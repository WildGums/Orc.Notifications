// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationService.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Notifications
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media;
    using Catel;
    using Catel.Logging;
    using Catel.Services;

    public class NotificationService : INotificationService
    {
        #region Fields
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly IUIVisualizerService _uiVisualizerService;
        private readonly IDispatcherService _dispatcherService;

        private readonly Queue<INotification> _notificationsQueue = new Queue<INotification>(); 
        #endregion

        #region Constructors
        public NotificationService(IUIVisualizerService uiVisualizerService, IDispatcherService dispatcherService)
        {
            Argument.IsNotNull(() => uiVisualizerService);
            Argument.IsNotNull(() => dispatcherService);

            _uiVisualizerService = uiVisualizerService;
            _dispatcherService = dispatcherService;

            CurrentNotifications = new ObservableCollection<INotification>();

            DefaultBorderBrush = Brushes.Black;
            DefaultBackgroundBrush = Brushes.DodgerBlue;
            DefaultFontBrush = Brushes.WhiteSmoke;

            var app = Application.Current;
            if (app != null)
            {
                var accentColorBrush = app.TryFindResource("AccentColorBrush") as SolidColorBrush;
                if (accentColorBrush != null)
                {

                    DefaultBorderBrush = accentColorBrush;
                    DefaultBackgroundBrush = new SolidColorBrush(Color.FromArgb(255, 245, 245, 245));
                    DefaultFontBrush = Brushes.Black;
                    accentColorBrush.Color.CreateAccentColorResourceDictionary();
                }
            }
        }
        #endregion

        #region Properties
        public ObservableCollection<INotification> CurrentNotifications { get; private set; }

        public SolidColorBrush DefaultBorderBrush { get; set; }

        public SolidColorBrush DefaultBackgroundBrush { get; set; }

        public SolidColorBrush DefaultFontBrush { get; set; }

        public bool IsSuspended { get; private set; }
        #endregion

        #region Events
        public event EventHandler<NotificationEventArgs> OpenedNotification;

        public event EventHandler<NotificationEventArgs> ClosedNotification;
        #endregion

        #region Methods
        public void Suspend()
        {
            IsSuspended = true;
        }

        public async Task Resume()
        {
            IsSuspended = false;

            while (_notificationsQueue.Count > 0)
            {
                var notification = _notificationsQueue.Dequeue();
                await ShowNotification(notification);
            }
        }

        public async Task ShowNotification(INotification notification)
        {
            Argument.IsNotNull(() => notification);

            if (IsSuspended)
            {
                Log.Debug("Notifications are suspended, queueing notification");

                _notificationsQueue.Enqueue(notification);

                return;
            }

            _dispatcherService.BeginInvoke(() =>
            {
                Log.Debug("Showing notification '{0}'", notification);

                _uiVisualizerService.Show<NotificationViewModel>(notification, OnNotificationClosed);

                OpenedNotification.SafeInvoke(this, new NotificationEventArgs(notification));

                CurrentNotifications.Add(notification);
            });
        }

        private void OnNotificationClosed(object sender, UICompletedEventArgs e)
        {
            var notification = e.DataContext as INotification;
            if (notification == null)
            {
                var notificationViewModel = e.DataContext as NotificationViewModel;
                if (notificationViewModel != null)
                {
                    notification = notificationViewModel.Notification;
                }
            }

            if (notification != null)
            {
                CurrentNotifications.Remove(notification);

                ClosedNotification.SafeInvoke(this, new NotificationEventArgs(notification));
            }
        }
        #endregion
    }
}
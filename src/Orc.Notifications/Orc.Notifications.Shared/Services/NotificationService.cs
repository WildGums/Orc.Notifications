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
    using System.ComponentModel;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using Catel;
    using Catel.Logging;
    using Catel.MVVM;
    using Catel.Services;
    using Size = System.Drawing.Size;

    public class NotificationService : INotificationService
    {
        #region Fields
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private static readonly Size NotificationSize = new Size(Orc.Notifications.NotificationSize.Width, Orc.Notifications.NotificationSize.Height);

        private readonly IViewModelFactory _viewModelFactory;
        private readonly IDispatcherService _dispatcherService;
        private readonly INotificationPositionService _notificationPositionService;

        private readonly Queue<INotification> _notificationsQueue = new Queue<INotification>();

        private Window _mainWindow;
        #endregion

        #region Constructors
        public NotificationService(IViewModelFactory viewModelFactory, IDispatcherService dispatcherService,
            INotificationPositionService notificationPositionService)
        {
            Argument.IsNotNull(() => viewModelFactory);
            Argument.IsNotNull(() => dispatcherService);
            Argument.IsNotNull(() => notificationPositionService);

            _viewModelFactory = viewModelFactory;
            _dispatcherService = dispatcherService;
            _notificationPositionService = notificationPositionService;

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

                _mainWindow = app.MainWindow;
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

            EnsureMainWindow();

            _dispatcherService.BeginInvoke(() =>
            {
                Log.Debug("Showing notification '{0}'", notification);

                var notificationLocation = _notificationPositionService.GetLeftTopCorner(NotificationSize, CurrentNotifications.Count);

                var popup = new Popup();

                popup.AllowsTransparency = true;
                popup.Placement = PlacementMode.AbsolutePoint;
                popup.PlacementRectangle = new Rect(notificationLocation.X, notificationLocation.Y, NotificationSize.Width, NotificationSize.Height);

                var notificationViewModel = _viewModelFactory.CreateViewModel<NotificationViewModel>(notification);
                notificationViewModel.Closed += (sender, e) => popup.IsOpen = false;

                // TODO: consider factory
                var notificationView = new NotificationView();
                notificationView.DataContext = notificationViewModel;
                notificationView.Unloaded += OnNotificationViewUnloaded;

                popup.Child = notificationView;

                popup.IsOpen = true;

                OpenedNotification.SafeInvoke(this, new NotificationEventArgs(notification));

                CurrentNotifications.Add(notification);
            });
        }

        private void EnsureMainWindow()
        {
            if (_mainWindow != null)
            {
                return;
            }

            var application = Application.Current;
            if (application != null)
            {
                _mainWindow = application.MainWindow;
                if (_mainWindow != null)
                {
                    _mainWindow.Closing += OnMainWindowClosing;
                }
            }
        }

        private void OnNotificationViewUnloaded(object sender, EventArgs e)
        {
            var notificationControl = sender as NotificationView;
            if (notificationControl == null)
            {
                return;
            }

            var notification = notificationControl.DataContext as INotification;
            if (notification == null)
            {
                var notificationViewModel = notificationControl.DataContext as NotificationViewModel;
                if (notificationViewModel == null)
                {
                    notificationViewModel = notificationControl.ViewModel as NotificationViewModel;
                }

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

        private void OnMainWindowClosing(object sender, CancelEventArgs e)
        {
            // todo: close main window
        }
        #endregion
    }
}
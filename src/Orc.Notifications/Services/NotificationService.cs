namespace Orc.Notifications
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    using Catel;
    using Catel.Logging;
    using Catel.MVVM;
    using Catel.Services;

    using Size = System.Drawing.Size;
    using Window = System.Windows.Window;

    public class NotificationService : INotificationService
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private static readonly Size NotificationSize = new Size(Orc.Notifications.NotificationSize.Width, Orc.Notifications.NotificationSize.Height);

        private readonly IViewModelFactory _viewModelFactory;

        private readonly IDispatcherService _dispatcherService;

        private readonly INotificationPositionService _notificationPositionService;

        private readonly Queue<INotification> _notificationsQueue = new Queue<INotification>();

        private Window? _mainWindow;
 
        public NotificationService(IViewModelFactory viewModelFactory, IDispatcherService dispatcherService, INotificationPositionService notificationPositionService)
        {
            ArgumentNullException.ThrowIfNull(viewModelFactory);
            ArgumentNullException.ThrowIfNull(dispatcherService);
            ArgumentNullException.ThrowIfNull(notificationPositionService);

            _viewModelFactory = viewModelFactory;
            _dispatcherService = dispatcherService;
            _notificationPositionService = notificationPositionService;

            CurrentNotifications = new ObservableCollection<INotification>();

            DefaultBorderBrush = Brushes.Black;
            DefaultBackgroundBrush = Brushes.DodgerBlue;
            DefaultFontBrush = Brushes.WhiteSmoke;

            var app = Application.Current;
            if (app is not null)
            {
                var accentColorBrush = app.TryFindResource("AccentColorBrush") as SolidColorBrush;
                if (accentColorBrush is not null)
                {
                    DefaultBorderBrush = accentColorBrush;
                    DefaultBackgroundBrush = new SolidColorBrush(Color.FromArgb(255, 245, 245, 245));
                    DefaultFontBrush = Brushes.Black;
                    accentColorBrush.Color.CreateAccentColorResourceDictionary();
                }

                _mainWindow = app.MainWindow;
            }
        }

        public ObservableCollection<INotification> CurrentNotifications { get; private set; }

        public SolidColorBrush DefaultBorderBrush { get; set; }

        public SolidColorBrush DefaultBackgroundBrush { get; set; }

        public SolidColorBrush DefaultFontBrush { get; set; }

        public bool IsSuspended { get; private set; }
        
        public event EventHandler<NotificationEventArgs>? OpenedNotification;

        public event EventHandler<NotificationEventArgs>? ClosedNotification;

        public void Suspend()
        {
            IsSuspended = true;
        }

        public void Resume()
        {
            IsSuspended = false;

            while (_notificationsQueue.Count > 0)
            {
                var notification = _notificationsQueue.Dequeue();
                ShowNotification(notification);
            }
        }

        public void ShowNotification(INotification notification)
        {
            ArgumentNullException.ThrowIfNull(notification);

            if (IsSuspended)
            {
                Log.Debug("Notifications are suspended, queueing notification");

                _notificationsQueue.Enqueue(notification);

                return;
            }

            _dispatcherService.BeginInvoke(() =>
            {
                EnsureMainWindow();

                var hasActiveWindows = HasActiveWindows();
                if (!hasActiveWindows && notification.Priority <= NotificationPriority.Normal)
                {
                    Log.Debug($"Not showing notification '{notification}' since priority is '{notification.Priority}' and app has no active windows.");
                    return;
                }

                Log.Debug($"Showing notification '{notification}'");

                var notificationLocation = _notificationPositionService.GetLeftTopCorner(NotificationSize, CurrentNotifications.Count);

                var popup = new Popup();

                popup.AllowsTransparency = true;
                popup.Placement = PlacementMode.Custom;
                popup.CustomPopupPlacementCallback += (popupSize, targetSize, offset) =>
                {
                    var x = DpiHelper.CalculateSize(DpiHelper.DpiX, notificationLocation.X);
                    var y = DpiHelper.CalculateSize(DpiHelper.DpiY, notificationLocation.Y);

                    var popupPlacement = new CustomPopupPlacement(new Point(x, y), PopupPrimaryAxis.None);

                    var ttplaces = new[] { popupPlacement };
                    return ttplaces;
                };

                var notificationViewModel = _viewModelFactory.CreateRequiredViewModel<NotificationViewModel>(notification, null);
                notificationViewModel.ClosedAsync += async (sender, e) =>
                {
                    Log.Debug($"Hiding notification '{notification}'");

                    popup.IsOpen = false;
                };

                var notificationView = new NotificationView();
                notificationView.DataContext = notificationViewModel;
                notificationView.Unloaded += OnNotificationViewUnloaded;

                popup.Child = notificationView;

                popup.IsOpen = true;

                OpenedNotification?.Invoke(this, new NotificationEventArgs(notification));

                CurrentNotifications.Add(notification);
            });
        }

        protected virtual bool HasActiveWindows()
        {
            var hasActiveWindows = Application.Current.Windows.OfType<Window>().FirstOrDefault(window => window.IsActive) is not null;
            return hasActiveWindows;
        }

        private void EnsureMainWindow()
        {
            if (_mainWindow is not null)
            {
                return;
            }

            var application = Application.Current;
            if (application is not null)
            {
                _mainWindow = application.MainWindow;
            }
        }

        private void OnNotificationViewUnloaded(object sender, EventArgs e)
        {
            var notificationView = sender as NotificationView;
            if (notificationView is null)
            {
                return;
            }

            notificationView.Unloaded -= OnNotificationViewUnloaded;

            var notification = notificationView.DataContext as INotification;
            if (notification is null)
            {
                var notificationViewModel = notificationView.DataContext as NotificationViewModel;
                if (notificationViewModel is null)
                {
                    notificationViewModel = notificationView.ViewModel as NotificationViewModel;
                }

                if (notificationViewModel is not null)
                {
                    notification = notificationViewModel.Notification;
                }
            }

            if (notification is not null)
            {
                CurrentNotifications.Remove(notification);

                ClosedNotification?.Invoke(this, new NotificationEventArgs(notification));
            }
        }
    }
}

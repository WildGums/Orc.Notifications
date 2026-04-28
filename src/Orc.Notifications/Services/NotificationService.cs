namespace Orc.Notifications;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using Catel.Logging;
using Catel.MVVM;
using Catel.Services;
using Microsoft.Extensions.Logging;
using Size = System.Drawing.Size;

public class NotificationService : INotificationService
{
    private static readonly Size NotificationSize = new(Notifications.NotificationSize.Width, Notifications.NotificationSize.Height);
    
    private readonly ILogger<NotificationService> _logger;
    private readonly IViewModelFactory _viewModelFactory;
    private readonly IDispatcherService _dispatcherService;
    private readonly INotificationPositionService _notificationPositionService;

    private readonly Queue<INotification> _notificationsQueue = new();


    private Window? _mainWindow;

    public NotificationService(ILogger<NotificationService> logger, IViewModelFactory viewModelFactory, 
        IDispatcherService dispatcherService, INotificationPositionService notificationPositionService)
    {
        _logger = logger;
        _viewModelFactory = viewModelFactory;
        _dispatcherService = dispatcherService;
        _notificationPositionService = notificationPositionService;

        CurrentNotifications = new ObservableCollection<INotification>();

        DefaultBorderBrush = Brushes.Black;
        DefaultBackgroundBrush = Brushes.DodgerBlue;
        DefaultFontBrush = Brushes.WhiteSmoke;

        var app = Application.Current;
        if (app is null)
        {
            return;
        }

        if (app.TryFindResource("AccentColorBrush") is SolidColorBrush accentColorBrush)
        {
            DefaultBorderBrush = accentColorBrush;
            DefaultBackgroundBrush = new SolidColorBrush(Color.FromArgb(255, 245, 245, 245));
            DefaultFontBrush = Brushes.Black;
            accentColorBrush.Color.CreateAccentColorResourceDictionary();
        }

        _mainWindow = app.MainWindow;
    }

    public ObservableCollection<INotification> CurrentNotifications { get; }

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
            _logger.LogDebug("Notifications are suspended, queueing notification");

            _notificationsQueue.Enqueue(notification);

            return;
        }

        _dispatcherService.BeginInvoke(() =>
        {
            EnsureMainWindow();

            var hasActiveWindows = HasActiveWindows();
            if (!hasActiveWindows && notification.Priority <= NotificationPriority.Normal)
            {
                _logger.LogDebug($"Not showing notification '{notification}' since priority is '{notification.Priority}' and app has no active windows.");
                return;
            }

            _logger.LogDebug($"Showing notification '{notification}'");

            var notificationLocation = _notificationPositionService.GetLeftTopCorner(NotificationSize, CurrentNotifications.Count);

            var popup = new Popup
            {
                AllowsTransparency = true,
                Placement = PlacementMode.Custom
            };

            popup.CustomPopupPlacementCallback += (_, _, _) =>
            {
                var x = DpiHelper.CalculateSize(DpiHelper.DpiX, notificationLocation.X);
                var y = DpiHelper.CalculateSize(DpiHelper.DpiY, notificationLocation.Y);

                var popupPlacement = new CustomPopupPlacement(new Point(x, y), PopupPrimaryAxis.None);

                var ttplaces = new[]
                {
                    popupPlacement
                };
                return ttplaces;
            };

            var notificationViewModel = _viewModelFactory.CreateRequiredViewModel<NotificationViewModel>(notification);
            notificationViewModel.ClosedAsync += async (_, _) =>
            {
                _logger.LogDebug($"Hiding notification '{notification}'");

                popup.IsOpen = false;
            };

            var notificationView = new NotificationView
            {
                DataContext = notificationViewModel
            };
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
        if (sender is not NotificationView notificationView)
        {
            return;
        }

        notificationView.Unloaded -= OnNotificationViewUnloaded;

        var notification = notificationView.DataContext as INotification;
        if (notification is null)
        {
            var notificationViewModel = notificationView.DataContext as NotificationViewModel 
                                        ?? notificationView.ViewModel as NotificationViewModel;

            if (notificationViewModel is not null)
            {
                notification = notificationViewModel.Notification;
            }
        }

        if (notification is null)
        {
            return;
        }

        CurrentNotifications.Remove(notification);

        ClosedNotification?.Invoke(this, new NotificationEventArgs(notification));
    }
}

namespace Orc.SupportPackage.Example.ViewModels;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using Catel;
using Catel.MVVM;
using Catel.Services;
using Notifications;

public class MainViewModel : FeaturedViewModelBase
{
    private readonly IMessageService _messageService;
    private readonly INotificationService _notificationService;

    public MainViewModel(IServiceProvider serviceProvider, INotificationService notificationService,
        IMessageService messageService)
        : base(serviceProvider)
    {
        _notificationService = notificationService;
        _messageService = messageService;

        ShowErrorNotification = new Command(serviceProvider, OnShowErrorNotificationExecute, OnShowNotificationCanExecute);
        ShowWarningNotification = new Command(serviceProvider, OnShowWarningNotificationExecute, OnShowNotificationCanExecute);
        ShowNotification = new Command(serviceProvider, OnShowNotificationExecute, OnShowNotificationCanExecute);

        NotificationPriorities = Enum<NotificationPriority>.GetValues();
    }

    public override string Title => "Orc.Notifications example";

    [DefaultValue("This is an example title")]
    public string NotificationTitle { get; set; }

    [DefaultValue("Showing a message using notifications is really cool")]
    public string NotificationMessage { get; set; }

    [DefaultValue(true)] public bool IsClosable { get; set; }

    [DefaultValue(false)] public bool MinimizeWindow { get; set; }

    public List<NotificationPriority> NotificationPriorities { get; }

    [DefaultValue(NotificationPriority.High)]
    public NotificationPriority NotificationPriority { get; set; }

    public Command ShowErrorNotification { get; }

    public Command ShowWarningNotification { get; }

    public Command ShowNotification { get; }

    private bool OnShowNotificationCanExecute()
    {
        if (string.IsNullOrWhiteSpace(NotificationTitle))
        {
            return false;
        }

        return !string.IsNullOrWhiteSpace(NotificationMessage);
    }

    private void OnShowErrorNotificationExecute()
    {
        if (MinimizeWindow)
        {
            Application.Current.MainWindow?.SetCurrentValue(Window.WindowStateProperty, WindowState.Minimized);
        }

        var notification = new ErrorNotification
        {
            Title = NotificationTitle,
            Message = NotificationMessage,
            Command = new TaskCommand(ServiceProvider, async () => await _messageService.ShowAsync("You just clicked a notification")),
            IsClosable = IsClosable,
            Priority = NotificationPriority
        };

        _notificationService.ShowNotification(notification);
    }

    private void OnShowWarningNotificationExecute()
    {
        if (MinimizeWindow)
        {
            Application.Current.MainWindow?.SetCurrentValue(Window.WindowStateProperty, WindowState.Minimized);
        }

        var notification = new WarningNotification
        {
            Title = NotificationTitle,
            Message = NotificationMessage,
            Command = new TaskCommand(ServiceProvider, async () => await _messageService.ShowAsync("You just clicked a notification")),
            IsClosable = IsClosable,
            Priority = NotificationPriority
        };

        _notificationService.ShowNotification(notification);
    }

    private void OnShowNotificationExecute()
    {
        if (MinimizeWindow)
        {
            Application.Current.MainWindow?.SetCurrentValue(Window.WindowStateProperty, WindowState.Minimized);
        }

        var notification = new Notification
        {
            Title = NotificationTitle,
            Message = NotificationMessage,
            Command = new TaskCommand(ServiceProvider, async () => await _messageService.ShowAsync("You just clicked a notification")),
            IsClosable = IsClosable,
            Priority = NotificationPriority
        };

        _notificationService.ShowNotification(notification);
    }
}

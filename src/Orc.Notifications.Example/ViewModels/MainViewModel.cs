namespace Orc.Notifications.Example.ViewModels;

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
    private readonly ILanguageService _languageService;
    private readonly IMessageService _messageService;
    private readonly INotificationService _notificationService;

    public MainViewModel(IServiceProvider serviceProvider, INotificationService notificationService,
        IMessageService messageService, ILanguageService languageService)
        : base(serviceProvider)
    {
        _notificationService = notificationService;
        _messageService = messageService;
        _languageService = languageService;

        ShowErrorNotification = new Command(serviceProvider, OnShowErrorNotificationExecute, OnShowNotificationCanExecute);
        ShowWarningNotification = new Command(serviceProvider, OnShowWarningNotificationExecute, OnShowNotificationCanExecute);
        ShowNotification = new Command(serviceProvider, OnShowNotificationExecute, OnShowNotificationCanExecute);

        NotificationPriorities = Enum<NotificationPriority>.GetValues();

        NotificationTitle = languageService.GetString("Orc_Notifications_Example_DefaultNotificationTitle") ?? string.Empty;
        NotificationMessage = languageService.GetString("Orc_Notifications_Example_DefaultNotificationMessage") ?? string.Empty;
    }

    public override string Title => _languageService.GetString("Orc_Notifications_Example_WindowTitle") ?? "Orc.Notifications example";

    public string NotificationTitle { get; set; }

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
            Command = new TaskCommand(ServiceProvider, async () => await _messageService.ShowAsync(_languageService.GetString("Orc_Notifications_Example_NotificationClickedMessage"))),
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
            Command = new TaskCommand(ServiceProvider, async () => await _messageService.ShowAsync(_languageService.GetString("Orc_Notifications_Example_NotificationClickedMessage"))),
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
            Command = new TaskCommand(ServiceProvider, async () => await _messageService.ShowAsync(_languageService.GetString("Orc_Notifications_Example_NotificationClickedMessage"))),
            IsClosable = IsClosable,
            Priority = NotificationPriority
        };

        _notificationService.ShowNotification(notification);
    }
}

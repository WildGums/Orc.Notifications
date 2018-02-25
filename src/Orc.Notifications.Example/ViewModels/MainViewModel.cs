// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.SupportPackage.Example.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;

    using Catel;
    using Catel.MVVM;
    using Catel.Services;

    using Notifications;

    public class MainViewModel : ViewModelBase
    {
        private readonly INotificationService _notificationService;

        private readonly IMessageService _messageService;

        public MainViewModel(INotificationService notificationService, IMessageService messageService)
        {
            Argument.IsNotNull(() => notificationService);
            Argument.IsNotNull(() => messageService);

            _notificationService = notificationService;
            _messageService = messageService;

            ShowNotification = new Command(OnShowNotificationExecute, OnShowNotificationCanExecute);

            Title = "Orc.Notifications example";

            NotificationPriorities = Enum<NotificationPriority>.GetValues();

            // NotificationPriority = NotificationPriority.High;
        }


        #region Properties
        [DefaultValue("This is an example title")]
        public string NotificationTitle { get; set; }

        [DefaultValue("Showing a message using notifications is really cool")]
        public string NotificationMessage { get; set; }

        [DefaultValue(true)]
        public bool IsClosable { get; set; }

        [DefaultValue(false)]
        public bool MinimizeWindow { get; set; }

        public List<NotificationPriority> NotificationPriorities { get; }

        [DefaultValue(NotificationPriority.High)]
        public NotificationPriority NotificationPriority { get; set; }

        #endregion

        #region Commands
        public Command ShowNotification { get; private set; }

        private bool OnShowNotificationCanExecute()
        {
            if (string.IsNullOrWhiteSpace(NotificationTitle))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(NotificationMessage))
            {
                return false;
            }

            return true;
        }

        private void OnShowNotificationExecute()
        {
            if (MinimizeWindow)
            {
                Application.Current.MainWindow.SetCurrentValue(Window.WindowStateProperty, WindowState.Minimized);
            }

            var notification = new Notification { Title = NotificationTitle, Message = NotificationMessage, Command = new TaskCommand(async () => await _messageService.ShowAsync("You just clicked a notification")), IsClosable = IsClosable, Priority = NotificationPriority};

            _notificationService.ShowNotification(notification);
        }

        #endregion
    }
}
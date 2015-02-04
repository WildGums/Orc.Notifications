// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.SupportPackage.Example.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Drawing.Imaging;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media.Imaging;
    using Catel;
    using Catel.IO;
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
        }

        #region Properties
        [DefaultValue("This is an example title")]
        public string NotificationTitle { get; set; }

        [DefaultValue("Showing a message using notifications is really cool")]
        public string NotificationMessage { get; set; }
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
            var notification = new Notification
            {
                Title = NotificationTitle,
                Message = NotificationMessage,
                Command = new Command(() => _messageService.Show("You just clicked a notification"))
            };

            _notificationService.ShowNotification(notification);
        }
        #endregion
    }
}
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
    using Notifications;

    public class MainViewModel : ViewModelBase
    {
        private readonly INotificationService _notificationService;

        public MainViewModel(INotificationService notificationService)
        {
            Argument.IsNotNull(() => notificationService);

            _notificationService = notificationService;

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
                Message = NotificationMessage
            };

            _notificationService.ShowNotification(notification);
        }
        #endregion
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationService.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Notifications
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
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
        }
        #endregion

        #region Properties
        public ObservableCollection<INotification> CurrentNotifications { get; private set; }

        public SolidColorBrush DefaultBorderBrush { get; set; }

        public SolidColorBrush DefaultBackgroundBrush { get; set; }

        public SolidColorBrush DefaultFontBrush { get; set; }
        #endregion

        #region Methods
        public async Task ShowNotification(INotification notification)
        {
            Argument.IsNotNull(() => notification);

            _dispatcherService.BeginInvoke(() =>
            {
                Log.Debug("Showing notification '{0}'", notification);

                _uiVisualizerService.Show<NotificationViewModel>(notification, OnNotificationClosed);

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
            }
        }
        #endregion
    }
}
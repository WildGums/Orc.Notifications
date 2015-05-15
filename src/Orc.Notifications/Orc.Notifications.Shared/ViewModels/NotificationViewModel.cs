// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationViewModel.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Notifications
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;
    using Catel;
    using Catel.MVVM;
    using Catel.Reflection;
    using Helpers;

    public class NotificationViewModel : ViewModelBase
    {
        private DispatcherTimer _dispatcherTimer;
        private readonly Assembly _entryAssembly = AssemblyHelper.GetEntryAssembly();

        public NotificationViewModel(INotification notification, INotificationService notificationService)
        {
            Argument.IsNotNull(() => notification);

            Notification = notification;
            Title = notification.Title;
            Message = notification.Message;
            Command = notification.Command;
            ShowTime = notification.ShowTime;

            BorderBrush = notification.BorderBrush ?? notificationService.DefaultBorderBrush;
            BackgroundBrush = notification.BackgroundBrush ?? notificationService.DefaultBackgroundBrush;
            FontBrush = notification.FontBrush ?? notificationService.DefaultFontBrush;
            AppIcon = ExtractLargestIcon();

            PauseTimer = new Command(OnPauseTimerExecute);
            ResumeTimer = new Command(OnResumeTimerExecute);
            ClosePopup = new TaskCommand(OnClosePopupExecute);
        }

        

        #region Properties
        public INotification Notification { get; private set; }

        public string Message { get; private set; }

        public ICommand Command { get; private set; }

        public TimeSpan ShowTime { get; private set; }

        public SolidColorBrush BorderBrush { get; private set; }

        public SolidColorBrush BackgroundBrush { get; private set; }

        public SolidColorBrush FontBrush { get; private set; }

        public BitmapSource AppIcon { get; private set; }
        #endregion

        #region Commands
        public TaskCommand ClosePopup { get; private set; }

        private async Task OnClosePopupExecute()
        {
            await CloseViewModel(null);
        }

        public Command PauseTimer { get; private set; }

        private void OnPauseTimerExecute()
        {
            if (_dispatcherTimer != null)
            {
                _dispatcherTimer.Stop();
            }
        }

        public Command ResumeTimer { get; private set; }

        private void OnResumeTimerExecute()
        {
            if (_dispatcherTimer != null)
            {
                _dispatcherTimer.Start();
            }
        }
        #endregion

        #region Methods
        protected override async Task Initialize()
        {
            await base.Initialize();

            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = ShowTime;
            _dispatcherTimer.Tick += OnDispatcherTimerTick;
            _dispatcherTimer.Start();
        }

        protected override async Task Close()
        {
            _dispatcherTimer.Stop();
            _dispatcherTimer.Tick -= OnDispatcherTimerTick;
            _dispatcherTimer = null;

            await base.Close();
        }

        private async void OnDispatcherTimerTick(object sender, EventArgs e)
        {
            await SaveAndCloseViewModel();
        }

        private BitmapImage ExtractLargestIcon()
        {
            return IconHelper.ExtractLargestIconFromFile(_entryAssembly.Location);
        }
        #endregion
    }
}
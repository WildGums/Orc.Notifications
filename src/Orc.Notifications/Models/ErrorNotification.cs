namespace Orc.Notifications
{
    using System;
    using System.Windows.Media;

    public class ErrorNotification : Notification
    {
        private static readonly SolidColorBrush DefaultBackgroundBrush = new SolidColorBrush(Color.FromArgb(255, 242, 91, 67));

        static ErrorNotification()
        {
            DefaultBackgroundBrush.Freeze();
        }

        public ErrorNotification()
        {
            ShowTime = TimeSpan.FromSeconds(5);
            Priority = NotificationPriority.High;
            Level = NotificationLevel.Error;
            BorderBrush = Brushes.Red;
            BackgroundBrush = DefaultBackgroundBrush;
        }
    }
}

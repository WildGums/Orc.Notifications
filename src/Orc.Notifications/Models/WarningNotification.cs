namespace Orc.Notifications
{
    using System;
    using System.Windows.Media;

    public class WarningNotification : Notification
    {
        public WarningNotification()
        {
            ShowTime = TimeSpan.FromSeconds(5);
            Priority = NotificationPriority.High;
            Level = NotificationLevel.Warning;
            BorderBrush = Brushes.Orange;
            BackgroundBrush = new SolidColorBrush(Color.FromArgb(255, 242, 155, 84));
        }
    }
}

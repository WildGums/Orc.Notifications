namespace Orc.Notifications
{
    using System;
    using System.Windows;
    using Catel.MVVM.Converters;

    public class NotificationBorderBrushConverter : ValueConverterBase<INotification>
    {
        protected override object Convert(INotification value, Type targetType, object parameter)
        {
            var borderBrush = value?.BorderBrush;
            if (borderBrush is not null)
            {
                return borderBrush.Color;
            }

            return Application.Current.Resources["NotificationAccentColor"];
        }
    }
}

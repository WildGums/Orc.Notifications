// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INotificationService.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Notifications
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Media;

    public interface INotificationService
    {
        Task ShowNotification(INotification notification);
        SolidColorBrush DefaultBorderBrush { get; set; }
        SolidColorBrush DefaultBackgroundBrush { get; set; }
        SolidColorBrush DefaultFontBrush { get; set; }
        ObservableCollection<INotification> CurrentNotifications { get; }
    }
}
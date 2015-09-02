// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INotificationPositionService.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Notifications
{
    using System.Drawing;

    public interface INotificationPositionService
    {
        Point GetLeftTopCorner(Size notificationSize, int numberOfNotifications);
    }
}
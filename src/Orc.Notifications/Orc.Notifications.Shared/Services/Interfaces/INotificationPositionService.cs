// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INotificationPositionService.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
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
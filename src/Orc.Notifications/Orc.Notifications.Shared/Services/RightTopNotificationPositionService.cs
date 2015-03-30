// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RightTopNotificationPositionService.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Notifications
{
    using System.Drawing;
    using Catel;

    public class RightTopNotificationPositionService : INotificationPositionService
    {
        private readonly INotificationService _notificationService;

        private const int Margin = 15;

        public RightTopNotificationPositionService(INotificationService notificationService)
        {
            Argument.IsNotNull(() => notificationService);

            _notificationService = notificationService;
        }

        public virtual Point GetLeftTopCorner(Size notificationSize)
        {
            var workArea = System.Windows.SystemParameters.WorkArea;

            var top = workArea.Top + Margin;
            var right = workArea.Right - Margin;

            var notificationCount = _notificationService.CurrentNotifications.Count;
            for (var i = 0; i < notificationCount; i++)
            {
                top += notificationSize.Height;
                top += Margin;
            }

            var left = right - notificationSize.Width;

            return new Point((int)left, (int)top);
        }
    }
}
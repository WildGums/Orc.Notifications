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
        private const int Margin = 15;

        public RightTopNotificationPositionService()
        {
        }

        public virtual Point GetLeftTopCorner(Size notificationSize, int numberOfNotifications)
        {
            var workArea = System.Windows.SystemParameters.WorkArea;

            var top = workArea.Top + Margin;
            var right = workArea.Right - Margin;

            for (var i = 0; i < numberOfNotifications; i++)
            {
                top += notificationSize.Height;
                top += Margin;
            }

            var left = right - notificationSize.Width;

            return new Point((int)left, (int)top);
        }
    }
}
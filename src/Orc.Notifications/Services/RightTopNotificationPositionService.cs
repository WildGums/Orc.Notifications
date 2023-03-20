namespace Orc.Notifications;

using System.Drawing;

public class RightTopNotificationPositionService : INotificationPositionService
{
    private const int Margin = 15;

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

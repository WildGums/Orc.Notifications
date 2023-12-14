namespace Orc.Notifications;

using System.Drawing;

public interface INotificationPositionService
{
    Point GetLeftTopCorner(Size notificationSize, int numberOfNotifications);
}

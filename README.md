# Orc.Notifications

Allows the user to show desktop notifications. Notification looks like popup window on top right corner of the screen

![Notifications 01](doc/images/Notifications_01.png)

for showing this notification use service:

* **INotificationService** 

for configuring notification used **Notification** class, which is implementes interface **INotification**

	public interface INotification
    {
        string Title { get; set; }
        string Message { get; set; }
        ICommand Command { get; set; }
        TimeSpan ShowTime { get; set; }
        SolidColorBrush BorderBrush { get; set; }
        SolidColorBrush BackgroundBrush { get; set; }
        SolidColorBrush FontBrush { get; set; }
    }





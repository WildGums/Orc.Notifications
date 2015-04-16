Orc.Notifications
==================

This library is used to create and display desktop notifications. Notification looks like popup windows and will appear on the top right corner of the screen.

Notifications can be used to breifly display important information to the user.

![Notifications 01](doc/images/Notifications_01.png)

In order to create notifications in your application use IOC with the **INotificationService** interface.

A default **Notification** class, which implements the **INotification** interface is provided.

```c#
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
```




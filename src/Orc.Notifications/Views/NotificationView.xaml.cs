namespace Orc.Notifications;

using System.Windows;
using System.Windows.Input;

public partial class NotificationView
{
    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        ActivatePreviousWindow();

        var vm = ViewModel as NotificationViewModel;
        var command = vm?.Command;
        command?.Execute(null);

        e.Handled = true;
    }

    private static void ActivatePreviousWindow()
    {
        var currentWindows = Application.Current.Windows;
        var count = currentWindows.Count - 1;
        if (count < 0)
        {
            return;
        }

        var window = currentWindows[count];
        window.Activate();
    }
}

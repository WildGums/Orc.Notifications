namespace Orc.Notifications;

using System.Windows;
using System.Windows.Input;

public partial class NotificationView
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationView"/> class.
    /// </summary>
    public NotificationView()
        : this(null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationView"/> class.
    /// </summary>
    /// <param name="viewModel">The view model to inject.</param>
    /// <remarks>
    /// This constructor can be used to use view-model injection.
    /// </remarks>
    public NotificationView(NotificationViewModel? viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }

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

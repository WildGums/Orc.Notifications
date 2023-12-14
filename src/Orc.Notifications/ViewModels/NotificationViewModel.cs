namespace Orc.Notifications;

using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Catel.MVVM;
using Catel.Reflection;
using Theming;

public class NotificationViewModel : ViewModelBase
{
    private DispatcherTimer? _dispatcherTimer;
    private readonly Assembly _entryAssembly = AssemblyHelper.GetRequiredEntryAssembly();

    public NotificationViewModel(INotification notification, INotificationService notificationService)
    {
        ArgumentNullException.ThrowIfNull(notification);

        Notification = notification;
        IsClosable = notification.IsClosable;
        Message = notification.Message;
        Command = notification.Command;
        ShowTime = notification.ShowTime;

        BorderBrush = notification.BorderBrush ?? notificationService.DefaultBorderBrush;
        BackgroundBrush = notification.BackgroundBrush ?? notificationService.DefaultBackgroundBrush;
        FontBrush = notification.FontBrush ?? notificationService.DefaultFontBrush;
        AppIcon = ExtractLargestIcon();

        PauseTimer = new Command(OnPauseTimerExecute);
        ResumeTimer = new Command(OnResumeTimerExecute);
        ClosePopup = new TaskCommand(OnClosePopupExecuteAsync);

        // Validation makes no sense on notifications
        AutomaticallyValidateOnPropertyChanged = false;
    }

    public override string Title => Notification.Title;

    public INotification Notification { get; }

    public bool IsClosable { get; }

    public string Message { get; }

    public ICommand? Command { get; }

    public TimeSpan ShowTime { get; }

    public SolidColorBrush? BorderBrush { get; }

    public SolidColorBrush? BackgroundBrush { get; }

    public SolidColorBrush? FontBrush { get; }

    public BitmapSource? AppIcon { get; }

    public TaskCommand ClosePopup { get; }

    private async Task OnClosePopupExecuteAsync()
    {
        await CloseViewModelAsync(null);
    }

    public Command PauseTimer { get; }

    private void OnPauseTimerExecute()
    {
        _dispatcherTimer?.Stop();
    }

    public Command ResumeTimer { get; }

    private void OnResumeTimerExecute()
    {
        _dispatcherTimer?.Start();
    }

    protected override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        _dispatcherTimer = new DispatcherTimer
        {
            Interval = ShowTime
        };
        _dispatcherTimer.Tick += OnDispatcherTimerTick;
        _dispatcherTimer.Start();
    }

    protected override async Task CloseAsync()
    {
        var dispatcherTimer = _dispatcherTimer;
        if (dispatcherTimer is not null)
        {
            dispatcherTimer.Stop();
            dispatcherTimer.Tick -= OnDispatcherTimerTick;
            _dispatcherTimer = null;
        }

        await base.CloseAsync();
    }

    private void OnDispatcherTimerTick(object? sender, EventArgs e)
    {
#pragma warning disable 4014
        // Cancel to make sure we don't enable validation
        this.CancelAndCloseViewModelAsync();
#pragma warning restore 4014
    }

    private BitmapImage? ExtractLargestIcon()
    {
        return IconHelper.ExtractLargestIconFromFile(_entryAssembly.Location);
    }
}

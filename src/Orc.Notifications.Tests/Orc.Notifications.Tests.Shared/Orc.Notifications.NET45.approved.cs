[assembly: System.Resources.NeutralResourcesLanguageAttribute("en-US")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleToAttribute("Orc.Notifications.Tests")]
[assembly: System.Runtime.InteropServices.ComVisibleAttribute(false)]
[assembly: System.Runtime.Versioning.TargetFrameworkAttribute(".NETFramework,Version=v4.5", FrameworkDisplayName=".NET Framework 4.5")]


public class static LoadAssembliesOnStartup { }
public class static ModuleInitializer
{
    public static void Initialize() { }
}
namespace Orc.Notifications
{
    
    public interface INotification
    {
        System.Windows.Media.SolidColorBrush BackgroundBrush { get; set; }
        System.Windows.Media.SolidColorBrush BorderBrush { get; set; }
        System.Windows.Input.ICommand Command { get; set; }
        System.Windows.Media.SolidColorBrush FontBrush { get; set; }
        bool IsClosable { get; set; }
        string Message { get; set; }
        System.TimeSpan ShowTime { get; set; }
        string Title { get; set; }
    }
    public interface INotificationPositionService
    {
        System.Drawing.Point GetLeftTopCorner(System.Drawing.Size notificationSize, int numberOfNotifications);
    }
    public interface INotificationService
    {
        System.Collections.ObjectModel.ObservableCollection<Orc.Notifications.INotification> CurrentNotifications { get; }
        System.Windows.Media.SolidColorBrush DefaultBackgroundBrush { get; set; }
        System.Windows.Media.SolidColorBrush DefaultBorderBrush { get; set; }
        System.Windows.Media.SolidColorBrush DefaultFontBrush { get; set; }
        bool IsSuspended { get; }
        public event System.EventHandler<Orc.Notifications.NotificationEventArgs> ClosedNotification;
        public event System.EventHandler<Orc.Notifications.NotificationEventArgs> OpenedNotification;
        void Resume();
        void ShowNotification(Orc.Notifications.INotification notification);
        void Suspend();
    }
    public class static INotificationServiceExtensions
    {
        public static void ShowNotification(this Orc.Notifications.INotificationService notificationService, string title, string message) { }
    }
    public class Notification : Orc.Notifications.INotification
    {
        public Notification() { }
        public System.Windows.Media.SolidColorBrush BackgroundBrush { get; set; }
        public System.Windows.Media.SolidColorBrush BorderBrush { get; set; }
        public System.Windows.Input.ICommand Command { get; set; }
        public System.Windows.Media.SolidColorBrush FontBrush { get; set; }
        public int Id { get; }
        public bool IsClosable { get; set; }
        public string Message { get; set; }
        public System.TimeSpan ShowTime { get; set; }
        public string Title { get; set; }
        public override string ToString() { }
    }
    public class NotificationEventArgs : System.EventArgs
    {
        public NotificationEventArgs(Orc.Notifications.INotification notification) { }
        public Orc.Notifications.INotification Notification { get; }
    }
    public class NotificationService : Orc.Notifications.INotificationService
    {
        public NotificationService(Catel.MVVM.IViewModelFactory viewModelFactory, Catel.Services.IDispatcherService dispatcherService, Orc.Notifications.INotificationPositionService notificationPositionService) { }
        public System.Collections.ObjectModel.ObservableCollection<Orc.Notifications.INotification> CurrentNotifications { get; }
        public System.Windows.Media.SolidColorBrush DefaultBackgroundBrush { get; set; }
        public System.Windows.Media.SolidColorBrush DefaultBorderBrush { get; set; }
        public System.Windows.Media.SolidColorBrush DefaultFontBrush { get; set; }
        public bool IsSuspended { get; }
        public event System.EventHandler<Orc.Notifications.NotificationEventArgs> ClosedNotification;
        public event System.EventHandler<Orc.Notifications.NotificationEventArgs> OpenedNotification;
        public void Resume() { }
        public void ShowNotification(Orc.Notifications.INotification notification) { }
        public void Suspend() { }
    }
    public class static NotificationSize
    {
        public const int Height = 68;
        public const int Width = 380;
    }
    public class NotificationView : Catel.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector
    {
        public NotificationView() { }
        public NotificationView(Orc.Notifications.NotificationViewModel viewModel) { }
        public void InitializeComponent() { }
        protected override void OnMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e) { }
    }
    public class NotificationViewModel : Catel.MVVM.ViewModelBase
    {
        public static readonly Catel.Data.PropertyData AppIconProperty;
        public static readonly Catel.Data.PropertyData BackgroundBrushProperty;
        public static readonly Catel.Data.PropertyData BorderBrushProperty;
        public static readonly Catel.Data.PropertyData FontBrushProperty;
        public static readonly Catel.Data.PropertyData IsClosableProperty;
        public static readonly Catel.Data.PropertyData MessageProperty;
        public static readonly Catel.Data.PropertyData NotificationProperty;
        public static readonly Catel.Data.PropertyData ShowTimeProperty;
        public NotificationViewModel(Orc.Notifications.INotification notification, Orc.Notifications.INotificationService notificationService) { }
        public System.Windows.Media.Imaging.BitmapSource AppIcon { get; }
        public System.Windows.Media.SolidColorBrush BackgroundBrush { get; }
        public System.Windows.Media.SolidColorBrush BorderBrush { get; }
        public Catel.MVVM.TaskCommand ClosePopup { get; }
        public System.Windows.Input.ICommand Command { get; }
        public System.Windows.Media.SolidColorBrush FontBrush { get; }
        public bool IsClosable { get; }
        public string Message { get; }
        public Orc.Notifications.INotification Notification { get; }
        public Catel.MVVM.Command PauseTimer { get; }
        public Catel.MVVM.Command ResumeTimer { get; }
        public System.TimeSpan ShowTime { get; }
        protected override System.Threading.Tasks.Task CloseAsync() { }
        protected override System.Threading.Tasks.Task InitializeAsync() { }
    }
    public class RightTopNotificationPositionService : Orc.Notifications.INotificationPositionService
    {
        public RightTopNotificationPositionService() { }
        public virtual System.Drawing.Point GetLeftTopCorner(System.Drawing.Size notificationSize, int numberOfNotifications) { }
    }
}
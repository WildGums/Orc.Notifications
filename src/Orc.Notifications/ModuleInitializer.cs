using Catel.IoC;
using Catel.Services;
using Orc.Notifications;

/// <summary>
/// Used by the ModuleInit. All code inside the Initialize method is ran as soon as the assembly is loaded.
/// </summary>
public static class ModuleInitializer
{
    /// <summary>
    /// Initializes the module.
    /// </summary>
    public static void Initialize()
    {
        var serviceLocator = ServiceLocator.Default;

        serviceLocator.RegisterType<INotificationService, NotificationService>();
        serviceLocator.RegisterType<INotificationPositionService, RightTopNotificationPositionService>();

        var uiVisualizerService = serviceLocator.ResolveType<IUIVisualizerService>();
        uiVisualizerService.Register<NotificationViewModel, NotificationView>();

        var languageService = serviceLocator.ResolveType<ILanguageService>();
        languageService.RegisterLanguageSource(new LanguageResourceSource("Orc.Notifications", "Orc.Notifications.Properties", "Resources"));
    }
}
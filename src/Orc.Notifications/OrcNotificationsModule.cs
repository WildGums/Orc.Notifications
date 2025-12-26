namespace Orc.Notifications
{
    using Catel.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Orc.Notifications;

    /// <summary>
    /// Core module which allows the registration of default services in the service collection.
    /// </summary>
    public static class OrcNotificationsModule
    {
        public static IServiceCollection AddOrcNotifications(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<INotificationService, NotificationService>();
            serviceCollection.TryAddSingleton<INotificationPositionService, RightTopNotificationPositionService>();

            serviceCollection.AddSingleton<ILanguageSource>(new LanguageResourceSource("Orc.Notifications", "Orc.Notifications.Properties", "Resources"));

            return serviceCollection;
        }
    }
}

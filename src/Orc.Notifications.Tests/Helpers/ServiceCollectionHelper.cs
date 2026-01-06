namespace Orc.Notifications.Tests
{
    using Catel;
    using Microsoft.Extensions.DependencyInjection;
    using Orc.Theming;

    internal static class ServiceCollectionHelper
    {
        public static IServiceCollection CreateServiceCollection()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddLogging();
            serviceCollection.AddCatelCore();
            serviceCollection.AddCatelMvvm();
            serviceCollection.AddOrcNotifications();
            serviceCollection.AddOrcTheming();

            return serviceCollection;
        }
    }
}

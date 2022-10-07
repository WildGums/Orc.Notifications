namespace Orc.Notifications
{
    using System.Windows;
    using System.Windows.Media;

    internal static class ColorExtensions
    {
        private static ResourceDictionary _accentColorResourceDictionary;

        public static ResourceDictionary CreateAccentColorResourceDictionary(this Color color)
        {
            if (_accentColorResourceDictionary is not null)
            {
                return _accentColorResourceDictionary;
            }

            var resourceDictionary = new ResourceDictionary
            {
                { "NotificationAccentColor", color }
            };

            resourceDictionary.Add("NotificationAccentColorBrush", new SolidColorBrush((Color) resourceDictionary["NotificationAccentColor"]));

            var application = Application.Current;
            var applicationResources = application.Resources;
            applicationResources.MergedDictionaries.Insert(0, resourceDictionary);

            _accentColorResourceDictionary = resourceDictionary;
            return applicationResources;
        }
    }
}

namespace Orc.Notifications;

using System.Windows;
using System.Windows.Media;

internal static class ColorExtensions
{
    private static ResourceDictionary? AccentColorResourceDictionary;

    public static ResourceDictionary CreateAccentColorResourceDictionary(this Color color)
    {
        if (AccentColorResourceDictionary is not null)
        {
            return AccentColorResourceDictionary;
        }

        var resourceDictionary = new ResourceDictionary
        {
            { "NotificationAccentColor", color }
        };

        resourceDictionary.Add("NotificationAccentColorBrush", new SolidColorBrush((Color) resourceDictionary["NotificationAccentColor"]));

        var application = Application.Current;
        var applicationResources = application.Resources;
        applicationResources.MergedDictionaries.Insert(0, resourceDictionary);

        AccentColorResourceDictionary = resourceDictionary;
        return applicationResources;
    }
}

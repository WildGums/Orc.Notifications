// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorExtensions.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Notifications
{
    using System.Windows;
    using System.Windows.Media;

    internal static class ColorExtensions
    {
        #region Constants
        private static ResourceDictionary _accentColorResourceDictionary;
        #endregion

        #region Methods
        public static ResourceDictionary CreateAccentColorResourceDictionary(this Color color)
        {
            if (_accentColorResourceDictionary != null)
            {
                return _accentColorResourceDictionary;
            }
            var resourceDictionary = new ResourceDictionary();

            resourceDictionary.Add("NotificationAccentColor", color);

            resourceDictionary.Add("NotificationAccentColorBrush", new SolidColorBrush((Color) resourceDictionary["NotificationAccentColor"]));

            var application = Application.Current;
            var applicationResources = application.Resources;
            applicationResources.MergedDictionaries.Insert(0, resourceDictionary);

            _accentColorResourceDictionary = resourceDictionary;
            return applicationResources;
        }
        #endregion
    }
}
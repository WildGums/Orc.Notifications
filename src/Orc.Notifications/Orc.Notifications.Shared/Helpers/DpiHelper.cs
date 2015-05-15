// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DpiHelper.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Notifications
{
    using System.Reflection;
    using System.Windows;

    internal static class DpiHelper
    {
        static DpiHelper()
        {
            var dpiXProperty = typeof(SystemParameters).GetProperty("DpiX", BindingFlags.NonPublic | BindingFlags.Static);
            var dpiYProperty = typeof(SystemParameters).GetProperty("Dpi", BindingFlags.NonPublic | BindingFlags.Static);

            DpiX = (int)dpiXProperty.GetValue(null, null);
            DpiY = (int)dpiYProperty.GetValue(null, null);
        }

        public static int DpiX { get; private set; }

        public static int DpiY { get; private set; }

        public static double CalculateSize(int dpi, double regularSize)
        {
            var factor = dpi/96d;

            return regularSize*factor;
        }
    }
}
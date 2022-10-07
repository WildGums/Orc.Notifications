namespace Orc.Notifications
{
    using System;
    using System.Reflection;
    using System.Windows;

    internal static class DpiHelper
    {
        static DpiHelper()
        {
            var dpiXProperty = typeof(SystemParameters).GetProperty("DpiX", BindingFlags.NonPublic | BindingFlags.Static);
            if (dpiXProperty is null)
            {
                throw new InvalidOperationException($"Cannot find property DpiX");
            }

            var dpiYProperty = typeof(SystemParameters).GetProperty("Dpi", BindingFlags.NonPublic | BindingFlags.Static);
            if (dpiYProperty is null)
            {
                throw new InvalidOperationException($"Cannot find property DpiY");
            }

            DpiX = (int?)dpiXProperty.GetValue(null, null) ?? 96;
            DpiY = (int?)dpiYProperty.GetValue(null, null) ?? 96;
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

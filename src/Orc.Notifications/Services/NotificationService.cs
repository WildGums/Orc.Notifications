// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationService.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Notifications
{
    using System.Threading.Tasks;
    using System.Windows.Media;
    using Catel;
    using Catel.Logging;
    using Catel.Services;

    public class NotificationService : INotificationService
    {
        #region Fields
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly IUIVisualizerService _uiVisualizerService;
        #endregion

        #region Constructors
        public NotificationService(IUIVisualizerService uiVisualizerService)
        {
            Argument.IsNotNull(() => uiVisualizerService);

            _uiVisualizerService = uiVisualizerService;

            DefaultBorderBrush = Brushes.Black;
            DefaultBackgroundBrush = Brushes.DodgerBlue;
            DefaultFontBrush = Brushes.WhiteSmoke;
        }
        #endregion

        #region Properties
        public SolidColorBrush DefaultBorderBrush { get; set; }

        public SolidColorBrush DefaultBackgroundBrush { get; set; }

        public SolidColorBrush DefaultFontBrush { get; set; }
        #endregion

        #region Methods
        public async Task ShowNotification(INotification notification)
        {
            Argument.IsNotNull(() => notification);

            Log.Debug("Showing notification '{0}'", notification);

            await _uiVisualizerService.Show<NotificationViewModel>(notification);
        }
        #endregion
    }
}
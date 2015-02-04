// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationView.xaml.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Notifications
{
    using System.Drawing;
    using Catel.IoC;
    using Catel.Windows;

    /// <summary>
    /// Interaction logic for NotificationView.xaml.
    /// </summary>
    public partial class NotificationView
    {
        private static readonly Size NotificationSize = new Size(Orc.Notifications.NotificationSize.Width, Orc.Notifications.NotificationSize.Height);

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationView"/> class.
        /// </summary>
        public NotificationView()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model to inject.</param>
        /// <remarks>
        /// This constructor can be used to use view-model injection.
        /// </remarks>
        public NotificationView(NotificationViewModel viewModel)
            : base(viewModel, DataWindowMode.Custom)
        {
            InitializeComponent();

            var dependencyResolver = this.GetDependencyResolver();
            var notificationPositionService = dependencyResolver.Resolve<INotificationPositionService>();

            var notificationLocation = notificationPositionService.GetLeftTopCorner(NotificationSize);

            WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
            Left = notificationLocation.X;
            Top = notificationLocation.Y;
        }
    }
}
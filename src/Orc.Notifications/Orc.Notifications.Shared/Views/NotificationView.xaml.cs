﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationView.xaml.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Notifications
{
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for NotificationView.xaml.
    /// </summary>
    public partial class NotificationView
    {
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
            : base(viewModel)
        {
            InitializeComponent();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            ActivatePreviousWindow();

            var vm = ViewModel as NotificationViewModel;
            if (vm != null)
            {
                var command = vm.Command;
                if (command != null)
                {
                    command.Execute(null);
                }
            }

            e.Handled = true;
        }

        private void ActivatePreviousWindow()
        {
            var currentWindows = Application.Current.Windows;
            var count = currentWindows.Count - 1;
            while (count >= 0)
            {
                var window = currentWindows[count--];
                //if (window is NotificationView)
                //{
                //    continue;
                //}

                window.Activate();
                return;
            }
        }
    }
}
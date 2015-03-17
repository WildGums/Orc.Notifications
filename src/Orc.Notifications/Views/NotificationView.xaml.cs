// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationView.xaml.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Notifications
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Input;
    using Catel.IoC;
    using Catel.Windows;
    using Catel.Windows.Threading;
    using Size = System.Drawing.Size;

    /// <summary>
    /// Interaction logic for NotificationView.xaml.
    /// </summary>
    public partial class NotificationView
    {
        private static readonly Size NotificationSize = new Size(Orc.Notifications.NotificationSize.Width, Orc.Notifications.NotificationSize.Height);

        private enum HookType : int
        {
            WH_JOURNALRECORD = 0,
            WH_JOURNALPLAYBACK = 1,
            WH_KEYBOARD = 2,
            WH_GETMESSAGE = 3,
            WH_CALLWNDPROC = 4,
            WH_CBT = 5,
            WH_SYSMSGFILTER = 6,
            WH_MOUSE = 7,
            WH_HARDWARE = 8,
            WH_DEBUG = 9,
            WH_SHELL = 10,
            WH_FOREGROUNDIDLE = 11,
            WH_CALLWNDPROCRET = 12,
            WH_KEYBOARD_LL = 13,
            WH_MOUSE_LL = 14
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern uint GetCurrentThreadId();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(HookType hookType, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        private delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);

        private readonly HookProc _hookProc;
        private IntPtr _hook;

        private System.Windows.Window _mainWindow;

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

            var threadId = GetCurrentThreadId();

            // Note: very important to store this in a field to prevent CallbackOnCollectedDelegateException
            // ReSharper disable once RedundantDelegateCreation
            _hookProc = new HookProc(CbtCallbackFunction);
            _hook = SetWindowsHookEx(HookType.WH_CBT, _hookProc, IntPtr.Zero, threadId);

            var dependencyResolver = this.GetDependencyResolver();
            var notificationPositionService = dependencyResolver.Resolve<INotificationPositionService>();

            var notificationLocation = notificationPositionService.GetLeftTopCorner(NotificationSize);

            WindowStartupLocation = WindowStartupLocation.Manual;
            Left = notificationLocation.X;
            Top = notificationLocation.Y;
        }

        protected override void OnLoaded(EventArgs e)
        {
            base.OnLoaded(e);

            var application = Application.Current;
            if (application != null)
            {
                _mainWindow = application.MainWindow;
                if (_mainWindow != null)
                {
                    _mainWindow.Closing += OnMainWindowClosing;
                }
            }
        }

        protected override void OnUnloaded(EventArgs e)
        {
            if (_mainWindow != null)
            {
                _mainWindow.Closing -= OnMainWindowClosing;
                _mainWindow = null;
            }

            base.OnUnloaded(e);
        }

        private void OnMainWindowClosing(object sender, CancelEventArgs e)
        {
            Close();
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
            var windowHandles = Application.Current.Windows;
            if (windowHandles.Count > 1)
            {
                var lastWindowHandle = windowHandles[windowHandles.Count - 2];
                lastWindowHandle.Activate();
            }
        }

        private int CbtCallbackFunction(int code, IntPtr wParam, IntPtr lParam)
        {
            Owner = null;

            switch (code)
            {
                case 5: /* HCBT_ACTIVATE */
                    UnhookWindowsHookEx(_hook);
                    return 1; /* prevent windows from handling activate */
            }

            //return the value returned by CallNextHookEx
            return CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
        }
    }
}
﻿namespace Orc.SupportPackage.Example;

using System.Globalization;
using System.Windows;
using Catel.IoC;
using Catel.Logging;
using Catel.Services;
using Orchestra;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    protected override void OnStartup(StartupEventArgs e)
    {
#if DEBUG
        LogManager.AddDebugListener(true);
#endif

        var languageService = ServiceLocator.Default.ResolveRequiredType<ILanguageService>();

        // Note: it's best to use .CurrentUICulture in actual apps since it will use the preferred language
        // of the user. But in order to demo multilingual features for devs (who mostly have en-US as .CurrentUICulture),
        // we use .CurrentCulture for the sake of the demo
        languageService.PreferredCulture = CultureInfo.CurrentCulture;
        languageService.FallbackCulture = new CultureInfo("en-US");

        this.ApplyTheme();

        base.OnStartup(e);
    }
}

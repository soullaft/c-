using MahApps.Metro;
using System;
using System.Windows;

namespace Vesna
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Tuple<AppTheme, Accent> appStyle = ThemeManager.DetectAppStyle(Application.Current);

            ThemeManager.ChangeAppStyle(Application.Current,
                                    ThemeManager.GetAccent("Green"),
                                    ThemeManager.GetAppTheme("BaseDark"));

            base.OnStartup(e);
        }
    }
}

#region

using System;
using System.Text;
using System.Windows;
using Hys.CareAgent.WcfService;
using log4net;
using MadMilkman.Ini;
using AutoUpdaterDotNET;
using System.Configuration;
using System.Timers;
using System.Globalization;
#endregion

namespace Hys.CareAgent.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static ILog Logger = LogManager.GetLogger(typeof(MainWindow));
        public MainWindow()
        {
            InitializeComponent();
            AutoUpdater.CurrentCulture = CultureInfo.CreateSpecificCulture("zh-CN");
            AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;
        }
        private void AutoUpdaterOnCheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            if (args != null)
            {
                if (args.IsUpdateAvailable)
                {
                    var dialogResult =
                        MessageBox.Show(
                            string.Format(
                                "({1}->{0})CareAgent有新的版本了，请及时更新!",
                                args.CurrentVersion, args.InstalledVersion),
                            @"CareAgent更新",
                            MessageBoxButton.OKCancel,
                            MessageBoxImage.Information, MessageBoxResult.Cancel, MessageBoxOptions.DefaultDesktopOnly);

                    if (dialogResult.Equals(MessageBoxResult.OK))
                    {
                        try
                        {
                            AutoUpdater.DownloadUpdate();
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButton.OK,
                                MessageBoxImage.Error);
                        }
                    }
                }
            }
        }
    }
}
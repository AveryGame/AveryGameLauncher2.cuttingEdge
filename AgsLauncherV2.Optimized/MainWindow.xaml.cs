using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using static AgsLauncherV2.Optimized.Services.Enums;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AgsLauncherV2.Optimized
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            launcherStatus = LauncherStatus.initialized;
        }

        // All NavButton logic
        private void Home(object sender, RoutedEventArgs e)
        {
            Pages.Home home = new Pages.Home();
            pageHost.NavigationService.Navigate(home);
            pageHost.Visibility = Visibility.Visible;
        }
        
        private void Changelog(object sender, RoutedEventArgs e)
        {
            Pages.Uncollapsed.Changelog changelog = new Pages.Uncollapsed.Changelog();
            pageHost.NavigationService.Navigate(changelog);
            pageHost.Visibility = Visibility.Visible;
        }

        private void Bugs(object sender, RoutedEventArgs e)
        {

        }

        private void News(object sender, RoutedEventArgs e)
        {

        }

        private void Settings(object sender, RoutedEventArgs e)
        {

        }
        // End NavButton logic

        // Basic window logic
        private void Close(object sender, RoutedEventArgs e)
        {
            Services.BasicLogic.HandleClose(launcherStatus, false, true, 1);
        }

        private void Minimize(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        
        private void DragBar(object sender, MouseButtonEventArgs e)
        {
            if (e.MouseDevice.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        // End basic window logic
    }
}

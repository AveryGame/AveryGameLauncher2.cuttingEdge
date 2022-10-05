using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AgsLauncherV2.Optimized.Pages.Uncollapsed
{
    /// <summary>
    /// Interaction logic for News.xaml
    /// </summary>
    public partial class News : Page
    {
        public News()
        {
            InitializeComponent();
        }


        // All NavButton logic
        private void Home(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            NavigationService.Navigate(home);
        }

        private void Changelog(object sender, RoutedEventArgs e)
        {
            Changelog changelog = new Changelog();
            NavigationService.Navigate(changelog);
        }

        private void Bugs(object sender, RoutedEventArgs e)
        {
            Bugs bugs = new Bugs();
            NavigationService.Navigate(bugs);
        }

        private void Settings(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            NavigationService.Navigate(settings);
        }
        // End NavButton logic


        //Unique page logic
        
        //End unique page logic
    }
}

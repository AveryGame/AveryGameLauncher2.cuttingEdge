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
    /// Interaction logic for Bugs.xaml
    /// </summary>
    public partial class Bugs : Page
    {
        public Bugs()
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

        private void News(object sender, RoutedEventArgs e)
        {
            News news = new News();
            NavigationService.Navigate(news);
        }

        private void Settings(object sender, RoutedEventArgs e)
        {

        }
        // End NavButton logic


        //Unique page logic
        private void LaunchButtonLogic(object sender, RoutedEventArgs e)
        {

        }
        //End unique page logic

        public MainWindow mw;
    }
}

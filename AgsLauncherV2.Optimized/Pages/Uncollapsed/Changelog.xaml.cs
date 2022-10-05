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
    /// Interaction logic for Changelog.xaml
    /// </summary>
    public partial class Changelog : Page
    {
        public Changelog()
        {
            InitializeComponent();
        }

        
        // All NavButton logic
        private void Home(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            NavigationService.Navigate(home);
        }

        private void Bugs(object sender, RoutedEventArgs e)
        {
            Bugs bugs = new Bugs();
            NavigationService.Navigate(bugs);
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

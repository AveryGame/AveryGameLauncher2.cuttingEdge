using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Diagnostics;
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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();
        }



        // All NavButton logic
        private void LegalNotice_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("explorer", "https://www.kianna.wtf/AveryGameLauncher2License");
        }
        
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

        private void News(object sender, RoutedEventArgs e)
        {
            News news = new News();
            NavigationService.Navigate(news);
        }
        // End NavButton logic


        //Unique page logic
        
        //End unique page logic
    }
}

using System.Windows;
using System.Windows.Controls;
using static AgsLauncherV2.Optimized.Services.Enums;

namespace AgsLauncherV2.Optimized.Pages.Uncollapsed
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
        }

        
        // All NavButton logic
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

        private void Settings(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            NavigationService.Navigate(settings);
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

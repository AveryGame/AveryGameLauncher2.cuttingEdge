using System.Windows;
using System.Windows.Controls;
using static AgsLauncherV2.Optimized.Services.Enums;

namespace AgsLauncherV2.Optimized.Pages
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
            Uncollapsed.Changelog changelog = new Uncollapsed.Changelog();
            NavigationService.Navigate(changelog);
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
        

        //Unique page logic
        private void LaunchButtonLogic(object sender, RoutedEventArgs e)
        {
            
        }
        //End unique page logic

        public MainWindow mw;
    }
}

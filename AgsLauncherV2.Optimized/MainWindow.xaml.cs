/*
 * AveryGame Launcher
 *  Copyright (C) 2022, Avery Fiebig-Dorey & Tristan Gee
 *
 *   This program is free software: you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation, either version 3 of the License, or
 *   (at your option) any later version.
 *
 *   This program is distributed in the hope that it will be useful,
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *   GNU General Public License for more details.
 *
 *   You should have received a copy of the GNU General Public License
 *   along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static AgsLauncherV2.Optimized.Services.Enums;
using System.Windows.Navigation;
using AgsLauncherV2.Optimized.Services;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;

namespace AgsLauncherV2.Optimized
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\CuttingEdge\\";
        public MainWindow()
        {
            //test
            InitializeComponent();
            BasicLogic.CheckAppData();
            InitializePageHost();
            InitializePagesOnAppEntry();
            InitializeVarsOnAppEntry();
            loadJsonStrings();
            RPC.InitRPC();
            //AGCloud json = JsonConvert.DeserializeObject<AGCloud>(localAppData + "clientStrings.json");
        }

        private void InitializePagesOnAppEntry()
        {
            Public.uncollapsedSettings = new Pages.Uncollapsed.Settings();
            Public.uncollapsedBugs = new Pages.Uncollapsed.Bugs();
            Public.uncollapsedNews = new Pages.Uncollapsed.News();
            Public.uncollapsedChangelog = new Pages.Uncollapsed.Changelog();
            Public.uncollapsedHome = new Pages.Uncollapsed.Home();
            Public.mainWindow = this;
        }

        private void InitializeVarsOnAppEntry()
        {
            RPC.rpcLabel = WelcomeRPCLabel;
            RPC.pfpImage = pfp;
            Public.Avgl2cEVersion = ReleaseString.Text;
        }

        
        // All NavButton logic
        private void Home(object sender, RoutedEventArgs e)
        {
            pageHost.NavigationService.Navigate(Public.uncollapsedHome);
            pageHost.Visibility = Visibility.Visible;
            HomeButton.Content = "• Home";
            ChangelogButton.Content = "Changelog";
            BugsButton.Content = "Bugs";
            NewsButton.Content = "News";
            SettingsButton.Content = "Settings";
        }
        
        private void Changelog(object sender, RoutedEventArgs e)
        {
            pageHost.NavigationService.Navigate(Public.uncollapsedChangelog);
            pageHost.Visibility = Visibility.Visible;
            HomeButton.Content = "Home";
            ChangelogButton.Content = "• Changelog";
            BugsButton.Content = "Bugs";
            NewsButton.Content = "News";
            SettingsButton.Content = "Settings";
        }

        private void Bugs(object sender, RoutedEventArgs e)
        {
            pageHost.NavigationService.Navigate(Public.uncollapsedBugs);
            pageHost.Visibility = Visibility.Visible;
            HomeButton.Content = "Home";
            ChangelogButton.Content = "Changelog";
            BugsButton.Content = "• Bugs";
            NewsButton.Content = "News";
            SettingsButton.Content = "Settings";
        }

        private void News(object sender, RoutedEventArgs e)
        {
            pageHost.NavigationService.Navigate(Public.uncollapsedNews);
            pageHost.Visibility = Visibility.Visible;
            HomeButton.Content = "Home";
            ChangelogButton.Content = "Changelog";
            BugsButton.Content = "Bugs";
            NewsButton.Content = "• News";
            SettingsButton.Content = "Settings";
        }

        private void Settings(object sender, RoutedEventArgs e)
        {
            pageHost.NavigationService.Navigate(Public.uncollapsedSettings);
            pageHost.Visibility = Visibility.Visible;
            HomeButton.Content = "Home";
            ChangelogButton.Content = "Changelog";
            BugsButton.Content = "Bugs";
            NewsButton.Content = "News";
            SettingsButton.Content = "• Settings";
        }
        // End NavButton logic

        // Basic window logic
        private void Close(object sender, RoutedEventArgs e)
        {
            BasicLogic.HandleClose(launcherStatus, false, true, 1);
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

        //Unique page logic
        private async void acceptCeNotice(object sender, RoutedEventArgs e)
        {
            AnimationHandler.FadeOut(cuttingEdgeNotice, 0.15);
            AnimationHandler.FadeOut(cuttingEdgeNoticeBlackout, 0.2);
            await Task.Delay(200);
            cuttingEdgeNotice.IsEnabled = false;
            cuttingEdgeNoticeBlackout.IsEnabled = false;
            cuttingEdgeNotice.Visibility = Visibility.Hidden;
            cuttingEdgeNoticeBlackout.Visibility = Visibility.Hidden;
            
        }

        private async void loadJsonStrings()
        {
            try
            {
                if (Public.json.showCuttingEdgeNotice == true)
                {
                    await Task.Delay(250);
                    cuttingEdgeNotice.Visibility = Visibility.Visible;
                    cuttingEdgeNoticeBlackout.Visibility = Visibility.Visible;
                    cuttingEdgeNotice.IsEnabled = true;
                    cuttingEdgeNoticeBlackout.IsEnabled = true;
                    AnimationHandler.FadeIn(cuttingEdgeNotice, 0.2);
                    AnimationHandler.FadeIn(cuttingEdgeNoticeBlackout, 0.2);
                }
                if (Public.userPreferences.CollapseSidebar == true)
                {
                    {
                        pageHost.Margin = new Thickness(88, 16, 56, 30);
                        AveryGame.Content = "AG";
                        AveryGame.Margin = new Thickness(5, 15, 0, 0);
                        AGSLogo.Margin = new Thickness(260, 0, 0, 0);
                        AnimationHandler.FadeOut(UncollapsedSidebar, 0.2);
                        AnimationHandler.FadeOut(HomeButton, 0.2);
                        AnimationHandler.FadeOut(ChangelogButton, 0.2);
                        AnimationHandler.FadeOut(BugsButton, 0.2);
                        AnimationHandler.FadeOut(NewsButton, 0.2);
                        AnimationHandler.FadeOut(SettingsButton, 0.2);
                        await Task.Delay(200);
                        HomeButton.Visibility = Visibility.Hidden;
                        ChangelogButton.Visibility = Visibility.Hidden;
                        BugsButton.Visibility = Visibility.Hidden;
                        NewsButton.Visibility = Visibility.Hidden;
                        SettingsButton.Visibility = Visibility.Hidden;
                        UncollapsedSidebar.Visibility = Visibility.Hidden;
                        HomeIcon.Visibility = Visibility.Hidden;
                        ChangelogIcon.Visibility = Visibility.Hidden;
                        BugsIcon.Visibility = Visibility.Hidden;
                        NewsIcon.Visibility = Visibility.Hidden;
                        SettingsIcon.Visibility = Visibility.Hidden;
                        HomeButton.IsEnabled = false;
                        ChangelogButton.IsEnabled = false;
                        BugsButton.IsEnabled = false;
                        NewsButton.IsEnabled = false;
                        SettingsButton.IsEnabled = false;
                        UncollapsedSidebar.IsEnabled = false;
                    }
                }
                if (!Directory.Exists(Public.userPreferences.InstallPath))
                {
                    JObject rss = JObject.Parse(File.ReadAllText(localAppData + "AGUserPreferences.json"));
                    rss["InstallPath"] = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\";
                    File.WriteAllText(localAppData + "AGUserPreferences.json", rss.ToString());
                    Public.userPreferences = JsonConvert.DeserializeObject<AGUserPreferences>(rss.ToString());
                }
                if (File.Exists(Public.userPreferences.InstallPath + "\\AveryGameFinale.zip"))
                {
                    File.Delete(Public.userPreferences.InstallPath + "\\AveryGameFinale.zip");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Normalize().ToString());
                await Task.Delay(1000);
                loadJsonStrings();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            launcherStatus = LauncherStatus.idle;
            //AnimationHandler.FadeOut(pageLoader, 0.15);
        }

        private void InitializePageHost()
        {
            pageHost.NavigationService.Navigate(Public.uncollapsedHome);
            pageHost.NavigationService.Navigate(Public.uncollapsedBugs);
            pageHost.NavigationService.Navigate(Public.uncollapsedChangelog);
            pageHost.NavigationService.Navigate(Public.uncollapsedNews);
            pageHost.NavigationService.Navigate(Public.uncollapsedSettings);
        }
        //End unique page logic
    }
}

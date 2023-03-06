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
using System.Reflection;
using static AgsLauncherV2.Optimized.Services.Enums.LogType;
namespace AgsLauncherV2.Optimized
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("Kernel32")]
        public static extern void AllocConsole();
        [DllImport("Kernel32", SetLastError = true)]
        public static extern void FreeConsole();
        private string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\CuttingEdge\\";
        
        public MainWindow()
        {
            Public.Avgl2cEVersion = "ags+void+3.0.2+kamo+cE?s=br";
            InitializeComponent();
            InitializeApplicationBase();
        }

        private async void InitializeApplicationBase()
        {
            var assemblyConfigurationAttribute = typeof(AgsLauncherV2.Optimized.MainWindow).Assembly.GetCustomAttribute<AssemblyConfigurationAttribute>();
            BuildConfiguration = assemblyConfigurationAttribute?.Configuration;
            if (BuildConfiguration == "Developer")
            {
                AllocConsole();
                Console.WriteLine("AgsLauncherV2.Optimized.MainWindow//OnApplicationLoad -- Build configuration is developer. \r\nIf you are not a developer, please delete this. \r\nAveryGame and it's developers are not liable for any damage done to your device.");
            }
            await AwaitInitializeLogger();
            InitializeVarsOnAppEntry();
            BasicLogic.CheckAppData();
            InitializePageHost();
            InitializePagesOnAppEntry();
            loadJsonStrings();
            RPC.InitRPC();
        }

        private static Task AwaitInitializeLogger()
        {
            Public.LogPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + $"\\AveryGame Launcher\\CuttingEdge\\Logs\\{DateTime.Now:dd-MM-yy on HH-mm-ss}.log";
            Logger.InitializeLogger();
            return Task.CompletedTask;
        }

        private void InitializePagesOnAppEntry()
        {
            Logger.Log(Info, "Setting public page variables to new instances of pages");
            Public.uncollapsedSettings = new();
            Public.uncollapsedBugs = new();
            Public.uncollapsedNews = new();
            Public.uncollapsedChangelog = new();
            Public.uncollapsedHome = new();
            Public.mainWindow = this;
            Logger.Log(Info, "Completed InitializePagesOnAppEntry()");
        }

        private void InitializeVarsOnAppEntry()
        {
            Logger.Log(Info, "Setting public variables to new instances of objects");
            RPC.rpcLabel = WelcomeRPCLabel;
            RPC.pfpImage = pfp;
            Public.Avgl2cEVersion = ReleaseString.Text;
            Logger.Log(Info, "Completed InitializeVarsOnAppEntry()");
        }


        // All NavButton logic
        private void Home(object sender, RoutedEventArgs e)
        {
            Logger.Log(Info, "Home button clicked, navigating");
            pageHost.NavigationService.Navigate(Public.uncollapsedHome);
            Logger.Log(Info, "Home button navigation complete, setting navigation button text");
            pageHost.Visibility = Visibility.Visible;
            HomeButton.Content = "• Home";
            ChangelogButton.Content = "Changelog";
            BugsButton.Content = "Bugs";
            NewsButton.Content = "News";
            SettingsButton.Content = "Settings";
            Logger.Log(Info, "Completed Home(object sender, RoutedEventArgs e)");
        }
        
        private void Changelog(object sender, RoutedEventArgs e)
        {
            Logger.Log(Info, "Changelog button clicked, navigating");
            pageHost.NavigationService.Navigate(Public.uncollapsedChangelog);
            Logger.Log(Info, "Changelog button navigation complete, setting navigation button text");
            pageHost.Visibility = Visibility.Visible;
            HomeButton.Content = "Home";
            ChangelogButton.Content = "• Changelog";
            BugsButton.Content = "Bugs";
            NewsButton.Content = "News";
            SettingsButton.Content = "Settings";
            Logger.Log(Info, "Completed Changelog(object sender, RoutedEventArgs e)");
        }

        private void Bugs(object sender, RoutedEventArgs e)
        {
            Logger.Log(Info, "Bugs button clicked, navigating");
            pageHost.NavigationService.Navigate(Public.uncollapsedBugs);
            Logger.Log(Info, "Bugs button navigation complete, setting navigation button text");
            pageHost.Visibility = Visibility.Visible;
            HomeButton.Content = "Home";
            ChangelogButton.Content = "Changelog";
            BugsButton.Content = "• Bugs";
            NewsButton.Content = "News";
            SettingsButton.Content = "Settings";
            Logger.Log(Info, "Completed Bugs(object sender, RoutedEventArgs e)");
        }

        private void News(object sender, RoutedEventArgs e)
        {
            Logger.Log(Info, "News button clicked, navigating");
            pageHost.NavigationService.Navigate(Public.uncollapsedNews);
            Logger.Log(Info, "News button navigation complete, setting navigation button text");
            pageHost.Visibility = Visibility.Visible;
            HomeButton.Content = "Home";
            ChangelogButton.Content = "Changelog";
            BugsButton.Content = "Bugs";
            NewsButton.Content = "• News";
            SettingsButton.Content = "Settings";
            Logger.Log(Info, "Completed News(object sender, RoutedEventArgs e)");
        }

        private void Settings(object sender, RoutedEventArgs e)
        {
            Logger.Log(Info, "Settings button clicked, navigating");
            pageHost.NavigationService.Navigate(Public.uncollapsedSettings);
            Logger.Log(Info, "Settings button navigation complete, setting navigation button text");
            pageHost.Visibility = Visibility.Visible;
            HomeButton.Content = "Home";
            ChangelogButton.Content = "Changelog";
            BugsButton.Content = "Bugs";
            NewsButton.Content = "News";
            SettingsButton.Content = "• Settings";
            Logger.Log(Info, "Completed Settings(object sender, RoutedEventArgs e)");
        }
        // End NavButton logic

        // Basic window logic
        private void Close(object sender, RoutedEventArgs e)
        {
            BasicLogic.HandleClose(launcherStatus, false, true, 1);
        }

        private void Minimize(object sender, RoutedEventArgs e)
        {
            Logger.Log(Info, "Setting WindowState to minimized");
            WindowState = WindowState.Minimized;
            Logger.Log(Info, "Completed Minimize(object sender, RoutedEventArgs e)");
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
            Logger.Log(Info, "cuttingEdge notice accepted, removing notice");
            AnimationHandler.FadeOut(cuttingEdgeNotice, 0.15);
            AnimationHandler.FadeOut(cuttingEdgeNoticeBlackout, 0.2);
            await Task.Delay(200);
            cuttingEdgeNotice.IsEnabled = false;
            cuttingEdgeNoticeBlackout.IsEnabled = false;
            cuttingEdgeNotice.Visibility = Visibility.Hidden;
            cuttingEdgeNoticeBlackout.Visibility = Visibility.Hidden;
            Logger.Log(Info, "Completed acceptCeNotice(object sender, RoutedEventArgs e)");
        }

        private async void loadJsonStrings()
        {
            Logger.Log(Info, "Loading JSON strings");
            try
            {
                if (Public.json.showCuttingEdgeNotice == true)
                {
                    Logger.Log(Info, "showCuttingEdgeNotice is enabled, showing notice");
                    await Task.Delay(250);
                    cuttingEdgeNotice.Visibility = Visibility.Visible;
                    cuttingEdgeNoticeBlackout.Visibility = Visibility.Visible;
                    cuttingEdgeNotice.IsEnabled = true;
                    cuttingEdgeNoticeBlackout.IsEnabled = true;
                    AnimationHandler.FadeIn(cuttingEdgeNotice, 0.2);
                    AnimationHandler.FadeIn(cuttingEdgeNoticeBlackout, 0.2);
                    Logger.Log(Info, "cuttingEdge notice shown");
                }
                if (Public.userPreferences.CollapseSidebar == true)
                {
                    Logger.Log(Info, "AGUserPreferences specifies CollapseSidebar, setting object positions and visibilities");
                    pageHost.Margin = new(88, 16, 56, 30);
                        AveryGame.Content = "AG";
                        AveryGame.Margin = new(5, 15, 0, 0);
                        AGSLogo.Margin = new(260, 0, 0, 0);
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
                    Logger.Log(Info, "Finished setting object positions and visibilities for AGUserPreferences' CollapseSidebar specification");
                }
                if (!Directory.Exists(Public.userPreferences.InstallPath))
                {
                    Logger.Log(Info, "InstallPath does not exist, creating");
                    JObject rss = JObject.Parse(File.ReadAllText(localAppData + "AGUserPreferences.apr"));
                    rss["InstallPath"] = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\";
                    File.WriteAllText(localAppData + "AGUserPreferences.apr", rss.ToString());
                    Public.userPreferences = JsonConvert.DeserializeObject<AGUserPreferences>(rss.ToString());
                    Logger.Log(Info, "InstallPath created, JSON value set as well");
                }
                if (File.Exists(Public.userPreferences.InstallPath + "\\AveryGameFinale.zip"))
                {
                    Logger.Log(Info, "Found 1 unfinished install, deleting");
                    File.Delete(Public.userPreferences.InstallPath + "\\AveryGameFinale.zip");
                    Logger.Log(Info, "Deleted 1 unfinished install");
                }
                Logger.Log(Info, "Completed loadJsonStrings()");
            }
            catch (Exception ex)
            {
                Logger.Log(Error, "Error loading JSON strings!");
                Logger.Log(Error, ex.Message.ToString());
                MessageBox.Show(ex.Message.Normalize().ToString());
                await Task.Delay(1000);
                Logger.Log(Info, "Attempting JSON string load again");
                loadJsonStrings();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Logger.Log(Info, "App window loaded, setting LauncherStatus to idle");
            launcherStatus = LauncherStatus.idle;
            Logger.Log(Info, "Completed Window_Loaded(object sender, RoutedEventArgs e)");
        }

        private void InitializePageHost()
        {
            Logger.Log(Info, "Navigating to all pages at once to avoid hitching");
            pageHost.NavigationService.Navigate(Public.uncollapsedHome);
            pageHost.NavigationService.Navigate(Public.uncollapsedBugs);
            pageHost.NavigationService.Navigate(Public.uncollapsedChangelog);
            pageHost.NavigationService.Navigate(Public.uncollapsedNews);
            pageHost.NavigationService.Navigate(Public.uncollapsedSettings);
            Logger.Log(Info, "Completed InitializePageHost()");
        }
        //End unique page logic
    }
}

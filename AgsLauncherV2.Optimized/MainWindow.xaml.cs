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
using Pastebin = PastebinAPI.Pastebin;
using System.Reflection;
using static AgsLauncherV2.Optimized.Services.Enums.LogTypeEnum;
using System.Windows.Interop;
using AgsLauncherV2.Optimized.Pages.Uncollapsed;

namespace AgsLauncherV2.Optimized
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        [DllImport("Kernel32")]
        private static extern void AllocConsole();
        private readonly string _localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\CuttingEdge\\";
        
        public MainWindow()
        {
            Avgl2CeVersion = "ags+void+3.0.2+kamo+cE?s=br";
            InitializeComponent();
            InitializeApplicationBase();
        }

        private async void InitializeApplicationBase()
        {
            await BasicLogic.CheckAppData();
            var assemblyConfigurationAttribute = typeof(MainWindow).Assembly.GetCustomAttribute<AssemblyConfigurationAttribute>();
            BuildConfiguration = assemblyConfigurationAttribute?.Configuration;
            if (BuildConfiguration == "Developer")
            {
                AllocConsole();
                Console.WriteLine("AgsLauncherV2.Optimized.MainWindow//OnApplicationLoad -- Build configuration is developer. \r\nIf you are not a developer, please delete this. \r\nAveryGame and it's developers are not liable for any damage done to your device.");
            }
            await AwaitInitializeLogger();
            InitializeVarsOnAppEntry();
            LoadJsonStrings();
        }

        private static Task AwaitInitializeLogger()
        {
            LogPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + $"\\AveryGame Launcher\\CuttingEdge\\Logs\\{DateTime.Now:dd-MM-yy on HH-mm-ss}.log";
            Logger.InitializeLogger();
            return Task.CompletedTask;
        }

        private void InitializePagesOnAppEntry()
        {
            Logger.Log(Info, "Setting public page variables to new instances of pages");
            UncollapsedSettings = new Settings();
            UncollapsedBugs = new Bugs();
            UncollapsedNews = new News();
            UncollapsedChangelog = new Changelog();
            UncollapsedHome = new Home();
            Public.MainWindow = this;
            Logger.Log(Info, "Completed InitializePagesOnAppEntry()");
        }

        private async void InitializeVarsOnAppEntry()
        {
            Logger.Log(Info, "Setting public variables to new instances of objects");
            Rpc.RpcLabel = WelcomeRpcLabel;
            Rpc.PfpImage = Pfp;
            Avgl2CeVersion = ReleaseString.Text;
            Pastebin.DevKey = "QY2g-xz1b7FqyReNDhuK0MdBw62ptzQY";
            PbUser = await Pastebin.LoginAsync("AveryGameCrashPad", "ICanSeeYouSkidder");
            var hWnd = new WindowInteropHelper(this).EnsureHandle();
            const DWMWINDOWATTRIBUTE attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
            var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
            DwmSetWindowAttribute(hWnd, attribute, ref preference, sizeof(uint));
            Logger.Log(Info, "Completed InitializeVarsOnAppEntry()");
        }


        // All NavButton logic
        private void Home(object sender, RoutedEventArgs e)
        {
            Logger.Log(Info, "Home button clicked, navigating");
            PageHost.NavigationService.Navigate(UncollapsedHome);
            Logger.Log(Info, "Home button navigation complete, setting navigation button text");
            PageHost.Visibility = Visibility.Visible;
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
            PageHost.NavigationService.Navigate(UncollapsedChangelog);
            Logger.Log(Info, "Changelog button navigation complete, setting navigation button text");
            PageHost.Visibility = Visibility.Visible;
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
            PageHost.NavigationService.Navigate(UncollapsedBugs);
            Logger.Log(Info, "Bugs button navigation complete, setting navigation button text");
            PageHost.Visibility = Visibility.Visible;
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
            PageHost.NavigationService.Navigate(UncollapsedNews);
            Logger.Log(Info, "News button navigation complete, setting navigation button text");
            PageHost.Visibility = Visibility.Visible;
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
            PageHost.NavigationService.Navigate(UncollapsedSettings);
            Logger.Log(Info, "Settings button navigation complete, setting navigation button text");
            PageHost.Visibility = Visibility.Visible;
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
            BasicLogic.HandleClose(LauncherStatus, false, true, 1);
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
        private async void AcceptCeNotice(object sender, RoutedEventArgs e)
        {
            Logger.Log(Info, "cuttingEdge notice accepted, removing notice");
            AnimationHandler.FadeOut(CuttingEdgeNotice, 0.15);
            AnimationHandler.FadeOut(CuttingEdgeNoticeBlackout, 0.2);
            await Task.Delay(200);
            CuttingEdgeNotice.IsEnabled = false;
            CuttingEdgeNoticeBlackout.IsEnabled = false;
            CuttingEdgeNotice.Visibility = Visibility.Hidden;
            CuttingEdgeNoticeBlackout.Visibility = Visibility.Hidden;
            Logger.Log(Info, "Completed acceptCeNotice(object sender, RoutedEventArgs e)");
        }

        private async void LoadJsonStrings()
        {
            Logger.Log(Info, "Loading JSON strings");
            try
            {
                if (Json.ShowCuttingEdgeNotice)
                {
                    Logger.Log(Info, "showCuttingEdgeNotice is enabled, showing notice");
                    await Task.Delay(250);
                    CuttingEdgeNotice.Visibility = Visibility.Visible;
                    CuttingEdgeNoticeBlackout.Visibility = Visibility.Visible;
                    CuttingEdgeNotice.IsEnabled = true;
                    CuttingEdgeNoticeBlackout.IsEnabled = true;
                    AnimationHandler.FadeIn(CuttingEdgeNotice, 0.2);
                    AnimationHandler.FadeIn(CuttingEdgeNoticeBlackout, 0.2);
                    Logger.Log(Info, "cuttingEdge notice shown");
                }
                if (UserPreferences.CollapseSidebar)
                {
                    Logger.Log(Info, "AGUserPreferences specifies CollapseSidebar, setting object positions and visibilities");
                    PageHost.Margin = new Thickness(88, 16, 56, 30);
                        AveryGame.Content = "AG";
                        AveryGame.Margin = new Thickness(5, 15, 0, 0);
                        AgsLogo.Margin = new Thickness(260, 0, 0, 0);
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
                if (!Directory.Exists(UserPreferences.InstallPath))
                {
                    Logger.Log(Info, "InstallPath does not exist, creating");
                    var rss = JObject.Parse(await File.ReadAllTextAsync(_localAppData + "AGUserPreferences.apr"));
                    rss["InstallPath"] = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\";
                    await File.WriteAllTextAsync(_localAppData + "AGUserPreferences.apr", rss.ToString());
                    UserPreferences = JsonConvert.DeserializeObject<AgUserPreferences>(rss.ToString());
                    Logger.Log(Info, "InstallPath created, JSON value set as well");
                }
                if (File.Exists(UserPreferences.InstallPath + "\\AveryGameFinale.zip"))
                {
                    Logger.Log(Info, "Found 1 unfinished install, deleting");
                    File.Delete(UserPreferences.InstallPath + "\\AveryGameFinale.zip");
                    Logger.Log(Info, "Deleted 1 unfinished install");
                }
                Logger.Log(Info, "Completed loadJsonStrings()");
            }
            catch (Exception ex)
            {
                Logger.Log(Error, "Error loading JSON strings!");
                Logger.Log(Error, ex.Message);
                MessageBox.Show(ex.Message.Normalize());
                await Task.Delay(1000);
                Logger.Log(Info, "Attempting JSON string load again");
                LoadJsonStrings();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Logger.Log(Info, "App window loaded, setting LauncherStatus to idle");
            PageHost.Visibility = Visibility.Hidden;
            InitializePagesOnAppEntry();
            InitializePageHost();
            Rpc.InitRpc();
            LauncherStatus = LauncherStatusEnum.Idle;
            Logger.Log(Info, "Completed Window_Loaded(object sender, RoutedEventArgs e)");
        }

        private void InitializePageHost()
        {
            Logger.Log(Info, "Navigating to all pages at once to avoid hitching");
            PageHost.NavigationService.Navigate(UncollapsedHome);
            PageHost.NavigationService.Navigate(UncollapsedBugs);
            PageHost.NavigationService.Navigate(UncollapsedChangelog);
            PageHost.NavigationService.Navigate(UncollapsedNews);
            PageHost.NavigationService.Navigate(UncollapsedSettings);
            Logger.Log(Info, "Completed InitializePageHost()");
        }

        // The enum flag for DwmSetWindowAttribute's second parameter, which tells the function what attribute to set.
        // Copied from dwmapi.h
        public enum DWMWINDOWATTRIBUTE
        {
            DWMWA_WINDOW_CORNER_PREFERENCE = 33
        }

        // The DWM_WINDOW_CORNER_PREFERENCE enum for DwmSetWindowAttribute's third parameter, which tells the function
        // what value of the enum to set.
        // Copied from dwmapi.h
        public enum DWM_WINDOW_CORNER_PREFERENCE
        {
            DWMWCP_DEFAULT = 0,
            DWMWCP_DONOTROUND = 1,
            DWMWCP_ROUND = 2,
            DWMWCP_ROUNDSMALL = 3
        }

        // Import dwmapi.dll and define DwmSetWindowAttribute in C# corresponding to the native function.
        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        private static extern void DwmSetWindowAttribute(IntPtr hwnd,
                                                         DWMWINDOWATTRIBUTE attribute,
                                                         ref DWM_WINDOW_CORNER_PREFERENCE pvAttribute,
                                                         uint cbAttribute);
        //End unique page logic
    }
}

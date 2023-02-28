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

namespace AgsLauncherV2.Optimized.Services
{
    internal class BasicLogic
    {
        public static void HandleClose(LauncherStatus windowStatus, [Optional] bool force, [Optional] bool useExitCode, [Optional] int exitCode)
        {
            if (force)
            {
                Environment.Exit(2);
            }
            if (windowStatus == LauncherStatus.idle)
            {
                if (force)
                {
                    if (useExitCode)
                    {
                        _exitCode = exitCode;
                    }
                    
                    Environment.Exit(_exitCode);
                }
                else
                {
                    if (useExitCode)
                    {
                        _exitCode = exitCode;
                    }
                    
                    Environment.Exit(_exitCode);
                }
            }
            if (windowStatus == LauncherStatus.downloading)
            {
                MessageBoxResult result = MessageBox.Show("AveryGame is not done downloading, and exiting will corrupt the download. Are you sure you want to exit?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    HandleClose(LauncherStatus.idle, true, true, 2);
                }
                else if (result == MessageBoxResult.No)
                {
                    
                }
            }
        }
        
        public static void CheckAppData()
        {
            if (!Directory.Exists(localAppData))
            {
                CreateAppData();
                CreateUserPreferences();
                DownloadAppData();
            }
            else if (Directory.Exists(localAppData))
            {
                DownloadAppData();
            }
        }
        
        public static void DownloadAppData()
        {
            WebClient wc = new();
            Public.clientStrings = wc.DownloadString(apiBase + "clientStrings.acs");
            Public.json = JsonConvert.DeserializeObject<AGCloud>(Public.clientStrings);
            Public.userPreferences = JsonConvert.DeserializeObject<AGUserPreferences>(File.ReadAllText(localAppData + "AGUserPreferences.apr"));
        }

        public static void CreateUserPreferences()
        {
            string json = JsonConvert.SerializeObject(new AGUserPreferences
            {
                CollapseSidebar = false,
                Arguments = "",
                InstallPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\",
                Ag1InstallPath = "null",
                Ag2InstallPath = "null"
            });
            File.WriteAllText(localAppData + "AGUserPreferences.apr", json);
        }

        public static void CreateAppData()
        {
            Directory.CreateDirectory(localAppData);
        }
        
        public static void LaunchAg1()
        {
            Process runningAg1proc = new Process();
            runningAg1proc.StartInfo.FileName = userPreferences.Ag1InstallPath;
            runningAg1proc.StartInfo.Arguments = userPreferences.Arguments;
            runningAg1proc.StartInfo.WorkingDirectory = userPreferences.InstallPath + "\\Avery Game\\Finale\\";
            runningAg1proc.Start();
        }

        private static void DownloadAg1CompletedCallback(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            try
            {
                ag1LaunchLabel.Content = "Extracting Avery Game";
                string gzip = "AveryGameFinale.zip";
                ZipFile.ExtractToDirectory(Public.userPreferences.InstallPath + "\\" + gzip, Public.userPreferences.InstallPath + "\\Avery Game\\");
                File.Delete(Public.userPreferences.InstallPath + "\\" + gzip);
                JObject rss = JObject.Parse(File.ReadAllText(localAppData + "AGUserPreferences.apr"));
                rss["Ag1InstallPath"] = Public.userPreferences.InstallPath + "\\Avery Game\\Finale\\AveryGame.exe";
                File.WriteAllText(localAppData + "AGUserPreferences.apr", rss.ToString());
                Public.userPreferences = JsonConvert.DeserializeObject<AGUserPreferences>(rss.ToString());
                ag1LaunchButton.IsEnabled = true;
                ag1LaunchLabel.Content = "Launch Avery Game";
                Public.uncollapsedHome.bAg1Installed = true;
                new ToastContentBuilder()
                    .AddArgument("action", "viewConversation")
                    .AddArgument("conversationId", 9813)
                    .AddText("Avery Game download status")
                    .AddText("Avery Game has finished downloading!")
                    .Show();
                Enums.launcherStatus = LauncherStatus.idle;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error finishing download: {ex}");
            }
        }
        public static void DownloadAg1()
        {
            try
            {
                Enums.launcherStatus = LauncherStatus.downloading;
                ag1LaunchButton.IsEnabled = false;
                string gzip = "AveryGameFinale.zip";
                WebClient webclient = new();
                new ToastContentBuilder()
                    .AddArgument("action", "viewConversation")
                    .AddArgument("conversationId", 9813)
                    .AddText("Avery Game download status")
                    .AddText("Download started...")
                    .Show();
                webclient.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(DownloadAg1CompletedCallback);
                webclient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(webclientDownloadProgressChanged);
                webclient.DownloadFileAsync(new Uri("https://www.googleapis.com/drive/v3/files/1FM2BJK6M8xd2y3IIG2n6UwCwRVJ9Qwvp?alt=media&key=AIzaSyD3hsuSxEFnxZkgadbUSPt_iyx8qJ4lwWQ"), Public.userPreferences.InstallPath + "\\" + gzip);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error finishing download: {ex.GetBaseException()}");
            }
        }
        public static void webclientDownloadProgressChanged(Object sender, DownloadProgressChangedEventArgs e)
        {
            ag1LaunchLabel.Content = "Installing Avery Game - " + e.ProgressPercentage.ToString() + "%";
            double progressAsDouble = (double)e.ProgressPercentage / 100;
            Public.uncollapsedHome.Ag1ImageTextureFilled.Opacity = progressAsDouble;
        }
        
        public static System.Windows.Controls.Label ag1LaunchLabel;
        public static System.Windows.Controls.Button ag1LaunchButton;

        public static bool IsWindowOpen<T>(string name = "") where T : Window
        {
            return string.IsNullOrEmpty(name)
               ? Application.Current.Windows.OfType<T>().Any()
               : Application.Current.Windows.OfType<T>().Any(w => w.Name.Equals(name));
        }

        private static int _exitCode = -1;
        private static string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\CuttingEdge\\";
    }
}

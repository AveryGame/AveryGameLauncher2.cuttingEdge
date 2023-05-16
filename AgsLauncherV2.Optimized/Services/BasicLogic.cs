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

using System.Net.Http;

namespace AgsLauncherV2.Optimized.Services
{
    internal class BasicLogic
    {
        public static void HandleClose(LauncherStatus windowStatus, [Optional] bool force, [Optional] bool useExitCode, [Optional] int exitCode)
        {
            Logger.Log(LogType.Warn, $"HandleClose called, LauncherStatus = {windowStatus}, Force? = {force}, Use exit code? = {useExitCode} Exit code = {exitCode}");
            if (force)
            {
                Logger.Log(LogType.Info, "Close was forced, shutting down immediately with exit code of 2");
                Environment.Exit(2);
            }
            if (windowStatus == LauncherStatus.idle)
            {
                Logger.Log(LogType.Info, "Launcher status is idle");
                if (force)
                {
                    if (useExitCode)
                    {
                        _exitCode = exitCode;
                    }
                    Logger.Log(LogType.Warn, $"Close was forced, shutting down immediately with exit code of {_exitCode}");
                    Environment.Exit(_exitCode);
                }
                else
                {
                    if (useExitCode)
                    {
                        _exitCode = exitCode;
                    }
                    Logger.Log(LogType.Info, $"Close was not forced, shutting down with exit code of {_exitCode}");
                    Environment.Exit(_exitCode);
                }
            }
            if (windowStatus == LauncherStatus.downloading)
            {
                Logger.Log(LogType.Warn, "Prevented closing, LauncherStatus is downloading");
                MessageBoxResult result = MessageBox.Show("AveryGame is not done downloading, and exiting will corrupt the download. Are you sure you want to exit?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    Logger.Log(LogType.Warn, "User confirmed force shutdown during download, immediately shutting down with exit code of 2");
                    HandleClose(LauncherStatus.idle, true, true, 2);
                }
                else if (result == MessageBoxResult.No)
                {
                    Logger.Log(LogType.Info, "User denied force shutdown during download");
                }
            }
        }
        
        public static void CheckAppData()
        {
            Logger.Log(LogType.Info, "Checking AppData directory");
            if (!Directory.Exists(localAppData))
            {
                Logger.Log(LogType.Info, "AppData directory does not exist! Creating");
                CreateAppData();
                CreateUserPreferences();
                DownloadAppData();
            }
            else if (Directory.Exists(localAppData))
            {
                Logger.Log(LogType.Info, "AppData directory exists, downloading JSON strings");
                DownloadAppData();
            }
        }
        
        public static void DownloadAppData()
        {
            Logger.Log(LogType.Info, "Initializing WebClient, preparing to download JSON strings");
            WebClient wc = new();
            Public.clientStrings = wc.DownloadString(apiBase + "clientStrings.acs");
            Public.json = JsonConvert.DeserializeObject<AGCloud>(Public.clientStrings);
            Public.userPreferences = JsonConvert.DeserializeObject<AGUserPreferences>(File.ReadAllText(localAppData + "AGUserPreferences.apr"));
            Logger.Log(LogType.Info, "Completed DownloadAppData(), JSON strings downloaded");
        }

        public static void CreateUserPreferences()
        {
            Logger.Log(LogType.Info, "Creating AGUserPreferences.apr");
            string json = JsonConvert.SerializeObject(new AGUserPreferences
            {
                CollapseSidebar = false,
                Arguments = "",
                InstallPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\",
                Ag1InstallPath = "null",
                Ag2InstallPath = "null"
            });
            Logger.Log(LogType.Info, "Writing serialized JSON to AGUserPreferences.apr");
            File.WriteAllText(localAppData + "AGUserPreferences.apr", json);
            Logger.Log(LogType.Info, "Completed CreateUserPreferences()");
        }

        public static void CreateAppData()
        {
            Directory.CreateDirectory(localAppData);
        }
        
        public static void LaunchAg1()
        {
            Logger.Log(LogType.Info, "Launching Ag1");
            Process runningAg1proc = new Process();
            runningAg1proc.StartInfo.FileName = userPreferences.Ag1InstallPath;
            runningAg1proc.StartInfo.Arguments = userPreferences.Arguments;
            runningAg1proc.StartInfo.WorkingDirectory = userPreferences.InstallPath + "\\Avery Game\\Finale\\";
            runningAg1proc.Start();
            Logger.Log(LogType.Info, "Completed LaunchAg1()");
        }

        private static void DownloadAg1CompletedCallback(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Logger.Log(LogType.Info, "Ag1 download completed, running callback");
            try
            {
                ag1LaunchLabel.Content = "Extracting Avery Game";
                string gzip = "AveryGameFinale.zip";
                Logger.Log(LogType.Info, "Extracting Ag1");
                ZipFile.ExtractToDirectory(Public.userPreferences.InstallPath + "\\" + gzip, Public.userPreferences.InstallPath + "\\Avery Game\\");
                Logger.Log(LogType.Info, "Deleting compressed Ag1");
                File.Delete(Public.userPreferences.InstallPath + "\\" + gzip);
                JObject rss = JObject.Parse(File.ReadAllText(localAppData + "AGUserPreferences.apr"));
                Logger.Log(LogType.Info, "Setting AGUserPreferences Ag1InstallPath to exe location");
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
                Logger.Log(LogType.Info, "Completed DownloadAg1CompletedCallback(object sender, System.ComponentModel.AsyncCompletedEventArgs e), set LauncherStatus to idle");
            }
            catch (Exception ex)
            {
                Logger.Log(LogType.Error, "Error finishing DownloadAg1CompletedCallback(object sender, System.ComponentModel.AsyncCompletedEventArgs e)");
                Logger.Log(LogType.Error, ex.Message.ToString());
                MessageBox.Show($"Error finishing download: {ex}");
            }
        }
        public static void DownloadAg1()
        {
            Logger.Log(LogType.Info, "DownloadAg1 called");
            try
            {
                Logger.Log(LogType.Info, "Setting LauncherStatus to downloading, disabling button, setting gzip string, initializing WebClient, sending toast");
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
                Logger.Log(LogType.Info, "Setting callbacks for Ag1 download");
                webclient.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(DownloadAg1CompletedCallback);
                webclient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(webclientDownloadProgressChanged);
                Logger.Log(LogType.Info, "Downloading Ag1 from GoogleAPIs");
                webclient.DownloadFileAsync(new Uri("https://www.googleapis.com/drive/v3/files/1FM2BJK6M8xd2y3IIG2n6UwCwRVJ9Qwvp?alt=media&key=AIzaSyD3hsuSxEFnxZkgadbUSPt_iyx8qJ4lwWQ"), Public.userPreferences.InstallPath + "\\" + gzip);
            }
            catch (Exception ex)
            {
                Logger.Log(LogType.Error, "Error finishing DownloadAg1()");
                Logger.Log(LogType.Error, ex.Message.ToString());
                MessageBox.Show($"Error finishing download: {ex.GetBaseException()}");
            }
        }
        public static double cachedLast;
        public static void webclientDownloadProgressChanged(Object sender, DownloadProgressChangedEventArgs e)
        {
            ag1LaunchLabel.Content = "Installing Avery Game - " + e.ProgressPercentage.ToString() + "%";
            double progressAsDouble = (double)e.ProgressPercentage / 100;
            if (progressAsDouble != cachedLast)
            {
                Public.uncollapsedHome.Ag1ImageTextureFilled.Opacity = progressAsDouble;
                Logger.Log(LogType.Info, $"Ag1ImageTextureFilled opacity set to {progressAsDouble}");
            }
            cachedLast = progressAsDouble;
        }

        public static async void CrashpadHandle()
        {
            //https://ptb.discord.com/api/webhooks/1096698300889038868/5vzV6qKaO9nctZhoksHzWlcd2sIx9DVqqnrIBvqtlRtZt6Pv3UmYW6qc2IArO2txj-NQ
            PastebinAPI.Paste errorPaste = await PbUser.CreatePasteAsync(File.ReadAllText(Public.LogPath), "AveryGameLauncher2.cuttingEdge crashpad instance at " + DateTime.Now);
            HttpClient client = new HttpClient();
            var values = new Dictionary<string, string>
                    {
                        { "name", "AveryGame Launcher Logging Webhook" },
                        { "type", "1" },
                        { "channel_id", "975664139517194250" },
                        { "token", "5vzV6qKaO9nctZhoksHzWlcd2sIx9DVqqnrIBvqtlRtZt6Pv3UmYW6qc2IArO2txj-NQ" },
                        { "guild_id", "907015974669131786" },
                        { "application_id", "null" },
                        { "user", "AveryGame Launcher Logging Webhook" },
                        { "content", $"New crashpad instance created <t:{((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds()}:R> {errorPaste.Url}"}
                    };
            var content = new FormUrlEncodedContent(values);
            await client.PostAsync("https://ptb.discord.com/api/webhooks/1096698300889038868/5vzV6qKaO9nctZhoksHzWlcd2sIx9DVqqnrIBvqtlRtZt6Pv3UmYW6qc2IArO2txj-NQ", content);
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

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
    internal abstract class BasicLogic
    {
        public static void HandleClose(LauncherStatusEnum windowStatus, [Optional] bool force, [Optional] bool useExitCode, [Optional] int exitCode)
        {
            if (force)
            {
                Logger.Log(LogTypeEnum.Info, "Close was forced, shutting down immediately with exit code of 2");
                Environment.Exit(2);
            }
            switch (windowStatus)
            {
                case LauncherStatusEnum.Idle:
                case LauncherStatusEnum.Intializing:
                case LauncherStatusEnum.WaitingRpc:
                case LauncherStatusEnum.GameLaunched:
                default:
                    Environment.Exit(ExitCode);
                    break;
                case LauncherStatusEnum.Downloading:
                case LauncherStatusEnum.Updating:
                    Logger.Log(LogTypeEnum.Warn, "Prevented closing, LauncherStatus is downloading");
                    var result = MessageBox.Show("AveryGame is not done downloading, and exiting will corrupt the download. Are you sure you want to exit?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                        case MessageBoxResult.OK:
                            Logger.Log(LogTypeEnum.Warn, "User confirmed force shutdown during download, immediately shutting down with exit code of 2");
                            HandleClose(LauncherStatusEnum.Idle, true, true, 2);
                            break;
                        case MessageBoxResult.No:
                        case MessageBoxResult.None:
                        case MessageBoxResult.Cancel:
                        default:
                            Logger.Log(LogTypeEnum.Info, "User denied force shutdown during download");
                            break;
                    }
                    break;
            }
        }
        
        public static async Task CheckAppData()
        {
            Logger.Log(LogTypeEnum.Info, "[CheckAppData()]: Running JSON-related application initialization checks now [EnsureAppDataExists(), EnsureUserPreferencesExists(), DownloadAppDataHandler()]");
            await EnsureAppDataExists();
            await EnsureUserPreferencesExists();
            await DownloadAppDataHandler();
            Logger.Log(LogTypeEnum.Info, "Completed CheckAppData()");
            return;
        }

        private static Task DownloadAppDataHandler()
        {
            Logger.Log(LogTypeEnum.Info, $"Downloading JSON from API. Endpoint: {ApiBase}/clientStrings.acs");
            ClientStrings = new WebClient().DownloadString(ApiBase + "clientStrings.acs");
            Logger.Log(LogTypeEnum.Info, "JSON strings downloaded, deserializing into Json field using AgCloud class");
            Json = JsonConvert.DeserializeObject<AgCloud>(ClientStrings);
            Logger.Log(LogTypeEnum.Info, "Completed DownloadAppDataHandler(), JSON strings downloaded and deserialized");
            return Task.CompletedTask;
        }

        private static Task EnsureAppDataExists()
        {
            Logger.Log(LogTypeEnum.Info, $"Ensuring AppData directory ({LocalAppData}) exists");

            if (Directory.Exists(LocalAppData)) return Task.CompletedTask;
            Logger.Log(LogTypeEnum.Info, "AppData directory does not exist! Creating");
            Directory.CreateDirectory(LocalAppData);

            return Task.CompletedTask;
        }

        private static async Task EnsureUserPreferencesExists()
        {
            if (File.Exists($"{LocalAppData}AGUserPreferences.apr") &&
                new FileInfo($"{LocalAppData}AGUserPreferences.apr").Length > 50)
            {
                UserPreferences = JsonConvert.DeserializeObject<AgUserPreferences>(await File.ReadAllTextAsync($"{LocalAppData}AGUserPreferences.apr"));
                return;
            }
            
            Logger.Log(LogTypeEnum.Info, $"Did not find file {LocalAppData}AGUserPreferences.apr or it was invalid! Creating and serializing json with AgUserPreferences class now");

            var serializedJson = JsonConvert.SerializeObject(new AgUserPreferences
            {
                CollapseSidebar = false, Arguments = "",
                InstallPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                              "\\AveryGame Launcher\\Avery Game",
                Ag1InstallPath = "null", Ag2InstallPath = "null"
            });
            
            await File.WriteAllTextAsync($"{LocalAppData}AGUserPreferences.apr", serializedJson);
            UserPreferences = JsonConvert.DeserializeObject<AgUserPreferences>(serializedJson);
            Logger.Log(LogTypeEnum.Info, "Completed EnsureUserPreferencesExists()");
        }
        
        public static void LaunchAg1()
        {
            Logger.Log(LogTypeEnum.Info, "Launching Ag1");
            var runningAg1Proc = new Process();
            runningAg1Proc.StartInfo.FileName = UserPreferences.Ag1InstallPath;
            runningAg1Proc.StartInfo.Arguments = UserPreferences.Arguments;
            runningAg1Proc.StartInfo.WorkingDirectory = UserPreferences.InstallPath + "Avery Game\\Finale\\";
            runningAg1Proc.Start();
            Logger.Log(LogTypeEnum.Info, "Completed LaunchAg1()");
        }

        private static void WebClientOnDownloadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Logger.Log(LogTypeEnum.Info, "Ag1 download completed, running callback");
            try
            {
                Ag1LaunchLabel.Content = "Extracting Avery Game";
                Logger.Log(LogTypeEnum.Info, "Extracting Ag1");
                ZipFile.ExtractToDirectory(UserPreferences.InstallPath + "AveryGameFinale.zip", UserPreferences.InstallPath + "Avery Game\\");
                Logger.Log(LogTypeEnum.Info, "Deleting compressed Ag1");
                File.Delete(UserPreferences.InstallPath + "" + "AveryGameFinale.zip");
                var rss = JObject.Parse(File.ReadAllText( LocalAppData + "AGUserPreferences.apr"));
                Logger.Log(LogTypeEnum.Info, "Setting AGUserPreferences Ag1InstallPath to exe location");
                rss["Ag1InstallPath"] = UserPreferences.InstallPath + "Avery Game\\Finale\\AveryGame.exe";
                File.WriteAllText(LocalAppData + "AGUserPreferences.apr", rss.ToString());
                UserPreferences = JsonConvert.DeserializeObject<AgUserPreferences>(rss.ToString());
                Ag1LaunchButton.IsEnabled = true;
                Ag1LaunchLabel.Content = "Launch Avery Game";
                UncollapsedHome.BAg1Installed = true;
                new ToastContentBuilder()
                    .AddArgument("action", "viewConversation")
                    .AddArgument("conversationId", 9813)
                    .AddText("Avery Game download status")
                    .AddText("Avery Game has finished downloading!")
                    .Show();
                LauncherStatus = LauncherStatusEnum.Idle;
                Logger.Log(LogTypeEnum.Info, "Completed DownloadAg1CompletedCallback(object sender, System.ComponentModel.AsyncCompletedEventArgs e), set LauncherStatus to idle");
            }
            catch (Exception ex)
            {
                Logger.Log(LogTypeEnum.Error, "Error finishing DownloadAg1CompletedCallback(object sender, System.ComponentModel.AsyncCompletedEventArgs e)");
                Logger.Log(LogTypeEnum.Error, ex.Message);
                MessageBox.Show($"Error finishing download: {ex}");
            }
        }
        public static void DownloadAg1()
        {
            Logger.Log(LogTypeEnum.Info, "DownloadAg1 called");
            try
            {
                Logger.Log(LogTypeEnum.Info, "Setting LauncherStatus to downloading, disabling button, setting gzip string, initializing WebClient, sending toast");
                LauncherStatus = LauncherStatusEnum.Downloading;
                Ag1LaunchButton.IsEnabled = false;
                WebClient webclient = new();
                new ToastContentBuilder()
                    .AddArgument("action", "viewConversation")
                    .AddArgument("conversationId", 9813)
                    .AddText("Avery Game download status")
                    .AddText("Download started...")
                    .Show();
                Logger.Log(LogTypeEnum.Info, "Setting callbacks for Ag1 download");
                webclient.DownloadFileCompleted += WebClientOnDownloadCompleted;
                webclient.DownloadProgressChanged += WebClientOnDownloadProgressChanged;
                Logger.Log(LogTypeEnum.Info, "Downloading Ag1 from GoogleAPIs");
                webclient.DownloadFileAsync(new Uri("https://www.googleapis.com/drive/v3/files/1FM2BJK6M8xd2y3IIG2n6UwCwRVJ9Qwvp?alt=media&key=AIzaSyD3hsuSxEFnxZkgadbUSPt_iyx8qJ4lwWQ"), UserPreferences.InstallPath + "\\" + "AveryGameFinale.zip");
            }
            catch (Exception ex)
            {
                Logger.Log(LogTypeEnum.Error, $"Error finishing DownloadAg1()\r\n{ex.Message}");
                MessageBox.Show($"Error finishing download: {ex.GetBaseException()}");
            }
        }
        
        //you're welcome, jake
        private static double _cachedLast;
        private static void WebClientOnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Ag1LaunchLabel.Content = "Installing Avery Game - " + e.ProgressPercentage + "%";
            var progressAsDouble = (double)e.ProgressPercentage / 100;
            if (progressAsDouble != _cachedLast)
            {
                UncollapsedHome.Ag1ImageTextureFilled.Opacity = progressAsDouble;
                Logger.Log(LogTypeEnum.Info, $"Ag1ImageTextureFilled opacity set to {progressAsDouble}");
            }
            _cachedLast = progressAsDouble;
        }

        public static async void CrashpadHandle() => await new HttpClient().PostAsync("https://ptb.discord.com/api/webhooks/1096698300889038868/5vzV6qKaO9nctZhoksHzWlcd2sIx9DVqqnrIBvqtlRtZt6Pv3UmYW6qc2IArO2txj-NQ", new FormUrlEncodedContent(new Dictionary<string, string> { { "name", "AveryGame Launcher Logging Webhook" }, { "type", "1" }, { "channel_id", "975664139517194250" }, { "token", "5vzV6qKaO9nctZhoksHzWlcd2sIx9DVqqnrIBvqtlRtZt6Pv3UmYW6qc2IArO2txj-NQ" }, { "guild_id", "907015974669131786" }, { "application_id", "null" }, { "user", "AveryGame Launcher Logging Webhook" }, { "content", $"New crashpad instance created <t:{((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds()}:R> {(await PbUser.CreatePasteAsync(await File.ReadAllTextAsync(LogPath), "AveryGameLauncher2.cuttingEdge crashpad instance at " + DateTime.Now)).Url}"} }));
        
        public static bool IsWindowOpen<T>(string name = "") where T : Window
        {
            return string.IsNullOrEmpty(name)
               ? Application.Current.Windows.OfType<T>().Any()
               : Application.Current.Windows.OfType<T>().Any(w => w.Name.Equals(name));
        }

        public static System.Windows.Controls.Label Ag1LaunchLabel;
        public static System.Windows.Controls.Button Ag1LaunchButton;
        private const int ExitCode = -1;
        private static readonly string LocalAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\CuttingEdge\\";
    }
}

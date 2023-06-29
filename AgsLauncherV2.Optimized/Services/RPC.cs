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
    internal abstract class Rpc
    {
        private static DiscordRpcClient _client;
        public static System.Windows.Controls.Label RpcLabel;
        public static Image PfpImage;
        private static bool _bIsRpcInsured;
        public static void InitRpc()
        {
            Logger.Log(LogTypeEnum.Info, "InitRPC called");
            _client = new DiscordRpcClient("1023932512612925501");
            _client.Logger = new ConsoleLogger() { Level = LogLevel.Trace };
            _client.OnPresenceUpdate += (sender, e) =>
            {
                Console.WriteLine("[AVGL2CE]: Recieved presence update from Discord.");
            };
            _client.OnConnectionEstablished += (sender, e) =>
            {
                Console.WriteLine("[AVGL2CE]: Connected to Discord.");
            };
            EnsurePresenceConnection();
            _client.OnConnectionFailed += (sender, e) =>
            {
                _bIsRpcInsured = false;
                Logger.Log(LogTypeEnum.Error, "RPC failed to initialize, immediately halting execution");
                return;
            };
            _client.OnReady += (sender, e) =>
            {
                Console.WriteLine("[AVGL2CE]: RPC ready.");
                Application.Current.Dispatcher.Invoke(RichPresenceConnectionSuccess, System.Windows.Threading.DispatcherPriority.ContextIdle);
            };
            Logger.Log(LogTypeEnum.Info, "Set connection status events, initializing client.");
            _client.Initialize();
            Logger.Log(LogTypeEnum.Info, "Successfully initialized RPC client, setting presence");
            _client.SetPresence(new RichPresence()
            {
                Details = "On the home page",
                Assets = new Assets
                {
                    LargeImageKey = "ag2cover",
                    LargeImageText = Public.MainWindow.ReleaseString.Text.Split('?')[0],
                    SmallImageKey = "",
                    SmallImageText = ""
                },
                Buttons = new[]
                {
                    new DiscordRPC.Button
                    {
                        Label = "Join Server",
                        Url = "https://discord.gg/K5SmweCMNr"
                    }
                }
            });
            Logger.Log(LogTypeEnum.Info, "Successfully set presence, appending start time to presence");
            
            _client.UpdateStartTime();
            Logger.Log(LogTypeEnum.Info, "Completed InitRPC()");
        }

        private static async void EnsurePresenceConnection()
        {
            Logger.Log(LogTypeEnum.Info, "EnsurePresenceConnection() called for the first time");
            await Task.Delay(15000);
            for (; ; )
            {
                if (_bIsRpcInsured)
                {
                    Logger.Log(LogTypeEnum.Info, "bIsRPCEnsured is true, waiting 15 seconds to check again");
                    await Task.Delay(15000);
                }
                else
                {
                    Logger.Log(LogTypeEnum.Warn, "bIsRPCEnsured is false, attempting to reconnect to RPC service");
                    InitRpc();
                }
            }
        }

        private static void RichPresenceConnectionSuccess()
        {
            Logger.Log(LogTypeEnum.Info, $"Presence fully connected for user {_client.CurrentUser.Username}#{_client.CurrentUser.Discriminator:0000}.");
            Logger.Log(LogTypeEnum.Info, "Rich presence connection was successful, setting PFP, name, and bIsRPCEnsured");
            _bIsRpcInsured = true;
            RpcLabel.Content = "Welcome, " + _client.CurrentUser.Username + "!";
            BitmapImage pfpBmp = new();
            pfpBmp.BeginInit();
            pfpBmp.UriSource = new Uri(_client.CurrentUser.GetAvatarURL(User.AvatarFormat.PNG, User.AvatarSize.x64));
            pfpBmp.EndInit();
            PfpImage.Source = pfpBmp;
            Logger.Log(LogTypeEnum.Info, "Completed RichPresenceConnectionSuccess()");
        }

        public static void CallRPCUpdateOnPageChange(string pageName)
        {
            Logger.Log(LogTypeEnum.Info, "Page changed, updating presence");
            _client.SetPresence(new RichPresence()
            {
                Details = "Browsing the " + pageName + " page",
                Assets = new Assets()
                {
                    LargeImageKey = "ag2cover",
                    LargeImageText = Public.MainWindow.ReleaseString.Text.Split('?')[0],
                    SmallImageKey = "",
                    SmallImageText = ""
                },
                Buttons = new[]
                {
                    new DiscordRPC.Button()
                    {
                        Label = "Join Server",
                        Url = "https://discord.gg/K5SmweCMNr"
                    }
                }
            });
            Logger.Log(LogTypeEnum.Info, "Updated presence, appending timer");
            _client.UpdateStartTime();
            Logger.Log(LogTypeEnum.Info, "Completed CallRPCUpdateOnPageChange(string pageName)");
        }
    }
}

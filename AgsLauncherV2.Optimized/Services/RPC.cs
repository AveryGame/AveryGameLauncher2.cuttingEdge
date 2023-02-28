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
    internal class RPC
    {
        public static DiscordRpcClient client;
        public static System.Windows.Controls.Label rpcLabel;
        public static Image pfpImage;
        internal static bool bIsRPCEnsured;
        public static void InitRPC()
        {
            Logger.Log(LogType.Info, "InitRPC called");
            client = new("1023932512612925501");
            client.Logger = new ConsoleLogger() { Level = LogLevel.Trace };
            client.OnPresenceUpdate += (sender, e) =>
            {
                Console.WriteLine("[AVGL2CE]: Recieved presence update from Discord.");
                Application.Current.Dispatcher.Invoke(RichPresenceConnectionSuccess, System.Windows.Threading.DispatcherPriority.ContextIdle);
            };
            client.OnConnectionEstablished += (sender, e) =>
            {
                Console.WriteLine("[AVGL2CE]: Connected to Discord.");
            };
            EnsurePresenceConnection();
            client.OnConnectionFailed += (sender, e) =>
            {
                bIsRPCEnsured = false;
                return;
            };
            Logger.Log(LogType.Info, "Set connection status events, initializing client.");
            client.Initialize();
            Logger.Log(LogType.Info, "Successfully initialized RPC client, setting presence");
            client.SetPresence(new()
            {
                Details = "On the home page",
                Assets = new Assets()
                {
                    LargeImageKey = "ag2cover",
                    LargeImageText = Public.mainWindow.ReleaseString.Text.Split('?')[0],
                    SmallImageKey = "",
                    SmallImageText = ""
                },
                Buttons = new DiscordRPC.Button[]
                {
                    new DiscordRPC.Button()
                    {
                        Label = "Join Server",
                        Url = "https://discord.gg/K5SmweCMNr"

                    }
                }
            });
            Logger.Log(LogType.Info, "Successfully set presence, appending start time to presence");
            
            client.UpdateStartTime();
            Logger.Log(LogType.Info, "Completed InitRPC()");
        }

        private static async void EnsurePresenceConnection()
        {
            Logger.Log(LogType.Info, "EnsurePresenceConnection() called for the first time");
            await Task.Delay(15000);
            for (; ; )
            {
                if (bIsRPCEnsured)
                {
                    Logger.Log(LogType.Info, "bIsRPCEnsured is true, waiting 15 seconds to check again");
                    await Task.Delay(15000);
                }
                else
                {
                    Logger.Log(LogType.Info, "bIsRPCEnsured is false, attempting to reconnect to RPC service");
                    InitRPC();
                }
            }
        }

        public static void RichPresenceConnectionSuccess()
        {
            Logger.Log(LogType.Info, $"Presence fully connected for user {client.CurrentUser.Username}#{client.CurrentUser.Discriminator.ToString("0000")}.");
            Logger.Log(LogType.Info, "Rich presence connection was successful, setting PFP, name, and bIsRPCEnsured");
            bIsRPCEnsured = true;
            rpcLabel.Content = "Welcome, " + client.CurrentUser.Username + "!";
            BitmapImage pfpBmp = new();
            pfpBmp.BeginInit();
            pfpBmp.UriSource = new(client.CurrentUser.GetAvatarURL(User.AvatarFormat.PNG, User.AvatarSize.x16));
            pfpBmp.EndInit();
            pfpImage.Source = pfpBmp;
            Logger.Log(LogType.Info, "Completed RichPresenceConnectionSuccess()");
        }

        public static void CallRPCUpdateOnPageChange(string pageName)
        {
            Logger.Log(LogType.Info, "Page changed, updating presence");
            client.SetPresence(new RichPresence()
            {
                Details = "Browsing the " + pageName + " page",
                Assets = new()
                {
                    LargeImageKey = "ag2cover",
                    LargeImageText = Public.mainWindow.ReleaseString.Text.Split('?')[0],
                    SmallImageKey = "",
                    SmallImageText = ""
                },
                Buttons = new DiscordRPC.Button[]
                {
                    new DiscordRPC.Button()
                    {
                        Label = "Join Server",
                        Url = "https://discord.gg/K5SmweCMNr"

                    }
                }
            });
            Logger.Log(LogType.Info, "Updated presence, appending timer");
            client.UpdateStartTime();
            Logger.Log(LogType.Info, "Completed CallRPCUpdateOnPageChange(string pageName)");
        }
    }
}

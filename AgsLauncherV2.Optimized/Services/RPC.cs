using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using DiscordRPC;
using DiscordRPC.Logging;

namespace AgsLauncherV2.Optimized.Services
{
    internal class RPC
    {
        public static DiscordRpcClient client;
        public static System.Windows.Controls.Label rpcLabel;
        public static System.Windows.Controls.Image pfpImage;
        public static void InitRPC()
        {
            client = new DiscordRpcClient("1023932512612925501");
            client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };
            client.OnPresenceUpdate += (sender, e) =>
            {
                Console.WriteLine("[AVGL2CE]: Recieved presence update from Discord.");
                Application.Current.Dispatcher.Invoke(RichPresenceConnectionSuccess, System.Windows.Threading.DispatcherPriority.ContextIdle);
            };
            client.OnConnectionEstablished += (sender, e) =>
            {
                Console.WriteLine("[AVGL2CE]: Connected to Discord.");
            };
            client.OnConnectionFailed += (sender, e) =>
            {
                //Application.Current.Dispatcher.Invoke(RichPresenceConnectionFailed, System.Windows.Threading.DispatcherPriority.ContextIdle);
            };
            client.Initialize();

            client.SetPresence(new RichPresence()
            {
                Details = "On the home page",
                Assets = new Assets()
                {
                    LargeImageKey = "ag2cover",
                    LargeImageText = "ags+kamo+3.0+dev--cuttingEdge",
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
            client.UpdateStartTime();
        }

        public static void RichPresenceConnectionSuccess()
        {
            rpcLabel.Content = "Welcome, " + client.CurrentUser.Username + "!";
            BitmapImage pfpBmp = new BitmapImage();
            pfpBmp.BeginInit();
            pfpBmp.UriSource = new Uri(client.CurrentUser.GetAvatarURL(User.AvatarFormat.PNG, User.AvatarSize.x16));
            pfpBmp.EndInit();
            pfpImage.Source = pfpBmp;
        }

        public static void CallRPCUpdateOnPageChange(string pageName)
        {
            client.SetPresence(new RichPresence()
            {
                Details = "Browsing the " + pageName + " page",
                Assets = new Assets()
                {
                    LargeImageKey = "ag2cover",
                    LargeImageText = "ags+kamo+2.8+dev--cuttingEdge",
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
            client.UpdateStartTime();
        }
    }
}

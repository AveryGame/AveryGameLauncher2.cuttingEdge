using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace AgsLauncherV2.Optimized.Services
{
    internal class Public
    {
        public static Pages.Uncollapsed.Home uncollapsedHome = new Pages.Uncollapsed.Home();
        public static Pages.Uncollapsed.Changelog uncollapsedChangelog = new Pages.Uncollapsed.Changelog();
        public static Pages.Uncollapsed.Bugs uncollapsedBugs = new Pages.Uncollapsed.Bugs();
        public static Pages.Uncollapsed.News uncollapsedNews = new Pages.Uncollapsed.News();
        public static Pages.Uncollapsed.Settings uncollapsedSettings = new Pages.Uncollapsed.Settings();
        public static MainWindow mainWindow = new MainWindow();
        public const string apiBase = "https://www.averyga.me/api/launcher/v2/cuttingedge/";
    }
    public class AGCloud
    {
        public bool showCuttingEdgeNotice { get; set; }
        public string newsHeader { get; set; }
        public string newsSubHeader { get; set; }
        public string newsDate { get; set; }
        public string newsImageUrl { get; set; }
    }
}

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
        public static Pages.Uncollapsed.Home uncollapsedHome;
        public static Pages.Uncollapsed.Changelog uncollapsedChangelog;
        public static Pages.Uncollapsed.Bugs uncollapsedBugs;
        public static Pages.Uncollapsed.News uncollapsedNews;
        public static Pages.Uncollapsed.Settings uncollapsedSettings;
        public static MainWindow mainWindow;
        public const string apiBase = "https://www.4drian.software/assets/averygame/launcher/v2/branches/cuttingedge/data/";
        public static string Avgl2cEVersion;
        public static string clientStrings;
        public static AGCloud json;
        public static AGUserPreferences userPreferences;
    }
    public class AGCloud
    {
        public bool showCuttingEdgeNotice { get; set; }
        public string prodLauncherClientVersion { get; set; }
        public string testerLauncherClientVersion { get; set; }
        public string devLauncherClientVersion { get; set; }
        public string prodGameClientVersion { get; set; }
        public string testerGameClientVersion { get; set; }
        public string devGameClientVersion { get; set; }
        public List<string> bugLogs { get; set; }
        public List<string> changeLogs { get; set; }
        public string newsImageUrl { get; set; }
        public string newsHeader { get; set; }
        public string newsSubheader { get; set; }
        public string newsDate { get; set; }
    }

    public class AGUserPreferences
    {
        public bool CollapseSidebar { get; set; }
        public string Arguments { get; set; }
        public string InstallPath { get; set; }
        public string Ag1InstallPath { get; set; }
        public string Ag2InstallPath { get; set; }
    }
}

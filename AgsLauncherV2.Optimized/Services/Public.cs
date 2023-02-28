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

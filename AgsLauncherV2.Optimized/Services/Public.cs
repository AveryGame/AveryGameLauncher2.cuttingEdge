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
    internal static class Public
    {
        public static Pages.Uncollapsed.Home UncollapsedHome;
        public static Pages.Uncollapsed.Changelog UncollapsedChangelog;
        public static Pages.Uncollapsed.Bugs UncollapsedBugs;
        public static Pages.Uncollapsed.News UncollapsedNews;
        public static Pages.Uncollapsed.Settings UncollapsedSettings;
        public static MainWindow MainWindow;
        public const string ApiBase = "https://www.4drian.software/assets/averygame/launcher/v2/branches/cuttingedge/data/";
        public static string Avgl2CeVersion;
        public static string BuildConfiguration;
        public static string ClientStrings;
        public static AgCloud Json;
        public static AgUserPreferences UserPreferences;
        public static string LogPath;
        public static PastebinAPI.User PbUser;
    }
    public class AgCloud
    {
        public bool ShowCuttingEdgeNotice { get; set; }
        public string ProdLauncherClientVersion { get; set; }
        public string TesterLauncherClientVersion { get; set; }
        public string DevLauncherClientVersion { get; set; }
        public string ProdGameClientVersion { get; set; }
        public string TesterGameClientVersion { get; set; }
        public string DevGameClientVersion { get; set; }
        public List<string> BugLogs { get; set; }
        public List<string> ChangeLogs { get; set; }
        public string NewsImageUrl { get; set; }
        public string NewsHeader { get; set; }
        public string NewsSubheader { get; set; }
        public string NewsDate { get; set; }
    }

    public class AgUserPreferences
    {
        public bool CollapseSidebar { get; set; }
        public string Arguments { get; set; }
        public string InstallPath { get; set; }
        public string Ag1InstallPath { get; set; }
        public string Ag2InstallPath { get; set; }
    }
}

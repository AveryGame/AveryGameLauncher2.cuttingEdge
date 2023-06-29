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

namespace AgsLauncherV2.Optimized.Pages.Uncollapsed
{
    /// <summary>
    /// Interaction logic for Bugs.xaml
    /// </summary>
    public partial class Bugs
    {
        public Bugs()
        {
            InitializeComponent();
            LoadPageSpecificJson();
        }


        //Unique page logic
        private void LoadPageSpecificJson()
        {
            Logger.Log(LogTypeEnum.Info, "Setting page-specific JSON for changelog page");
            VerStr.Text = "Game Version " + Json.DevGameClientVersion + " - Launcher Version " + Json.DevLauncherClientVersion;
            LogLine1.Text = Json.BugLogs[0];
            LogLine2.Text = Json.BugLogs[1];
            LogLine3.Text = Json.BugLogs[2];
            LogLine4.Text = Json.BugLogs[3];
            LogLine5.Text = Json.BugLogs[4];
            LogLine6.Text = Json.BugLogs[5];
            LogLine7.Text = Json.BugLogs[6];
            LogLine8.Text = Json.BugLogs[7];
            LogLine9.Text = Json.BugLogs[8];
            LogLine10.Text = Json.BugLogs[9];
            Logger.Log(LogTypeEnum.Info, "Appended all JSON strings to corresponding elements for changelog page");
            // TODO: Find a way to do this in shorter lines
        }
        //End unique page logic
    }
}

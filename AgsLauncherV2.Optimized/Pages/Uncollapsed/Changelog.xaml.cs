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
    /// Interaction logic for Changelog.xaml
    /// </summary>
    public partial class Changelog : Page
    {
        public Changelog()
        {
            InitializeComponent();
            LoadPageSpecificJson();
        }


        //Unique page logic
        private void LoadPageSpecificJson()
        {
            Logger.Log(LogType.Info, "Setting page-specific JSON for changelog page");
            VerSTR.Text = "Game Version " + Public.json.testerGameClientVersion + " - Launcher Version " + Public.json.testerGameClientVersion;
            LogLine1.Text = Public.json.changeLogs[0];
            LogLine2.Text = Public.json.changeLogs[1];
            LogLine3.Text = Public.json.changeLogs[2];
            LogLine4.Text = Public.json.changeLogs[3];
            LogLine5.Text = Public.json.changeLogs[4];
            LogLine6.Text = Public.json.changeLogs[5];
            LogLine7.Text = Public.json.changeLogs[6];
            LogLine8.Text = Public.json.changeLogs[7];
            LogLine9.Text = Public.json.changeLogs[8];
            LogLine10.Text = Public.json.changeLogs[9];
            Logger.Log(LogType.Info, "Appended all JSON strings to corresponding elements for changelog page");
        }
        //End unique page logic
    }
}

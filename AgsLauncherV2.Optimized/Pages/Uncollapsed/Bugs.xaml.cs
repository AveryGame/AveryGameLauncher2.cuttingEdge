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
    public partial class Bugs : Page
    {
        public Bugs()
        {
            InitializeComponent();
            LoadPageSpecificJson();
        }


        // All NavButton logic
        private void Home(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(uncollapsedHome);
        }

        private void Changelog(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(uncollapsedChangelog);
        }

        private void News(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(uncollapsedNews);
        }

        private void Settings(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(uncollapsedSettings);
        }
        // End NavButton logic


        //Unique page logic
        private void LoadPageSpecificJson()
        {
            VerSTR.Text = "Game Version " + Public.json.testerGameClientVersion + " - Launcher Version " + Public.json.testerLauncherClientVersion;
            LogLine1.Text = Public.json.bugLogs[0];
            LogLine2.Text = Public.json.bugLogs[1];
            LogLine3.Text = Public.json.bugLogs[2];
            LogLine4.Text = Public.json.bugLogs[3];
            LogLine5.Text = Public.json.bugLogs[4];
            LogLine6.Text = Public.json.bugLogs[5];
            LogLine7.Text = Public.json.bugLogs[6];
            LogLine8.Text = Public.json.bugLogs[7];
            LogLine9.Text = Public.json.bugLogs[8];
            LogLine10.Text = Public.json.bugLogs[9];
            // TODO: Find a way to do this in shorter lines
        }
        //End unique page logic
    }
}

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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : System.Windows.Controls.Page
    {
        public Settings()
        {
            InitializeComponent();
            LoadPreferences();
            CheckGameExistsLoop();
        }




        // Unique page logic
        private void LegalNotice_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Logger.Log(LogType.Info, "Legal notice clicked, taking user to license");
            Process.Start("explorer", "https://www.4drian.software/assets/averygame/launcher/v2/branches/cuttingedge/data/legal/license/");
            Logger.Log(LogType.Info, "Completed LegalNotice_MouseDown(object sender, MouseButtonEventArgs e)");
        }
        private async void CheckGameExistsLoop()
        {
            Logger.Log(LogType.Info, "Running CheckGameExistsLoop() for the first time");
            for (; ; )
            {
                if (File.Exists(Public.userPreferences.Ag1InstallPath))
                {
                    Logger.Log(LogType.Info, "Ag1 install path exists, enabling button");
                    DeleteAg.Visibility = Visibility.Visible;
                }
                if (!File.Exists(Public.userPreferences.Ag1InstallPath))
                {
                    Logger.Log(LogType.Info, "Ag1 install path does not exist, disabling button");
                    DeleteAg.Visibility = Visibility.Hidden;
                }
                Logger.Log(LogType.Info, "Completed checks, waiting 2.5 seconds before checking again");
                await Task.Delay(2500);
            }
        }
        private void OpenFileDialog(object sender, RoutedEventArgs e)
        {
            Logger.Log(LogType.Info, "Showing FolderBrowserDialog for changing AG install path");
            FolderBrowserDialog dlg = new();
            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dlg.SelectedPath))
            {
                JObject rss = JObject.Parse(File.ReadAllText(localAppData + "AGUserPreferences.apr"));
                rss["InstallPath"] = dlg.SelectedPath;
                File.WriteAllText(localAppData + "AGUserPreferences.apr", rss.ToString());
                Public.userPreferences = JsonConvert.DeserializeObject<AGUserPreferences>(rss.ToString());
                GamePath.Content = "Current install path: " +  dlg.SelectedPath;
            }
            Logger.Log(LogType.Info, "Completed OpenFileDialog(object sender, RoutedEventArgs e)");
        }
        private void LoadPreferences()
        {
            Logger.Log(LogType.Info, "Loading AGUserPreferences");
            if (File.Exists(Public.userPreferences.Ag1InstallPath))
            {
                DeleteAg.Visibility = Visibility.Visible;
            }
            Logger.Log(LogType.Info, "Syncing collapse sidebar checkbox to JSON state");
            CollapseCB.IsChecked = userPreferences.CollapseSidebar;
            Logger.Log(LogType.Info, "Syncing arguments text box to JSON contents");
            args.Text = userPreferences.Arguments;
            Logger.Log(LogType.Info, "Appending AGUserPreferences' InstallPath to GamePath.Content");
            GamePath.Content = "Current install path: " + userPreferences.InstallPath;
        }
        private static readonly string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\CuttingEdge\\";

        private void CollapseCB_Checked(object sender, RoutedEventArgs e)
        {
            Logger.Log(LogType.Info, "Collapse sidebar checked, writing to AGUserPreferences");
            JObject rss = JObject.Parse(File.ReadAllText(localAppData + "AGUserPreferences.apr"));
            rss["CollapseSidebar"] = true;
            File.WriteAllText(localAppData + "AGUserPreferences.apr", rss.ToString());
            Public.userPreferences = JsonConvert.DeserializeObject<AGUserPreferences>(rss.ToString());
            Logger.Log(LogType.Info, "Completed CollapseCB_Checked(object sender, RoutedEventArgs e)");
        }

        private void CollapseCB_Unchecked(object sender, RoutedEventArgs e)
        {
            Logger.Log(LogType.Info, "Collapse sidebar unchecked, writing to AGUserPreferences");
            JObject rss = JObject.Parse(File.ReadAllText(localAppData + "AGUserPreferences.apr"));
            rss["CollapseSidebar"] = false;
            File.WriteAllText(localAppData + "AGUserPreferences.apr", rss.ToString());
            Public.userPreferences = JsonConvert.DeserializeObject<AGUserPreferences>(rss.ToString());
            Logger.Log(LogType.Info, "Completed CollapseCB_Unchecked(object sender, RoutedEventArgs e)");
        }

        private void DeleteAg_Click(object sender, RoutedEventArgs e)
        {
            Logger.Log(LogType.Info, "DeleteAg_Click called, asking for confirmation");
            if (i == 0 || i ==1)
            {
                i++;
                DeleteAg.Content = "Are you sure?";
                Logger.Log(LogType.Info, "Set DeleteAg.Content to 'Are you sure?'");
            }
            if (i == 2)
            {
                Logger.Log(LogType.Info, "User confirmed to delete Ag1, deleting InstallPath directory from AGUserPreferences");
                Directory.Delete(userPreferences.InstallPath, true);
                DeleteAg.Visibility = Visibility.Hidden;
                JObject rss = JObject.Parse(File.ReadAllText(localAppData + "AGUserPreferences.apr"));
                rss["InstallPath"] = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\";
                File.WriteAllText(localAppData + "AGUserPreferences.apr", rss.ToString());
                Public.userPreferences = JsonConvert.DeserializeObject<AGUserPreferences>(rss.ToString());
                Public.uncollapsedHome.bAg1Installed = false;
                new ToastContentBuilder()
                    .AddArgument("action", "viewConversation")
                    .AddArgument("conversationId", 9813)
                    .AddText("Avery Game")
                    .AddText("Avery Game has been successfully deleted.")
                    .Show();
                Logger.Log(LogType.Info, "Completed DeleteAg_Click(object sender, RoutedEventArgs e)");
            }
        }

        // TODO: Add rpc option
        // TODO: Add toast option
        
        private int i = 0;
        // End unique page logic
    }
}

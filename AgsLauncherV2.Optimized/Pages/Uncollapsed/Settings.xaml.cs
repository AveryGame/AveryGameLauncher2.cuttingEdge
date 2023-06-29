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
    public partial class Settings
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
            Logger.Log(LogTypeEnum.Info, "Legal notice clicked, taking user to license");
            Process.Start("explorer", "https://www.4drian.software/assets/averygame/launcher/v2/branches/cuttingedge/data/legal/license/");
            Logger.Log(LogTypeEnum.Info, "Completed LegalNotice_MouseDown(object sender, MouseButtonEventArgs e)");
        }
        private async void CheckGameExistsLoop()
        {
            Logger.Log(LogTypeEnum.Info, "Running CheckGameExistsLoop() for the first time");
            for (; ; )
            {
                if (!string.IsNullOrWhiteSpace(UserPreferences.Ag1InstallPath) || UserPreferences.Ag1InstallPath != "null" || File.Exists(UserPreferences.Ag1InstallPath))
                {
                    DeleteAg.Visibility = Visibility.Visible;
                }
                if (string.IsNullOrWhiteSpace(UserPreferences.Ag1InstallPath) || UserPreferences.Ag1InstallPath == "null" || !File.Exists(UserPreferences.Ag1InstallPath))
                { 
                    DeleteAg.Visibility = Visibility.Hidden;
                }
                await Task.Delay(2500);
            }
            //"fUnCtIoN nEvEr ReTuRnS"
            //...thats the point, rider; thats the point.
        }
        private void OpenFileDialog(object sender, RoutedEventArgs e)
        {
            Logger.Log(LogTypeEnum.Info, "Showing FolderBrowserDialog for changing AG install path");
            FolderBrowserDialog dlg = new();
            var result = dlg.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dlg.SelectedPath))
            {
                var rss = JObject.Parse(File.ReadAllText(LocalAppData + "AGUserPreferences.apr"));
                rss["InstallPath"] = dlg.SelectedPath;
                File.WriteAllText(LocalAppData + "AGUserPreferences.apr", rss.ToString());
                UserPreferences = JsonConvert.DeserializeObject<AgUserPreferences>(rss.ToString());
                GamePath.Content = "Current install path: " +  dlg.SelectedPath;
            }
            Logger.Log(LogTypeEnum.Info, "Completed OpenFileDialog(object sender, RoutedEventArgs e)");
        }
        private void LoadPreferences()
        {
            Logger.Log(LogTypeEnum.Info, "Loading AGUserPreferences");
            if (File.Exists(UserPreferences.Ag1InstallPath))
            {
                DeleteAg.Visibility = Visibility.Visible;
            }
            Logger.Log(LogTypeEnum.Info, "Syncing collapse sidebar checkbox to JSON state");
            CollapseCb.IsChecked = UserPreferences.CollapseSidebar;
            Logger.Log(LogTypeEnum.Info, "Syncing arguments text box to JSON contents");
            Args.Text = UserPreferences.Arguments;
            Logger.Log(LogTypeEnum.Info, "Appending AGUserPreferences' InstallPath to GamePath.Content");
            GamePath.Content = "Current install path: " + UserPreferences.InstallPath;
        }
        private static readonly string LocalAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\CuttingEdge\\";

        private void CollapseCB_Checked(object sender, RoutedEventArgs e)
        {
            Logger.Log(LogTypeEnum.Info, "Collapse sidebar checked, writing to AGUserPreferences");
            var rss = JObject.Parse(File.ReadAllText(LocalAppData + "AGUserPreferences.apr"));
            rss["CollapseSidebar"] = true;
            File.WriteAllText(LocalAppData + "AGUserPreferences.apr", rss.ToString());
            UserPreferences = JsonConvert.DeserializeObject<AgUserPreferences>(rss.ToString());
            Logger.Log(LogTypeEnum.Info, "Completed CollapseCB_Checked(object sender, RoutedEventArgs e)");
        }

        private void CollapseCB_Unchecked(object sender, RoutedEventArgs e)
        {
            Logger.Log(LogTypeEnum.Info, "Collapse sidebar unchecked, writing to AGUserPreferences");
            var rss = JObject.Parse(File.ReadAllText(LocalAppData + "AGUserPreferences.apr"));
            rss["CollapseSidebar"] = false;
            File.WriteAllText(LocalAppData + "AGUserPreferences.apr", rss.ToString());
            UserPreferences = JsonConvert.DeserializeObject<AgUserPreferences>(rss.ToString());
            Logger.Log(LogTypeEnum.Info, "Completed CollapseCB_Unchecked(object sender, RoutedEventArgs e)");
        }

        private void DeleteAg_Click(object sender, RoutedEventArgs e)
        {
            Logger.Log(LogTypeEnum.Info, "DeleteAg_Click called, asking for confirmation");
            if (_i is 0)
            {
                _i++;
                DeleteAg.Content = "Are you sure?";
                Logger.Log(LogTypeEnum.Info, "Set DeleteAg.Content to 'Are you sure?'");
                return;
            }
            Logger.Log(LogTypeEnum.Info, "User confirmed to delete Ag1, deleting InstallPath directory from AGUserPreferences");
            Directory.Delete(UserPreferences.InstallPath + "Avery Game\\", true);
            DeleteAg.Visibility = Visibility.Hidden;
            var rss = JObject.Parse(File.ReadAllText(LocalAppData + "AGUserPreferences.apr"));
            rss["InstallPath"] = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\";
            rss["Ag1InstallPath"] = "null";
            File.WriteAllText(LocalAppData + "AGUserPreferences.apr", rss.ToString());
            UserPreferences = JsonConvert.DeserializeObject<AgUserPreferences>(rss.ToString());
            UncollapsedHome.BAg1Installed = false;
            UncollapsedHome.Ag1ImageTextureFilled.Opacity = 0;
            new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                .AddArgument("conversationId", 9813)
                .AddText("Avery Game")
                .AddText("Avery Game has been successfully deleted.")
                .Show();
            Logger.Log(LogTypeEnum.Info, "Completed DeleteAg_Click(object sender, RoutedEventArgs e)");
        }

        // TODO: Add rpc option
        // TODO: Add toast option
        
        private int _i;
        // End unique page logic
    }
}

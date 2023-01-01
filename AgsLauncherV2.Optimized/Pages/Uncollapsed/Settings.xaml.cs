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


using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using static AgsLauncherV2.Optimized.Services.Public;
using System.Windows.Shapes;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.IO;
using AgsLauncherV2.Optimized.Services;
using Newtonsoft.Json;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Threading.Tasks;

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
            Process.Start("explorer", "https://www.kianna.wtf/AveryGameLauncher2License");
        }
        private async void CheckGameExistsLoop()
        {
            for (; ; )
            {
                if (File.Exists(Public.userPreferences.Ag1InstallPath))
                {
                    DeleteAg.Visibility = Visibility.Visible;
                }
                if (!File.Exists(Public.userPreferences.Ag1InstallPath))
                {
                    DeleteAg.Visibility = Visibility.Hidden;
                }
                await Task.Delay(2500);
            }
        }
        private void OpenFileDialog(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dlg.SelectedPath))
            {
                JObject rss = JObject.Parse(File.ReadAllText(localAppData + "AGUserPreferences.json"));
                rss["InstallPath"] = dlg.SelectedPath;
                File.WriteAllText(localAppData + "AGUserPreferences.json", rss.ToString());
                Public.userPreferences = JsonConvert.DeserializeObject<AGUserPreferences>(rss.ToString());
                GamePath.Content = "Current install path: " +  dlg.SelectedPath;
            }
        }
        private void LoadPreferences()
        {
            if (File.Exists(Public.userPreferences.Ag1InstallPath))
            {
                DeleteAg.Visibility = Visibility.Visible;
            }
            CollapseCB.IsChecked = userPreferences.CollapseSidebar;
            args.Text = userPreferences.Arguments;
            GamePath.Content = "Current install path: " + userPreferences.InstallPath;
        }
        private static readonly string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\CuttingEdge\\";

        private void CollapseCB_Checked(object sender, RoutedEventArgs e)
        {
            JObject rss = JObject.Parse(File.ReadAllText(localAppData + "AGUserPreferences.json"));
            rss["CollapseSidebar"] = true;
            File.WriteAllText(localAppData + "AGUserPreferences.json", rss.ToString());
            Public.userPreferences = JsonConvert.DeserializeObject<AGUserPreferences>(rss.ToString());
        }

        private void CollapseCB_Unchecked(object sender, RoutedEventArgs e)
        {
            JObject rss = JObject.Parse(File.ReadAllText(localAppData + "AGUserPreferences.json"));
            rss["CollapseSidebar"] = false;
            File.WriteAllText(localAppData + "AGUserPreferences.json", rss.ToString());
            Public.userPreferences = JsonConvert.DeserializeObject<AGUserPreferences>(rss.ToString());
        }

        private void DeleteAg_Click(object sender, RoutedEventArgs e)
        {
            if (i == 0 || i ==1)
            {
                i++;
                DeleteAg.Content = "Are you sure?";
            }
            if (i == 2)
            {
                Directory.Delete(userPreferences.InstallPath, true);
                DeleteAg.Visibility = Visibility.Hidden;
                JObject rss = JObject.Parse(File.ReadAllText(localAppData + "AGUserPreferences.json"));
                rss["InstallPath"] = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\";
                File.WriteAllText(localAppData + "AGUserPreferences.json", rss.ToString());
                Public.userPreferences = JsonConvert.DeserializeObject<AGUserPreferences>(rss.ToString());
                Public.uncollapsedHome.bAg1Installed = false;
                new ToastContentBuilder()
                    .AddArgument("action", "viewConversation")
                    .AddArgument("conversationId", 9813)
                    .AddText("Avery Game")
                    .AddText("Avery Game has been successfully deleted.")
                    .Show();
            }
        }
        private int i = 0;
        // End unique page logic
    }
}

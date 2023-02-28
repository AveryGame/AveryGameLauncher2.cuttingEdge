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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
            LoadJsonOnPageEntry();
            LoadVarsOnPageEntry();
            CheckGameExistsLoop();
        }


        //Unique page logic
        private async void CheckGameExistsLoop()
        {
            for (; ; )
            {
                if (Enums.launcherStatus != LauncherStatus.downloading) 
                {
                    if (File.Exists(Public.userPreferences.Ag1InstallPath))
                    {
                        Ag1LaunchText.Content = "Launch Avery Game";
                        bAg1Installed = true;
                    }
                    if (!File.Exists(Public.userPreferences.Ag1InstallPath))
                    {
                        Ag1LaunchText.Content = "Install Avery Game";
                        bAg1Installed = false;
                    }
                }
                await Task.Delay(500);
            }
        }
        private void LoadVarsOnPageEntry()
        {
            BasicLogic.ag1LaunchButton = AG1Launch;
            BasicLogic.ag1LaunchLabel = Ag1LaunchText;
            if (File.Exists(userPreferences.Ag1InstallPath))
            {
                bAg1Installed = true;
            }
        }
        private void LaunchButtonLogic(object sender, RoutedEventArgs e)
        {
            if (bAg1Installed)
            {
                BasicLogic.LaunchAg1();
            }
            if (!bAg1Installed)
            {
                BasicLogic.DownloadAg1();
            }
        }

        private void LaunchButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            AnimationHandler.FadeAnimation(Ag1ImageTexture, 0.9, Ag1ImageTexture.Opacity, 0.8);
        }

        private void LaunchButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            AnimationHandler.FadeAnimation(Ag1ImageTexture, 0.5, Ag1ImageTexture.Opacity, 0.3);
        }

        private void LaunchButton_Copy_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            AnimationHandler.FadeAnimation(Ag2ImageTexture, 0.9, Ag2ImageTexture.Opacity, 0.8);
        }

        private void LaunchButton_Copy_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            AnimationHandler.FadeAnimation(Ag2ImageTexture, 0.5, Ag2ImageTexture.Opacity, 0.3);
        }
        
        private void LoadJsonOnPageEntry()
        {
            if (!File.Exists(Public.userPreferences.Ag1InstallPath))
            {
                Ag1LaunchText.Content = "Install Avery Game";
                bAg1Installed = false;
            }
        }

        public bool bAg1Installed;
        //End unique page logic
    }
}

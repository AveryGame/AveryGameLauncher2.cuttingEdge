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
    public partial class Home
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
            Logger.Log(LogTypeEnum.Info, "Running CheckGameExistsLoop() for the first time");
            for (; ; )
            {
                if (LauncherStatus != LauncherStatusEnum.Downloading) 
                {
                    if (File.Exists(UserPreferences.Ag1InstallPath))
                    {
                        Ag1LaunchText.Content = "Launch Avery Game";
                        BAg1Installed = true;
                    }
                    if (!File.Exists(UserPreferences.Ag1InstallPath))
                    {
                        Ag1LaunchText.Content = "Install Avery Game";
                        BAg1Installed = false;
                    }
                }
                
                await Task.Delay(5000);
            }
        }
        private void LoadVarsOnPageEntry()
        {
            Logger.Log(LogTypeEnum.Info, "Loading variables on page entry for home page, setting BasicLogic ag1 data fields to page objects");
            BasicLogic.Ag1LaunchButton = AG1Launch;
            BasicLogic.Ag1LaunchLabel = Ag1LaunchText;
            
            if (File.Exists(UserPreferences.Ag1InstallPath))
            {
                Logger.Log(LogTypeEnum.Info, "Ag1InstallPath returned a file, setting bAg1Installed to true");
                BAg1Installed = true;
            }
            
            Logger.Log(LogTypeEnum.Info, "Completed LoadVarsOnPageEntry() for home page");
        }
        private void LaunchButtonLogic(object sender, RoutedEventArgs e)
        {
            Logger.Log(LogTypeEnum.Info, "Called LaunchButtonLogic");
            switch (BAg1Installed)
            {
                case (true):
                    Logger.Log(LogTypeEnum.Info, "bAg1Installed returned true, launching Ag1");
                    BasicLogic.LaunchAg1();
                    break;
                default:
                    Logger.Log(LogTypeEnum.Info, "bAg1Installed returned false, downloading Ag1");
                    BasicLogic.DownloadAg1();
                    break;
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
            Logger.Log(LogTypeEnum.Info, "Loading page-specific JSON for home page");

            if (File.Exists(UserPreferences.Ag1InstallPath))
            {
                Logger.Log(LogTypeEnum.Info,
                        "Ag1InstallPath does not return a file, setting Ag1LaunchText.Content to 'Install Avery Game', setting bAg1Installed to false");
                Ag1LaunchText.Content = "Install Avery Game";
                BAg1Installed = false;
            }
        }

        public bool BAg1Installed;
        private static readonly string LocalAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\CuttingEdge\\";
        //End unique page logic
    }
}

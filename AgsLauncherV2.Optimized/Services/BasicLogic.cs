/*
 *AveryGame Launcher
 * Copyright(C) 2022, Avery Fiebig-Dorey & Tristan Gee
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
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows;
using static AgsLauncherV2.Optimized.Services.Enums;
using static AgsLauncherV2.Optimized.Services.Public;

namespace AgsLauncherV2.Optimized.Services
{
    internal class BasicLogic
    {
        public static void HandleClose(LauncherStatus windowStatus, [Optional] bool force, [Optional] bool useExitCode, [Optional] int exitCode)
        {
            if (windowStatus == LauncherStatus.initialized)
            {
                if (force)
                {
                    if (useExitCode)
                    {
                        _exitCode = exitCode;
                    }
                    Environment.Exit(_exitCode);
                }
                else
                {
                    if (useExitCode)
                    {
                        _exitCode = exitCode;
                    }
                    
                    Environment.Exit(_exitCode);
                }
            }
            if (windowStatus != LauncherStatus.initialized)
            {
                MessageBoxResult result = MessageBox.Show("AveryGame is not done downloading, and exiting will corrupt the download. Are you sure you want to exit?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    HandleClose(LauncherStatus.initialized, true, true, 2);
                }
                else if (result == MessageBoxResult.No)
                {
                    
                }
            }
        }
        
        public static void CheckAppData()
        {
            if (!Directory.Exists(Public.appData))
            {
                Directory.CreateDirectory(Public.appData);
                DownloadAppData();
            }
            else if (Directory.Exists(appData))
            {
                DownloadAppData();
            }
        }

        public static void DownloadAppData()
        {
            WebClient wc = new WebClient();
            wc.DownloadFile(new Uri(apiBase + "clientStrings.json"), appData + "clientStrings.json");
        }
        
        public static void CreateAppData()
        {
            Directory.CreateDirectory(appData);
        }
        
        private static int _exitCode = -1;
    }
}

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using static AgsLauncherV2.Optimized.Services.Enums;

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
        
        private static int _exitCode = -1;
    }
}
